using System;
using System.Threading.Tasks;
using CFToolsSDK.classes;
using CFToolsSDK.classes.logging;

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

            Console.ReadKey();
        }
    }
}
