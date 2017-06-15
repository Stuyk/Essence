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
            int dimension = new Random().Next(900000, 999999);
            API.consoleOutput(string.Format("{0} has connected. Set to dimension: {1}", player.name, dimension));
            API.setEntityDimension(player, dimension);
            API.freezePlayer(player, true);
            API.setEntityTransparency(player, 0);
        }
    }
}
