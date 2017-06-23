using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.connections
{
    public static class ConnectionManager
    {
        private static Dictionary<string, DateTime> ips = new Dictionary<string, DateTime>();

        public static void AddClient(string ip)
        {
            if (ips.ContainsKey(ip))
            {
                return;
            }

            ips.Add(ip, DateTime.Now.AddSeconds(60));
        }

        public static void RemoveClient(string ip)
        {
            if (!ips.ContainsKey(ip))
            {
                return;
            }

            ips.Remove(ip);
        }

        public static void CheckClients()
        {
            List<string> removeables = new List<string>();

            foreach (string ip in ips.Keys)
            {
                if (ips[ip] > DateTime.Now)
                {
                    removeables.Add(ip);
                    continue;
                }
            }

            if (removeables.Count <= 0)
            {
                return;
            }

            int count = 0;

            foreach (string ip in removeables)
            {
                if (ips.ContainsKey(ip))
                {
                    ips.Remove(ip);
                    count++;
                    continue;
                }
            }

            Console.WriteLine("[Connection Manager] Removed {0} ips from connection manager.", count);
        }

        public static bool CheckAddress(string ip)
        {
            if (ips.ContainsKey(ip))
            {
                return true;
            }
            return false;
        }
    }
}
