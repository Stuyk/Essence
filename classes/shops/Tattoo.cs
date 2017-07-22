using Essence.classes.utility;
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

namespace Essence.classes
{
    class TattooInfo : Script
    {
        private Vector3 location;

        public TattooInfo() { }

        public TattooInfo(Vector3 loc)
        {
            location = loc;
            generatePoint();
        }

        private void generatePoint()
        {
            PointInfo point = PointHelper.addNewPoint();
            point.Position = location;
            point.BlipColor = 3;
            point.BlipType = 75;
            point.Text = "Tattoo Shop";
            point.DrawLabel = true;
            point.ID = "SHOP_TATTOO";
            point.InteractionEnabled = true;
        }

        public Vector3 Location
        {
            set
            {
                location = value;
            }
            get
            {
                return location;
            }
        }
    }

    class Tattoo : Script
    {
        private List<TattooInfo> tattooInfos;
        private List<Vector3> locations;

        /** Returns a list of Tattoos. */
        public List<TattooInfo> Tattoos
        {
            get
            {
                return tattooInfos;
            }
        }

        public Tattoo()
        {
            locations = new List<Vector3>();
            tattooInfos = new List<TattooInfo>();
            API.onResourceStart += API_onResourceStart;
        }

        private void API_onResourceStart()
        {
            loadLocations();
            setupLocations();
        }

        private void loadLocations()
        {
            locations = Utility.pullLocationsFromFile("resources/Essence/data/tattoos.txt");
        }

        private void setupLocations()
        {
            int count = 0;

            foreach (Vector3 location in locations)
            {
                TattooInfo tattoo = new TattooInfo(location);
                tattooInfos.Add(tattoo);
                count++;
            }

            API.consoleOutput(string.Format("Added {0} Tattoo locations.", count));
        }

        public void startTattooShop(Client player, params object[] arguments)
        {
            API.sendChatMessageToPlayer(player, "~b~Essence: ~r~We're Sorry! The tattoo shops are under contruction.");
        }

    }
}
