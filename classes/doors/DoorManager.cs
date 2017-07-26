using Essence.classes.anticheat;
using Essence.classes.events;
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
                return;

            int count = 0;

            foreach (DataRow row in table.Rows)
            {
                DoorInfo door = new DoorInfo(row);
                doors.Add(door);
                EventManager.events.Add(new EventInfo($"{door.Id}", "DoorCalls", "EnterInterior"));
                API.shared.consoleOutput($"Door Loaded: {door.Id}");
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
                return;

            // Setup to find our target door.
            DoorInfo targetDoor = null;
            string doorID = arguments[0].ToString().Replace("Door-", string.Empty);
            // Find our target door.
            for (int i = 0; i < doors.Count; i++)
            {
                if (doors[i].Id.Replace("Door-", string.Empty) != doorID)
                    continue;

                targetDoor = doors[i];
                break;
            }
            // If our target door does not exist. Stop.
            if (targetDoor == null)
            {
                Console.WriteLine("[Door] Not a valid door. " + arguments[0].ToString());
                return;
            }
            // If our target door is locked, don't let the player in.
            if (targetDoor.Locked)
            {
                API.shared.triggerClientEvent(player, "HeadNotification", "~r~This door is locked.");
                return;
            }
            // Load the IPL if it hasn't been loaded today, then teleport them into the interior.
            loadIPL(targetDoor.IPL);
            AnticheatInfo info = player.getData("Anticheat");
            info.LastPosition = targetDoor.InteriorLocation;
            player.setData("LastPosition", player.position);
            player.position = targetDoor.InteriorLocation;
            player.dimension = targetDoor.CoreId;
        }

        public static void ExitDoor(Client player, params object[] arguments)
        {
            if (!player.hasData("LastPosition"))
                return;

            AnticheatInfo info = player.getData("Anticheat");
            Vector3 lastPos = player.getData("LastPosition");
            info.LastPosition = lastPos;
            player.position = lastPos;
            player.dimension = 0;

            player.resetData("LastPosition");
        }

        public static void ToggleDoor(Client player, params object[] arguments)
        {
            // Setup to find our target door.
            DoorInfo targetDoor = null;
            for (int i = 0; i < doors.Count; i++)
            {
                if (doors[i].DoorLocation.DistanceTo2D(player.position) > 5)
                    continue;

                targetDoor = doors[i];
                break;
            }
            // If our target door does not exist. Stop.
            if (targetDoor == null)
                return;

            if (!player.hasData("Instance"))
                return;

            Player instance = player.getData("Instance");

            if (instance.Id != targetDoor.OwnerId)
                return;

            if (targetDoor.Locked)
            {
                targetDoor.Locked = false;
                API.shared.triggerClientEvent(player, "HeadNotification", "~g~Door unlocked.");
            } else
            {
                targetDoor.Locked = true;
                API.shared.triggerClientEvent(player, "HeadNotification", "~r~Door locked.");
            }
        }

        private static void loadIPL(string ipl)
        {
            API.shared.requestIpl(ipl);
        }
    }
}
