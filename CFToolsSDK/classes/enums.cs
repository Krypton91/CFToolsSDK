using System;
using System.Collections.Generic;
using System.Text;

namespace CFToolsSDK.classes
{
    public class Enums
    {
        public enum LEADERBOARD_STAT
        {
            kills,
            deaths,
            suicides,
            playtime,
            longest_kill,
            longest_shot,
            kdratio
        }

        public enum LEADERBOARD_ORDER
        {
            ASCEDING = 1,
            DESCENDING = -1
        }
    }
}
