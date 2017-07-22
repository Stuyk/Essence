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
    class AtmInfo : Script
    {
        private Vector3 location;

        public AtmInfo() { }

        public AtmInfo(Vector3 loc)
        {
            location = loc;
            generatePoint();
        }

        private void generatePoint()
        {
            PointInfo point = PointHelper.addNewPoint();
            point.Position = location;
            point.BlipColor = 2;
            point.BlipType = 431;
            point.Text = "ATM";
            point.DrawLabel = true;
            point.ID = "SHOP_ATM";
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

    class Atm : Script
    {
        private List<AtmInfo> atmInfos;
        private List<Vector3> locations;

        /** Returns a list of ATMs. */
        public List<AtmInfo> Atms
        {
            get
            {
                return atmInfos;
            }
        }

        public Atm()
        {
            locations = new List<Vector3>();
            atmInfos = new List<AtmInfo>();
            API.onResourceStart += API_onResourceStart;
        }

        private void API_onResourceStart()
        {
            loadLocations();
            setupLocations();
        }

        private void loadLocations()
        {
            locations = Utility.pullLocationsFromFile("resources/Essence/data/atms.txt");
        }

        private void setupLocations()
        {
            int count = 0;

            foreach (Vector3 location in locations)
            {
                AtmInfo atm = new AtmInfo(location);
                atmInfos.Add(atm);
                count++;
            }

            API.consoleOutput(string.Format("Added {0} ATM locations.", count));
        }

        public void loadATM(Client player, params object[] arguments)
        {
            API.sendChatMessageToPlayer(player, "This hasn't been implemented yet.");
        }

        [Command("deposit", Description = "/deposit [amount]")]
        public void depositMoney(Client player, int amount)
        {
            bool closeEnough = false;
            for (int i = 0; i < atmInfos.Count; i++)
            {
                if (atmInfos[i].Location.DistanceTo(player.position) <= 2)
                {
                    closeEnough = true;
                    break;
                }
            }

            if (!closeEnough)
            {
                API.sendChatMessageToPlayer(player, "~b~Essence: ~r~You don't seem to be near any atms.");
                return;
            }

            // Check for +/- Contains
            if (amount.ToString().Contains("+") || amount.ToString().Contains("-") || amount <= 0)
            {
                API.sendChatMessageToPlayer(player, "~b~ATM: ~r~That is not a valid amount.");
                return;
            }

            Player instance = (Player)API.getEntityData(player, "Instance");

            if (instance.Money < amount)
            {
                API.sendChatMessageToPlayer(player, "~b~ATM: ~r~You do not have that much money to deposit.");
                return;
            }

            instance.depositMoney(amount);
            API.sendChatMessageToPlayer(player, string.Format("|| Money: ${0} || Bank: ${1}", instance.Money, instance.Bank));
        }

        [Command("withdraw", Description = "/withdraw [amount]")]
        public void withdrawMoney(Client player, int amount)
        {
            bool closeEnough = false;
            for (int i = 0; i < atmInfos.Count; i++)
            {
                if (atmInfos[i].Location.DistanceTo(player.position) <= 2)
                {
                    closeEnough = true;
                    break;
                }
            }

            if (!closeEnough)
            {
                API.sendChatMessageToPlayer(player, "~b~Essence: ~r~You don't seem to be near any atms.");
                return;
            }

            if (amount.ToString().Contains("+") || amount.ToString().Contains("-") || amount <= 0)
            {
                API.sendChatMessageToPlayer(player, "~b~ATM: ~r~That is not a valid amount.");
                return;
            }

            Player instance = (Player)API.getEntityData(player, "Instance");

            if (instance.Bank < amount)
            {
                API.sendChatMessageToPlayer(player, "~b~ATM: ~r~You do not have that much money to withdraw.");
                return;
            }

            instance.withdrawMoney(amount);
            API.sendChatMessageToPlayer(player, string.Format("|| Money: ${0} || Bank: ${1}", instance.Money, instance.Bank));
        }
    }
}
