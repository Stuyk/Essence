using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes
{
    class Connect : Script
    {
        public Connect()
        {
            API.onPlayerConnected += API_onPlayerConnected;
        }

        private void API_onPlayerConnected(Client player)
        {
            API.setEntityDimension(player, new Random().Next(90000, 99999));
            API.freezePlayer(player, true);
            API.setEntityTransparency(player, 0);
        }
    }
}
