using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.connections
{
    // This class is used to add / remove ips from the cooldown list.
    // Basically adds them in and adds 60 seconds to the time they were added in on.
    // Then it loops through and checks if they are ready to be removed.
    // If they are, it will remove them automatically.
    public static class ConnectionManager
    {
        private static Dictionary<string, DateTime> ips = new Dictionary<string, DateTime>();
        // Cooldown before a player can rejoin.
        public static void AddClient(string ip)
        {
            if (ips.ContainsKey(ip))
                return;

            ips.Add(ip, DateTime.Now.AddSeconds(60));
        }
        // Remove a client.
        public static void RemoveClient(string ip)
        {
            if (!ips.ContainsKey(ip))
                return;
            ips.Remove(ip);
        }
        // Check the available clients.
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
                return;

            int count = 0;

            foreach (string ip in removeables)
            {
                if (!ips.ContainsKey(ip))
                    continue;
                ips.Remove(ip);
                count++;
                continue;
            }

            Console.WriteLine($"[Connection Manager] Removed IPs from manager: {count}");
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
