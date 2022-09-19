using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CFToolsSDK.classes.config;
using CFToolsSDK.classes.logging;
using CFToolsSDK.classes.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        private async Task<Tuple<bool, string>> Get(string endPointURL, Dictionary<string, string> RequestParams)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthToken);
            var builder = new UriBuilder(BASE_URL + endPointURL);
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            foreach (var item in RequestParams)
            {
                query[item.Key] = item.Value;
            }

            builder.Query = query.ToString();
            string url = builder.ToString();
            var res = await client.GetAsync(url);
            var content = await res.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return Tuple.Create(res.IsSuccessStatusCode, content);
        }
    }
}
