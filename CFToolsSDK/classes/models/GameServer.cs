using System;
using System.Collections.Generic;
using System.Text;

namespace CFToolsSDK.classes.models
{
    public class GameServer
    {
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string error { get; set; }
        public bool experimental { get; set; }
        public string hive { get; set; }
        public bool modded { get; set; }
        public bool official { get; set; }
        public string shard { get; set; }
        public string name { get; set; }
        public bool offline { get; set; }
        public bool whitelist { get; set; }
        public bool isFPPEnabled { get; set; }
        public bool isTPPEnabled { get; set; }
        public string time { get; set; }
        public string time_acceleration_general { get; set; }
        public string time_acceleration_night { get; set; }

        public GeoLocation location { get; set; }
        public List<Mod> mods { get; set; }
        public Jwstatus status { get; set; }
        public string map_name { get; set; }
        public int Rank { get; set; }
        public int Rating { get; set; }
        public bool isPasswordProtected { get; set; }
        public bool isBattleyeEnabled { get; set; }
        public bool vac { get; set; }
        public int slots { get; set; }
        public string version { get; set; }
    }

    public class GeoLocation
    {
        public bool available { get; set; }
        public string city_name { get; set; }
        public string city_region { get; set; }
        public string continent { get; set; }
        public string country_Code { get; set; }
        public string country_Name { get; set; }
        public string timezone { get; set; }
    }

    public class Mod
    {
        
        public long file_id { get; set; }
        public string name { get; set; }
    }

    public class Jwstatus
    {
        public bool bots { get; set; }
        public int players { get; set; }
        public bool Queue { get; set; }
        public int QueueCount { get; set; }
    }
}
