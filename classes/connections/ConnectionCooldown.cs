using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Essence.classes.connections
{
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
                API.kickPlayer(player, "Please wait up to 60 seconds since your last logout to rejoin.");
                return;
            }
        }

        private void API_onResourceStart()
        {
            Timer timer = new Timer();
            timer.Interval = 60000;
            timer.Start();
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ConnectionManager.CheckClients();
        }
    }
}
