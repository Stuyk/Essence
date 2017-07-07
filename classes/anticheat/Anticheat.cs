using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.anticheat
{
    public class Anticheat : Script
    {
        public Anticheat()
        {
            API.onUpdate += API_onUpdate;
            API.onPlayerConnected += API_onPlayerConnected;
        }

        private void API_onPlayerConnected(Client player)
        {
            
        }

        private void API_onUpdate()
        {
            
        }
        /*
        private bool isPlayerTeleportHacking(Client player)
        {
           ///
        }
        */
    }
}
