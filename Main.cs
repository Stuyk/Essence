using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disorder_District
{
    public class Main : Script
    {
        public Main()
        {
            API.setGamemodeName("~r~Disorder ~b~District");
            API.onPlayerConnected += API_onPlayerConnected;
            API.onPlayerFinishedDownload += API_onPlayerFinishedDownload;
            API.onPlayerRespawn += API_onPlayerRespawn;
        }

        private void API_onPlayerRespawn(Client player)
        {
            API.setEntityPosition(player, new Vector3(822.1755, 1293.973, 365.0179));
            
        }

        private void API_onPlayerFinishedDownload(Client player)
        {
            //API.setPlayerHealth(player, -10);
        }

        private void API_onPlayerConnected(Client player)
        {
            API.sendChatMessageToPlayer(player, "Welcome, try these commands.");
            API.sendChatMessageToPlayer(player, "/createparty");
            API.sendChatMessageToPlayer(player, "/invite");
            API.sendChatMessageToPlayer(player, "/joinparty");
            API.sendChatMessageToPlayer(player, "/leaveparty");
            API.sendChatMessageToPlayer(player, "~y~Missions: ~w~/newtestmission, /point2point, /trailblazer");
            API.sendChatMessageToPlayer(player, "Report any issues to 'Stuyk' on Discord.");
        }

        [Command("savePos", "~w~/savePos <location name>")]
        public void cmdSavePosition(Client player, string location)
        {
            Vector3 groundLevel = new Vector3(player.position.X, player.position.Y, player.position.Z - 1);
            using (StreamWriter writer = new StreamWriter("SavedPositions.txt", true))
            {
                writer.WriteLine(string.Format("[{0}]", location));
                writer.WriteLine(string.Format("X: {0}", groundLevel.X));
                writer.WriteLine(string.Format("Y: {0}", groundLevel.Y));
                writer.WriteLine(string.Format("Z: {0}", groundLevel.Z));
                writer.WriteLine(string.Format("new Vector3({0}, {1}, {2})", groundLevel.X, groundLevel.Y, groundLevel.Z));
            }

            API.sendChatMessageToPlayer(player, "Saved location as " + location);
        }

        [Command("gimmecar")]
        public void cmdGimmeCar(Client player, VehicleHash type)
        {
            var vehicle = API.createVehicle(type, player.position, new Vector3(), 0, 0);
            API.setPlayerIntoVehicle(player, vehicle, -1);
        }

        [Command("gimmegun")]
        public void cmdGun(Client player, WeaponHash type)
        {
            API.givePlayerWeapon(player, type, 99999, true, true);
        }
    }
}
