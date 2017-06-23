using Essence.classes;
using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes
{
    public class Main : Script
    {
        public Main()
        {
            API.setGamemodeName("~g~By ~b~Stuyk");
            API.setServerName("~g~Essence ~b~Pure ~b~Roleplay");
        }

        [Command("car")]
        public void cmdCar(Client player, VehicleHash car)
        {
            if (API.hasEntityData(player, "LastCar"))
            {
                API.deleteEntity(API.getEntityData(player, "LastCar"));
            }

            NetHandle vehicle = API.createVehicle(car, player.position, new Vector3(), 0, 0);
            API.setEntityData(player, "LastCar", vehicle);
            API.setPlayerIntoVehicle(player, vehicle, -1);
        }

        [Command("randomClothes")]
        public void randomClothes(Client player)
        {
            Player client = API.getEntityData(player, "Instance");
            API.setPlayerSkin(player, PedHash.FreemodeMale01);
            client.PlayerClothing.Top = new Random().Next(0, 50);
            client.PlayerClothing.Legs = new Random().Next(0, 50);
            client.PlayerClothing.Feet = new Random().Next(0, 50);

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
                writer.WriteLine(string.Format("Rotation: new Vector3({0}, {1}, {2})", player.rotation.X, player.rotation.Y, player.rotation.Z));
            }

            API.sendChatMessageToPlayer(player, "Saved location as " + location);
        }

        [Command("testnote", GreedyArg = true)]
        public void cmdTestNote(Client player, string text)
        {
            API.triggerClientEvent(player, "HeadNotification", text);
        }
    }
}
