using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.commands
{
    class VehicleCommands : Script
    {
        public VehicleCommands() { }

        [Command("findvehicles")]
        public void cmdFindVehicles(Client player)
        {
            if (!API.hasEntityData(player, "Instance"))
            {
                return;
            }

            Player instance = (Player)API.getEntityData(player, "Instance");

            int count = 0;

            foreach (Vehicle vehInfo in instance.PlayerVehicles)
            {
                API.triggerClientEvent(player, "Temp_Blip", API.getEntityPosition(vehInfo.Handle));
                count++;
            }

            API.sendChatMessageToPlayer(player, string.Format("~b~Essence: ~w~Found a total of {0} vehicles.", count));
        }
    }
}
