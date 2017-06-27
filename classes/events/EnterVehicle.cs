﻿using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.events
{
    public class EnterVehicle : Script
    {
        public EnterVehicle()
        {
            API.onPlayerEnterVehicle += API_onPlayerEnterVehicle;
        }

        private void API_onPlayerEnterVehicle(Client player, GTANetworkShared.NetHandle vehicle)
        {
            if (API.getVehicleLocked(vehicle))
            {
                API.warpPlayerOutOfVehicle(player);
                API.setVehicleLocked(vehicle, true);
                API.setVehicleDoorState(vehicle, -1, false);
                API.setVehicleDoorState(vehicle, 1, false);
            }
        }
    }
}
