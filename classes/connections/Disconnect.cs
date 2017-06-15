using GTANetworkServer;
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
            if (!API.hasEntityData(player, "Instance"))
            {
                API.consoleOutput(string.Format("Disconnected unknown user, {0}", player.name));
                return;
            }

            API.consoleOutput(string.Format("Disconnected registered user, {0}", player.name));
            Player instance = (Player)API.getEntityData(player, "Instance");
            logoutPlayer(instance);
            removePlayerVehicles(instance);
        }

        // Looks up the player by their ID and updates them based on their unique id.
        private void logoutPlayer(Player instance)
        {
            // Pull our users instance.
            
            string before = "UPDATE Players SET";
            string[] varNames = { "LoggedIn", "X", "Y", "Z" };
            string after = string.Format("WHERE Id='{0}'", instance.ID);
            object[] args = { "0", instance.PlayerClient.position.X, instance.PlayerClient.position.Y, instance.PlayerClient.position.Z };
            db.compileQuery(before, after, varNames, args);
        }

        private void removePlayerVehicles(Player instance)
        {
            instance.removePlayerVehicles();
        }
    }
}
