using Essence.classes;
using Essence.classes.discord;
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

        [Command("gun")]
        public void cmdGun(Client player, WeaponHash gun)
        {
            API.givePlayerWeapon(player, gun, 999, true, true);
        }

        [Command("tp")]
        public void cmdInvitePlayerToMission(Client player, string target)
        {
            Client targetPlayer = null;

            foreach (Client p in API.getAllPlayers())
            {
                if (p.name.Contains(target))
                {
                    targetPlayer = p;
                    break;
                }
            }

            if (targetPlayer == null)
            {
                API.triggerClientEvent(player, "Mission_Head_Notification", "~r~Player does not exist.", "Error");
                return;
            }

            if (targetPlayer == player)
            {
                API.triggerClientEvent(player, "Mission_Head_Notification", "~r~Stop trying to teleport to yourself you fuck.", "Error");
                return;
            }

            API.setEntityPosition(targetPlayer, player.position);
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

        [Command("saveCustomPos", "~w~/savePos <filename>")]
        public void cmdCustomSave(Client player, string filename)
        {
            Vector3 groundLevel = new Vector3(player.position.X, player.position.Y, player.position.Z - 1);
            using (StreamWriter writer = new StreamWriter("resources/Essence/data/" + filename + "positions.txt", true))
            {
                writer.WriteLine(string.Format("{0}, {1}, {2}", groundLevel.X, groundLevel.Y, groundLevel.Z));
            }

            using (StreamWriter writer = new StreamWriter("resources/Essence/data/" + filename + "rotations.txt", true))
            {
                writer.WriteLine(string.Format("{0}, {1}, {2}", player.rotation.X, player.rotation.Y, player.rotation.Z));
            }

            API.sendChatMessageToPlayer(player, "Saved location as " + filename);
        }
        
        [Command("chopshopTime")]
        public void gotjsdfas(Client player)
        {
            API.setEntityPosition(player, new Vector3(1569.829, -2130.04, 77.33018));
        }

        [Command("tryAnimation")]
        public void tryAnim(Client player, string dic, string target)
        {
            API.playPlayerAnimation(player, 0, dic, target);
            API.delay(5000, true, () =>
            {
                player.stopAnimation();
            });
        }

        [Command("gopos")]
        public void goPosCMD(Client player, double value1, double value2, double value3)
        {
            API.setEntityPosition(player, new Vector3(value1, value2, value3));
        }

        [Command("testnote", GreedyArg = true)]
        public void cmdTestNote(Client player, string text)
        {
            API.triggerClientEvent(player, "HeadNotification", text);
        }

        [Command("magic")]
        public void cmdClientWeather(Client player)
        {
            API.sendNativeToPlayer(player, (ulong)Hash._SET_WEATHER_TYPE_OVER_TIME, "RAIN", 25f);
        }
    }
}
