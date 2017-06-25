﻿using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.events
{
    public class EventHandler : Script
    {
        public EventHandler()
        {
            API.onClientEventTrigger += API_onClientEventTrigger;
        }

        private void API_onClientEventTrigger(Client player, string eventName, params object[] arguments)
        {
            // Login Switch
            switch (eventName)
            {
                case "clientLogin":
                    if (arguments.Length <= 0)
                    {
                        return;
                    }
                    API.call("Login", "cmdLogin", player, arguments[0].ToString(), arguments[1].ToString());
                    return;
                case "clientRegister":
                    if (arguments.Length <= 0)
                    {
                        return;
                    }
                    API.call("Register", "cmdRegister", player, arguments[0].ToString(), arguments[1].ToString());
                    return;
                default:
                    break;
            }

            // In Vehicle Switch
            if (player.isInVehicle)
            {
                switch (eventName)
                {
                    case "Vehicle_Engine":
                        API.call("VehicleCommands", "ToggleEngine", player);
                        return;
                }
            }

            // Out of Vehicle Switch
            if (!player.isInVehicle)
            {
                switch (eventName)
                {
                    case "SHOP_ATM":
                        // !!! IMPLEMENT PLS !!!
                        API.sendChatMessageToPlayer(player, "Don't forget to implement this shit.");
                        return;
                    case "JOB_LONG_RANGE_TRUCKING":
                        API.call("LongRangeTrucking", "startLongRangeTruckingJob", player);
                        return;
                    case "JOB_SHORT_RANGE_TRUCKING":
                        API.call("ShortRangeTrucking", "startShortRangeTruckingJob", player);
                        return;
                }
            }


            
        }
    }
}
