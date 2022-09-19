﻿using System;
using System.Collections.Generic;
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
            List<Leaderboard> board = await webManager.GetLeaderborad("24234c37-0703-49dc-9145-b67da3863723", Enums.LEADERBOARD_STAT.kills, Enums.LEADERBOARD_ORDER.DESCENDING, 20);
            if(board != null)
            {
                Logger.GetInstance().Debug("Successfully recived Leaderboard!");
                foreach (var entry in board)
                {
                    Logger.GetInstance().Debug($"{entry.rank}. {entry.latest_name} has {entry.kills} with a KD {entry.kdratio}");
                }
            }
            Console.ReadKey();
        }
    }
}