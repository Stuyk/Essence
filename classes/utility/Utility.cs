﻿using GTANetworkServer;
using GTANetworkShared;
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
    }
}
