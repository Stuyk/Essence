using Essence.classes.utility;
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
    class BarberInfo : Script
    {
        private Vector3 location;

        public BarberInfo() { }

        public BarberInfo(Vector3 loc)
        {
            location = loc;
            generatePoint();
        }

        private void generatePoint()
        {
            PointInfo point = PointHelper.addNewPoint();
            point.Position = location;
            point.BlipColor = 3;
            point.BlipType = 71;
            point.Text = "Barber";
            point.DrawLabel = true;
            point.ID = "SHOP_BARBER";
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

    class Barber : Script
    {
        private List<BarberInfo> barberInfos;
        private List<Vector3> locations;

        /** Returns a list of Barbers. */
        public List<BarberInfo> Barbers
        {
            get
            {
                return barberInfos;
            }
        }

        public Barber()
        {
            locations = new List<Vector3>();
            barberInfos = new List<BarberInfo>();
            API.onResourceStart += API_onResourceStart;
        }

        private void API_onResourceStart()
        {
            loadLocations();
            setupLocations();
        }

        private void loadLocations()
        {
            locations = Utility.pullLocationsFromFile("resources/Essence/data/barbers.txt");
        }

        private void setupLocations()
        {
            int count = 0;

            foreach (Vector3 location in locations)
            {
                BarberInfo barber = new BarberInfo(location);
                barberInfos.Add(barber);
                count++;
            }

            API.consoleOutput(string.Format("Added {0} Barber locations.", count));
        }

        public void startBarberShop(Client player)
        {
            API.sendChatMessageToPlayer(player, "~b~Essence: ~r~Fire Barber shop code!.");
        }

    }
}
