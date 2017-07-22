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

namespace Essence.classes.shops
{
    public class DoorInfo
    {
        int model;
        Vector3 position;

        public DoorInfo(int mod, Vector3 pos)
        {
            model = mod;
            position = pos;
        }

        public int Model
        {
            get
            {
                return model;
            }
        }

        public Vector3 Position
        {
            get
            {
                return position;
            }
        }
    }

    public class Doors : Script
    {
        List<DoorInfo> doors;

        public Doors()
        {
            API.onResourceStart += API_onResourceStart;
            API.onPlayerFinishedDownload += API_onPlayerFinishedDownload;
        }

        private void API_onResourceStart()
        {
            int count = 0;

            string[] lines = File.ReadAllLines("resources/Essence/data/doors.txt");
            doors = new List<DoorInfo>();

            foreach (string line in lines)
            {
                string[] result = line.Split(',');

                int id = Convert.ToInt32(result[0]);
                Vector3 location = new Vector3(Convert.ToDouble(result[1]), Convert.ToDouble(result[2]), Convert.ToDouble(result[3]));
                doors.Add(new DoorInfo(id, location));
                count++;
            }

            API.consoleOutput("Opened " + count + " doors.");
        }

        private void API_onPlayerFinishedDownload(Client player)
        {
            OpenAllTheDoors(player);
        }

        private void OpenAllTheDoors(Client player)
        {
            foreach (DoorInfo door in doors)
            {
                API.triggerClientEvent(player, "RetrieveDoor", door.Model, door.Position);
            }
        }
    }
}
