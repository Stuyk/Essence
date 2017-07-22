using Essence.classes.anticheat;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
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

        private void API_onPlayerExitVehicle(Client player, NetHandle vehicle)
        {
            Anticheat.playerLeftVehicle(player);
        }
    }
}
