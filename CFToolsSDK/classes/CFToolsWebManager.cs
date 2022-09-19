using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using CFToolsSDK.classes.config;
using CFToolsSDK.classes.logging;
using CFToolsSDK.classes.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XAct.Library.Settings;
using static CFToolsSDK.classes.Enums;

namespace CFToolsSDK.classes
{
    public class CFToolsWebManager
    {
        readonly string BASE_URL = "https://data.cftools.cloud";
        private string AuthToken { get; set; }
        public bool IsAuthorized { get; set; }

        public string GetAuthToken()
        {
            return this.AuthToken;
        }

        /// <summary>
        /// used to fetch the auth token from WEBAPI
        /// </summary>
        /// <returns>true when Auth was successfully otherwise false.</returns>

        public async Task <bool> RegenerateAuthToken()
        {
            CFToolsConfigManager config = new CFToolsConfigManager();
            string Application_id = config.Application_id;
            string secret = config.secret;
            return await Auth(Application_id, secret);
        }

        public CFToolsWebManager(bool AutorenewToken = false)
        {
            Logger.GetInstance().Debug("Init CF-Tools Web Manager.");
            Init();
        }

        private async void Init()
        {
            CFToolsConfigManager config = new CFToolsConfigManager();
            string Application_id = config.Application_id;
            string secret = config.secret;
            bool result = await Auth(Application_id, secret);
            IsAuthorized = result;
            if (result)
            {
                Logger.GetInstance().Log("[CF-Tools Cloud]-> authorization successfully!");
            }
            else
            {
                Logger.GetInstance().Error(new Exception("[CF-Tools Cloud]-> ERROR: authorization forbidden!\nPlease check your credentials JSON!"));
            }
        }



        private async Task<bool> Auth(string Application_id, string secret)
        {
            string endPointURL = "/v1/auth/register";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            var data = new Dictionary<string, string>
            {
                {"application_id", Application_id},
                {"secret", secret}
            };

            var res = await client.PostAsync(endPointURL, new FormUrlEncodedContent(data));
            var content = await res.Content.ReadAsStringAsync();
            dynamic responseData = JObject.Parse(content);
            string Token = responseData["token"];
            AuthToken = Token;
            return res.IsSuccessStatusCode && !string.IsNullOrEmpty(AuthToken);
        }

        public async Task <GameServer> GetGameServer(string ServerHash)
        {
            string endPoint = $"/v1/gameserver/{ServerHash}";
            var result = await Get(endPoint, null);
            if (result.Item1)
            {
                Console.WriteLine(result.Item2);
                var jo = JObject.Parse(result.Item2);
                GameServer server = new GameServer();
                var data = (JObject)jo[ServerHash];
                if (data != null)
                {
                    var _object = data.SelectToken("_object");
                    server.created_at = _object.SelectToken("created_at").ToString();
                    server.updated_at = _object.SelectToken("updated_at").ToString();
                    server.error = _object.SelectToken("error").ToString();
                    var attributes = data.SelectToken("attributes");
                    server.experimental = bool.Parse(attributes.SelectToken("experimental").ToString());
                    server.hive = attributes.SelectToken("hive").ToString();
                    server.modded = bool.Parse(attributes.SelectToken("modded").ToString());
                    server.official = bool.Parse(attributes.SelectToken("official").ToString());
                    server.whitelist = bool.Parse(attributes.SelectToken("whitelist").ToString());

                    var environment = data.SelectToken("environment");
                    var perspectives = environment.SelectToken("perspectives");
                    server.isTPPEnabled = bool.Parse(perspectives.SelectToken("3rd").ToString());
                    server.isFPPEnabled = bool.Parse(perspectives.SelectToken("1rd").ToString());
                    server.time = environment.SelectToken("time").ToString();

                    var time_acceleration = environment.SelectToken("time_acceleration");
                    server.time_acceleration_general = time_acceleration.SelectToken("general").ToString();
                    server.time_acceleration_night = time_acceleration.SelectToken("night").ToString();

                    var geoloc = data.SelectToken("geolocation");

                    GeoLocation loc = new GeoLocation();
                    loc.available = bool.Parse(geoloc.SelectToken("available").ToString());
                    
                    var city = geoloc.SelectToken("city");
                    loc.city_name = city.SelectToken("name").ToString();
                    loc.city_region = city.SelectToken("region").ToString();

                    loc.continent = geoloc.SelectToken("continent").ToString();

                    var country = geoloc.SelectToken("country");
                    loc.country_Code = country.SelectToken("code").ToString();
                    loc.country_Code = country.SelectToken("name").ToString();
                    loc.timezone = geoloc.SelectToken("timezone").ToString();

                    server.location = loc;

                    server.map_name = data.SelectToken("map").ToString();

                    string ModList = data.SelectToken("mods").ToString();
                    var Data = JsonConvert.DeserializeObject<List<Mod>>(ModList);
                    server.mods = Data;


                    server.name = data.SelectToken("name").ToString();
                    server.offline = bool.Parse(data.SelectToken("offline").ToString());
                    server.Rank = int.Parse(data.SelectToken("rank").ToString());
                    server.Rating = int.Parse(data.SelectToken("rating").ToString());
                    var security = data.SelectToken("security");
                    server.isBattleyeEnabled = bool.Parse(security.SelectToken("battleye").ToString());
                    server.isPasswordProtected = bool.Parse(security.SelectToken("password").ToString());
                    server.vac = bool.Parse(security.SelectToken("vac").ToString());

                    var status = data.SelectToken("status");
                    Jwstatus state = new Jwstatus();
                    state.bots = bool.Parse(status.SelectToken("bots").ToString());
                    state.players = int.Parse(status.SelectToken("players").ToString());

                    var queue = status.SelectToken("queue");
                    state.Queue = bool.Parse(queue.SelectToken("active").ToString());
                    state.QueueCount = int.Parse(queue.SelectToken("size").ToString());
                    server.status = state;
                    server.slots = int.Parse(status.SelectToken("slots").ToString());
                    server.version = data.SelectToken("version").ToString();
                    return server;
                }
            }
            else
                Logger.GetInstance().Error(new Exception("[CF-Tools Cloud] -> Response was not successfully!"));

            return null;
        }

