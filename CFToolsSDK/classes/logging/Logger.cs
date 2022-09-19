using System;
using System.Collections.Generic;
using System.Text;

namespace CFToolsSDK.classes.logging
{
    public class Logger
    {

        internal Logger()
        {

        }



        public void Debug(string Message)
        {
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[DEBUG] -> " + Message);
            Console.ResetColor();
#endif
        }

        public void Log(string Message, bool LogToFile = false)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Message);
            Console.ResetColor();

            if (LogToFile)
            {
                throw new NotImplementedException("TODO: Implement this.");
            }
        }

        public void Error(Exception ex, string OptionalMessage = "")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("A fatal error has been occured.");
            if(OptionalMessage != String.Empty)
                Console.WriteLine(OptionalMessage);

            Console.WriteLine("Error-Message: " + ex.Message);
            Console.WriteLine("Error-Trace: " + ex.StackTrace);
            Console.WriteLine();
            Console.ResetColor();
        }




        private static Logger g_Instance;
        public static Logger GetInstance()
        {
            if (g_Instance == null)
                g_Instance = new Logger();

            return g_Instance;
        }
    }
}
