﻿using Essence.classes.anticheat;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.doors
{
    public static class DoorManager
    {
        public static Database db = new Database();
        private static List<DoorInfo> doors = new List<DoorInfo>();

        public static List<DoorInfo> Doors
        {
            get
            {
                return doors;
            }
        }

        public static void LoadAllDoors()
        {
            DataTable table = db.executeQueryWithResult("SELECT * FROM Doors");

            if (table.Rows.Count <= 0)
            {
                return;
            }

            int count = 0;

            foreach (DataRow row in table.Rows)
            {
                DoorInfo door = new DoorInfo(row);
                doors.Add(door);
                count++;
            }

            Console.WriteLine("[Doors] TOTAL COUNT: " + count.ToString());
        }

        /// <summary>
        /// Create a door from the Admin Commands.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="price"></param>
        public static void CreateDoor(Client player, int price)
        {
            // Setup registration.
            string[] varNamesTwo = { "X", "Y", "Z", "Locked", "Price"};
            string tableName = "Doors";
            string[] dataTwo = { player.position.X.ToString(), player.position.Y.ToString(), player.position.Z.ToString(), true.ToString(), price.ToString() };
            db.compileInsertQuery(tableName, varNamesTwo, dataTwo);
        }

        public static void EnterDoor(Client player, params object[] arguments)
        {
            if (arguments.Length <= 0)
            {
                return;
            }

            DoorInfo targetDoor = null;
            string doorID = arguments[0].ToString().Replace("Door-", string.Empty);

            for (int i = 0; i < doors.Count; i++)
            {
                if (doors[i].Id.Replace("Door-", string.Empty) == doorID)
                {
                    targetDoor = doors[i];
                    break;
                }
            }

            if (targetDoor == null)
            {
                Console.WriteLine("[Door] Not a valid door. " + arguments[0].ToString());
                return;
            }

            if (targetDoor.Locked)
            {
                API.shared.sendChatMessageToPlayer(player, "~r~This door is locked.");
                return;
            }

            loadIPL(targetDoor.IPL);
            AnticheatInfo info = player.getData("Anticheat");
            info.LastPosition = targetDoor.InteriorLocation;
            player.setData("LastPosition", player.position);
            player.position = targetDoor.InteriorLocation;


            API.shared.delay(10000, true, () =>
            {
                info.LastPosition = player.getData("LastPosition");
                player.position = player.getData("LastPosition");
            });
        }

        private static void loadIPL(string ipl)
        {
            API.shared.requestIpl(ipl);
        }
    }
}
