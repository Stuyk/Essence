using Essence.classes.anticheat;
using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.events
{
    public class ExitVehicle : Script
    {
        public ExitVehicle()
        {
            API.onPlayerExitVehicle += API_onPlayerExitVehicle;
        }

        private void API_onPlayerExitVehicle(Client player, GTANetworkShared.NetHandle vehicle)
        {
            Anticheat.playerLeftVehicle(player);
        }
    }
}
