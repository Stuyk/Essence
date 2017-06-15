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
    class AtmInfo : Script
    {
        private Vector3 location;
        private Blip blip;

        public AtmInfo()
        {
            location = null;
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

        public Blip BlipMarker
        {
            set
            {
                blip = value;
            }
            get
            {
                return blip;
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
            var lines = File.ReadAllLines("resources/Essence/data/atms.txt");
            foreach (var line in lines)
            {
                string[] result = line.Split(',');
                Vector3 newVector = new Vector3(Convert.ToSingle(result[0]), Convert.ToSingle(result[1]), Convert.ToSingle(result[2]));
                locations.Add(newVector);
                
            }
        }

        private void setupLocations()
        {
            int count = 0;

            foreach (Vector3 location in locations)
            {
                AtmInfo atm = new AtmInfo();
                atmInfos.Add(atm);
                atm.Location = location;
                atm.BlipMarker = API.createBlip(location);
                // Blip Info
                API.setBlipShortRange(atm.BlipMarker, true);
                API.setBlipName(atm.BlipMarker, "Automated Teller Machine");
                API.setBlipSprite(atm.BlipMarker, 431);
                API.setBlipColor(atm.BlipMarker, 2);

                count++;
            }

            API.consoleOutput(string.Format("Added {0} ATM locations.", count));
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
