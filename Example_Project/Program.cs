using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using CFToolsSDK.classes;
using CFToolsSDK.classes.logging;
using CFToolsSDK.classes.models;
using XAct;

namespace Example_Project
{
    internal class Program
    {
        private static CFToolsWebManager webManager;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            webManager = new CFToolsWebManager();
            MainAsync().Wait();

        }

        static async Task MainAsync()
        {

            Logger.GetInstance().Log("CF-Tools Auth connected!");
            while (!webManager.IsAuthorized)
            {
                Logger.GetInstance().Debug("Waiting for Auth...........");
            }
            //FullPlayerList playerlist = await webManager.GetFullPlayerList("");
            /*if(playerlist != null)
            {
                Logger.GetInstance().Debug("Successfully recived Leaderboard!");
                foreach (var entry in playerlist.sessions)
                {
                    Logger.GetInstance().Debug($"{entry.cftools_id} with name: {entry.gamedata.player_name} SessionId: {entry.id}");
                }
            }
            */

            /*DateTime expires = DateTime.UtcNow;
            expires = expires.AddDays(2);
            bool test = await webManager.AddQueuePriority("", "", "Sheesh", CFHelper.ConvertDateTimeToIso8601Time(expires));
            */

            WhiteListResponse whitelist = await webManager.GetWhitelist("24234c37-0703-49dc-9145-b67da3863723", "6227e595092b438359f52d71");

            bool addedSuccessfully = await webManager.AddWhiteListEntry();

            Console.ReadKey();
        }
    }
}
