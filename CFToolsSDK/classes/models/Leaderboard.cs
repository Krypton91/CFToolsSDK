using System;
using System.Collections.Generic;
using System.Text;

namespace CFToolsSDK.classes.models
{
    public class Leaderboard
    {
        public string cftools_id { get; set; }
        public int deaths { get; set; }
        public int hits { get; set; }
        public double kdratio { get; set; }
        public int kills { get; set; }
        public string latest_name { get; set; }
        public double longest_kill { get; set; }
        public double longest_shot { get; set; }
        public int playtime { get; set; }
        public int rank { get; set; }
        public int suicides { get; set; }
        public int? environment_deaths { get; set; }
        public int? infected_deaths { get; set; }
        public int? falldamage_deaths { get; set; }
    }

    public class LeaderboardResponse
    {
        public List<Leaderboard> leaderboard { get; set; }
        public bool status { get; set; }
    }
}
