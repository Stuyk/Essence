using Essence.classes.anticheat;
using Essence.classes.doors;
using Essence.classes.utility;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.administration
{
    public class Commands : Script
    {
        public Commands()
        {
            // Nothing
        }

        [Command("ahelp")]
        public void aCMD_AHelp(Client player)
        {
            if (!Utility.isPlayerAdmin(player))
            {
                return;
            }

            API.sendChatMessageToPlayer(player, string.Format("/atp, /akick, /aban, /acar, /adoor, /agun, /apos, /aheal, /aarmor, /saveCustom, /savePOS, /aHurt, /aKill, /adoor"));
        }

        [Command("atp")]
        public void aCMD_Teleport(Client player, string target)
        {
            if (!Utility.isPlayerAdmin(player))
            {
                return;
            }

            foreach (Client p in API.getAllPlayers())
            {
                if (p.name.ToLower().Contains(target.ToLower()))
                {
                    API.setEntityPosition(player, p.position);
                    return;
                }
            }
            API.sendChatMessageToPlayer(player, $"{target} does not exist.");
        }

        [Command("aKick")]
        public void aCMD_Kick(Client player, string target)
        {
            if (!Utility.isPlayerAdmin(player))
            {
                return;
            }

            foreach (Client p in API.getAllPlayers())
            {
                if (p.name.ToLower() == target.ToLower())
                {
                    p.kick();
                    return;
                }
            }
        }

        [Command("aBan")]
        public void aCMD_Ban(Client player, string target, string reason)
        {
            if (!Utility.isPlayerAdmin(player))
            {
                return;
            }

            foreach (Client p in API.getAllPlayers())
            {
                if (p.name.ToLower() == target.ToLower())
                {
                    p.ban(reason);
                    API.consoleOutput($"Banned {target} for {reason} by {player.name}");
                    return;
                }
            }
        }

        [Command("aCar")]
        public void cmdCar(Client player, VehicleHash car)
        {
            if (!Utility.isPlayerAdmin(player))
            {
                return;
            }

            if (API.hasEntityData(player, "LastCar"))
            {
                API.deleteEntity(API.getEntityData(player, "LastCar"));
            }

            NetHandle vehicle = API.createVehicle(car, player.position, new Vector3(), 0, 0);
            API.setEntityData(player, "LastCar", vehicle);
            API.setPlayerIntoVehicle(player, vehicle, -1);
        }

        [Command("aGun")]
        public void cmdGun(Client player, WeaponHash gun)
        {
            if (!Utility.isPlayerAdmin(player))
            {
                return;
            }

            API.givePlayerWeapon(player, gun, 999, true, true);
        }

        [Command("aPos")]
        public void aCMD_Pos(Client player, double value1, double value2, double value3)
        {
            if (!Utility.isPlayerAdmin(player))
            {
                return;
            }

            API.setEntityPosition(player, new Vector3(value1, value2, value3));
        }

        [Command("aHeal")]
        public void aCMD_Health(Client player)
        {
            if (!Utility.isPlayerAdmin(player))
            {
                return;
            }

            AnticheatInfo info = player.getData("Anticheat");
            info.HealthChangedRecently = true;
            player.health = 100;
        }

        [Command("aArmor")]
        public void aCMD_Armor(Client player)
        {
            if (!Utility.isPlayerAdmin(player))
            {
                return;
            }

            AnticheatInfo info = player.getData("Anticheat");
            info.ArmorChangedRecently = true;
            player.armor = 100;
        }

        [Command("aHurt")]
        public void aCMD_Hurt(Client player)
        {
            if (!Utility.isPlayerAdmin(player))
            {
                return;
            }

            player.health -= 10;
        }

        [Command("aKill")]
        public void aCMD_Kill(Client player)
        {
            if (!Utility.isPlayerAdmin(player))
            {
                return;
            }

            player.health = -1;
        }

        [Command("aDoor")]
        public void aCMD_CreateDoor(Client player, int price = 50000)
        {
            if (!Utility.isPlayerAdmin(player))
            {
                return;
            }

            if (price <= 50000)
            {
                API.consoleOutput($"{price} is too low!");
                return;
            }

            DoorManager.CreateDoor(player, price);
        }


        [Command("saveCustom", "~w~/savePos <filename>")]
        public void cmdCustomSave(Client player, string filename)
        {
            if (!Utility.isPlayerAdmin(player))
            {
                return;
            }

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

        [Command("savePos", "~w~/savePos <location name>")]
        public void cmdSavePosition(Client player, string location)
        {
            if (!Utility.isPlayerAdmin(player))
            {
                return;
            }

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
    }
}
