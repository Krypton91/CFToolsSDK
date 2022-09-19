using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CFToolsSDK.classes.config;
using CFToolsSDK.classes.logging;
using Newtonsoft.Json.Linq;

namespace CFToolsSDK.classes
{
    public class CFToolsWebManager
    {
        readonly string BASE_URL = "https://data.cftools.cloud";
        private string AuthToken { get; set; }
        public bool IsAuthorized { get; set; }


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
            Console.WriteLine("APPID: " + Application_id);
            Console.WriteLine("secret: " + secret);
            bool result = await Auth(Application_id, secret);
            if (result)
            {
                Logger.GetInstance().Log("[CF-Tools Clound]-> authorization successfully!");
            }
            else
            {
                Logger.GetInstance().Error(new Exception("[CF-Tools Clound]-> ERROR: authorization forbidden!\nPlease check your credentials JSON!"));
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

        public void Test()
        {

        }
    }
}
