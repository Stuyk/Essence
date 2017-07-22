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

        public void startBarberShop(Client player, params object[] arguments)
        {
            API.triggerClientEvent(player, "OPEN_BARBER_MENU");
        }

        public void PurchaseBarber(Client player, params object[] arguments)
        {
            if (!API.hasEntityData(player, "Instance"))
            {
                return;
            }
            //take money
            Player instance = (Player)API.getEntityData(player, "Instance");
            instance.Money -= 200;

            //save Hairs
            int Hurr = Convert.ToInt32(arguments[1]);
            int HurrDye = Convert.ToInt32(arguments[2]);
            int HurrDye2 = Convert.ToInt32(arguments[3]);
            instance.PlayerSkin.Hair = Hurr;
            instance.PlayerSkin.HairColor = HurrDye;
            instance.PlayerSkin.HairHighlight = HurrDye2;
            instance.PlayerSkin.savePlayerFace();

        }

    }
}
