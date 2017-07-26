using Essence.classes.anticheat;
using Essence.classes.connections;
using Essence.classes.discord;
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

namespace Essence.classes
{
    class Disconnect : Script
    {
        Database db = new Database();

        public Disconnect()
        {
            API.onPlayerDisconnected += API_onPlayerDisconnected;
        }

        private void API_onPlayerDisconnected(Client player, string reason)
        {
            // Add our player to our connection cooldown.
            ConnectionManager.AddClient(player.address);

            if (!API.hasEntityData(player, "Instance"))
                return;

            Player instance = (Player)API.getEntityData(player, "Instance");
            // Used for players in interiors. Logs them out outside the interior.
            if (player.hasData("LastPosition"))
                player.position = player.getData("LastPosition");

            if (player.hasData("Anticheat"))
            {
                AnticheatInfo info = player.getData("Anticheat");
                info.isOnline = false;
                info.LastPosition = player.position;
            }

            
            // Update player data just as they disconnect.
            instance.updatePlayerPosition();
            instance.PlayerClothing.savePlayerClothes();
            instance.PlayerInventory.saveInventory();
            instance.removePlayerVehicles();

            DiscordBot.sendMessageToServer(string.Format("{0} has logged out from the server.", player.name));
        }

        private void removePlayerVehicles(Player instance)
        {
            instance.removePlayerVehicles();
        }
    }
}
