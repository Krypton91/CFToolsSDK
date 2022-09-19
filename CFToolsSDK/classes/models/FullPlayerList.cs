using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFToolsSDK.classes.models
{
    public class Bans
    {
        public bool community { get; set; }
        public string economy { get; set; }
        public int game { get; set; }
        public int last_ban { get; set; }
        public int vac { get; set; }
    }

    public class Connection
    {
        public string country_code { get; set; }
        public CountryNames country_names { get; set; }
        public string ipv4 { get; set; }
        public bool malicious { get; set; }
        public string provider { get; set; }
    }

    public class CountryNames
    {
        public string de { get; set; }
        public string en { get; set; }
        public string es { get; set; }
        public string fr { get; set; }
        public string ja { get; set; }

        [JsonProperty("pt-BR")]
        public string PtBR { get; set; }
        public string ru { get; set; }

        [JsonProperty("zh-CN")]
        public string ZhCN { get; set; }
    }

    public class Gamedata
    {
        public string player_name { get; set; }
        public string steam64 { get; set; }
    }

    public class Info
    {
        public int ban_count { get; set; }
        public List<object> labels { get; set; }
    }

    public class Live
    {
        public double load_time { get; set; }
        public bool loaded { get; set; }
        public Ping ping { get; set; }
        public Position position { get; set; }
    }

    public class Persona
    {
        public Bans bans { get; set; }
        public Profile profile { get; set; }
    }

    public class Ping
    {
        public int actual { get; set; }
        public int trend { get; set; }
    }

    public class Position
    {
        public List<double> join { get; set; }
        public List<double> latest { get; set; }
        public object leave { get; set; }
    }

    public class Profile
    {
        public string avatar { get; set; }
        public string name { get; set; }
        public bool @private { get; set; }
    }

    public class FullPlayerList
    {
        public List<Session> sessions { get; set; }
        public bool status { get; set; }
    }

    public class Session
    {
        public string cftools_id { get; set; }
        public Connection connection { get; set; }
        public DateTime created_at { get; set; }
        public Gamedata gamedata { get; set; }
        public string id { get; set; }
        public Info info { get; set; }
        public Live live { get; set; }
        public Persona persona { get; set; }
        public Stats stats { get; set; }
    }

    public class Stats
    {
        public int? deaths { get; set; }
        public int? hits { get; set; }
        public double? longest_shot { get; set; }
        public int? suicides { get; set; }
        public int? kills { get; set; }
        public double? longest_kill { get; set; }
        public int? environment_deaths { get; set; }
        public int? infected_deaths { get; set; }
    }
}
