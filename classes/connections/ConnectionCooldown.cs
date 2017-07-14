using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Essence.classes.connections
{
    // This class will make sure the player gets kicked if they're on the cooldown list.
    // The cooldown list is to prevent exploiting of the Payload system for saves.
    // This pretty much enforces users to have to wait for two cycles of saves after they log out.
    // Preventing any manipulation of save data, and lets us have bulk saves.
    public class ConnectionCooldown : Script
    {
        public ConnectionCooldown()
        {
            API.onResourceStart += API_onResourceStart;
            API.onPlayerBeginConnect += API_onPlayerBeginConnect;
        }

        private void API_onPlayerBeginConnect(Client player, CancelEventArgs cancelConnection)
        {
            if (ConnectionManager.CheckAddress(player.address))
            {
                API.kickPlayer(player, "Please wait up to 10 seconds since your last logout to rejoin.");
                return;
            }
        }

        private void API_onResourceStart()
        {
            Timer timer = new Timer();
            timer.Interval = 10000;
            timer.Start();
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ConnectionManager.CheckClients();
        }
    }
}
