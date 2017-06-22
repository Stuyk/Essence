using GTANetworkServer;
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
            switch (eventName)
            {
                case "clientLogin":
                    API.call("Login", "cmdLogin", player, arguments[0].ToString(), arguments[1].ToString());
                    return;
                case "clientRegister":
                    API.call("Register", "cmdRegister", player, arguments[0].ToString(), arguments[1].ToString());
                    return;
            }

            if (!API.hasEntityData(player, "LoggedIn"))
            {
                return;
            }

            if (player.isInVehicle)
            {
                switch (eventName)
                {
                    case "Vehicle_Engine":
                        API.call("VehicleCommands", "ToggleEngine", player);
                        return;
                }
            }

            if (!player.isInVehicle)
            {
                switch (eventName)
                {

                }
            }


            
        }
    }
}
