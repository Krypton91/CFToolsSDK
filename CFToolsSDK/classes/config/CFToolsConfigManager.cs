using System.Configuration;

namespace CFToolsSDK.classes.config
{
    internal class CFToolsConfigManager
    {
        internal string Application_id { get; set; }
        internal string secret { get; set; }

        internal CFToolsConfigManager()
        {
            this.Application_id = ConfigurationManager.AppSettings["Application_id"];
            this.secret = ConfigurationManager.AppSettings["secret"];
        }
    }
}