        public async Task<List<Leaderboard>> GetLeaderborad(string server_api_id, LEADERBOARD_STAT stat, LEADERBOARD_ORDER order, int limit)
        {
            int Order = (int)order;
            var ReqData = new Dictionary<string, string>
            {
                    {"stat", stat.ToString()},
                    {"order", Order.ToString()},
                    {"limit", limit.ToString() }
            };

            string endPoint = $"/v1/server/{server_api_id}/leaderboard";
            var result = await Get(endPoint, ReqData);
            if (result.Item1)
            {
                var leaderboard = JsonConvert.DeserializeObject<LeaderboardResponse>(result.Item2);
                return leaderboard.leaderboard;
            }
            else
                Logger.GetInstance().Error(new Exception("[CF-Tools Cloud] -> Response was not successfully!"));

            return null;
        }


        public async Task<FullPlayerList> GetFullPlayerList(string server_api_id)
        {
            string endPoint = $"/v1/server/{server_api_id}/GSM/list";
            var result = await Get(endPoint, null);
            if (result.Item1)
            {
                var Data = JsonConvert.DeserializeObject<FullPlayerList>(result.Item2);
                return Data;
            }
            else
                Logger.GetInstance().Error(new Exception("[CF-Tools Cloud] -> Response was not successfully!"));

            return null;
        }

        public async Task<bool> KickPlayer(string server_api_id, string gamesession_id, string reason)
        {
            string endPoint = $"/v1/server/{server_api_id}/kick";
            var ReqData = new Dictionary<string, string>
            {
                    {"gamesession_id", gamesession_id},
                    {"order", reason}
            };
            var result = await Post(endPoint, ReqData);
            return result.Item1;
        }

        public async Task<bool> SendPrivateMessage(string server_api_id, string gamesession_id, string content)
        {
            string endPoint = $"/v1/server/{server_api_id}/message-private";
            var ReqData = new Dictionary<string, string>
            {
                    {"gamesession_id", gamesession_id},
                    {"content", content}
            };
            var result = await Post(endPoint, ReqData);
            return result.Item1;
        }

        private async Task<Tuple<bool, string>> Get(string endPointURL, Dictionary<string, string> RequestParams)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthToken);
            var builder = new UriBuilder(BASE_URL + endPointURL);
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            if (RequestParams != null)
            {
                foreach (var item in RequestParams)
                {
                    query[item.Key] = item.Value;
                }

                builder.Query = query.ToString();
            }
            string url = builder.ToString();
            var res = await client.GetAsync(url);
            var content = await res.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return Tuple.Create(res.IsSuccessStatusCode, content);
        }


        private async Task<Tuple<bool, string>> Post(string endPointURL, Dictionary<string, string> RequestParams)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthToken);
            var builder = new UriBuilder(BASE_URL + endPointURL);
            string url = builder.ToString();
            var encodedContent = new FormUrlEncodedContent(RequestParams);
            var res = await client.PostAsync(url, encodedContent);
            var content = await res.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return Tuple.Create(res.IsSuccessStatusCode, content);
        }
    }
}
