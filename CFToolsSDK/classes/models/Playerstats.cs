using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace CFToolsSDK.classes.models
{
    public class Playerstats
    {
        public string cftools_id { get; set; }
        public DateTime cleared_at { get; set; }
        public DateTime created_at { get; set; }
        public Game game { get; set; }
        public Omega omega { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class Game
    {
        public General general { get; set; }
    }

    public class General
    {
        public int deaths { get; set; }
        public double kdratio { get; set; }
        public List<Weapon> used_weapons { get; set; }
    }

    public class Omega
    {
        public List<string> name_history { get; set; }
        public int playtime { get; set; }
        public int sessions { get; set; }
    }

    public class Weapon
    {
        public string name { get; set; }
        public decimal damage { get; set; }
        public int deaths { get; set; }
        public decimal hits { get; set; }
        public int kills { get; set; }
        public decimal longest_kill { get; set; }
        public decimal longest_shot { get; set; }
    }
}
