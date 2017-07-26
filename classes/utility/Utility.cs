using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
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

namespace Essence.classes.utility
{
    public static class Utility
    {
        private static Database db = new Database();

        public static List<Vector3> pullLocationsFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            List<Vector3> list = new List<Vector3>();

            foreach(string line in lines)
            {
                string[] result = line.Split(',');
                Vector3 location = new Vector3(Convert.ToDouble(result[0]), Convert.ToDouble(result[1]), Convert.ToDouble(result[2]));
                list.Add(location);
            }

            return list;
        }

        public static List<Array> pullPricesFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            List<Array> Plist = new List<Array>();

            foreach (string line in lines)
            {
                string[] result = line.Split(',');
                int[,] prices = new int[, ] { { Convert.ToInt32(result[0]), Convert.ToInt32(result[1]), Convert.ToInt32(result[1]) } };
                Plist.Add(prices);
            }

            return Plist;
        }

        public static List<string> pullTypesFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            List<string> list = new List<string>();

            foreach (string line in lines)
            {
                list.Add(line);
            }

            return list;
        }

        public static SpawnInfo findOpenSpawn(List<SpawnInfo> spawns, Mission missionInstance)
        {
            foreach (SpawnInfo spawn in spawns)
            {
                if (!spawn.Occupied)
                {
                    spawn.Occupied = true;
                    return spawn;
                }
            }
            return null;
        }

        public static bool isPlayerAdmin(Client player)
        {
            if (!player.hasData("Instance"))
            {
                return false;
            }

            Player instance = player.getData("Instance");

            if (instance.IsAdmin)
            {
                return true;
            } else {
                return false;
            }
        }

        public static void setupTableForPlayer(string id, string tableName)
        {
            string[] varNames = { "Owner" };
            string[] data = { id };
            db.compileInsertQuery(tableName, varNames, data);
        }
    }
}
