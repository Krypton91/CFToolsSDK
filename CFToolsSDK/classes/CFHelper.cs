using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using XSystem.Security.Cryptography;

namespace CFToolsSDK.classes
{
    public static class CFHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="IPAdress"></param> Gameserver IP-Adress.
        /// <param name="Port"></param> Gameserver port
        /// <param name="GameId"></param> 1 = DayZ check CF-Tools docs for more infos!
        /// <returns>Server_Id used from cf to identify the Gameserver</returns>
        public static string GenerateServerId(string IPAdress, string Port, string GameId = "1")
        {
            string input = GameId + IPAdress + Port;
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }

        public static string ConvertDateTimeToIsco8601Time(DateTime time)
        {
            return time.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz", CultureInfo.InvariantCulture);
        }
    }
}
