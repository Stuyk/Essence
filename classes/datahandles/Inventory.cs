using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.datahandles
{
    public class Inventory : Script
    {
        Database db = new Database();

        private Client client;
        private Player player;
        // Types of items.
        private int carParts;
        private int unrefinedDrugs;
        private int refinedDrugs;

        public Inventory() {}

        public Inventory(Client client, Player player)
        {
            this.player = player;
            this.client = client;
        }

        /// <summary>
        /// This is where we load everything from SQLite for the inventory system.
        /// </summary>
        public void LoadInventory()
        {
            string[] varNames = { "Owner" };
            string before = "SELECT * FROM Inventory WHERE";
            object[] data = { player.ID };
            DataTable result = db.compileSelectQuery(before, varNames, data);

            if (result.Rows.Count <= 0)
            {
                string[] names = { "Owner" };
                string[] invData = { player.ID.ToString() };
                db.compileInsertQuery("Inventory", names, invData);

                result = db.compileSelectQuery(before, varNames, data);
            }

            convertInventory(result.Rows[0]);
        }

        private void convertInventory(DataRow inv)
        {
            CarParts = Convert.ToInt32(inv["CarParts"]);
            UnrefinedDrugs = Convert.ToInt32(inv["UnrefinedDrugs"]);
            RefinedDrugs = Convert.ToInt32(inv["RefinedDrugs"]);

            Console.WriteLine(String.Format("Inventory: {0}, {1}, {2}", CarParts, UnrefinedDrugs, RefinedDrugs));
        }

        /// <summary>
        /// Loads items based on quantity. *** Type / Quantity ***
        /// </summary>
        public void LoadItemsToLocal()
        {
            if (CarParts != 0)
            {
                // Type of Item, Amount of Item, is It Consumeable?
                API.triggerClientEvent(client, "Add_Inventory_Item", "CarParts", CarParts, false);
            }

            if (UnrefinedDrugs != 0)
            {
                API.triggerClientEvent(client, "Add_Inventory_Item", "UnrefinedDrugs", UnrefinedDrugs, false);
            }
            
            if (RefinedDrugs != 0)
            {
                API.triggerClientEvent(client, "Add_Inventory_Item", "RefinedDrugs", RefinedDrugs, true);
            }
        }

        public void addBasedOnType(string type, int quantity)
        {
            switch (type)
            {
                case "CarParts":
                    CarParts += quantity;
                    return;
                case "UnrefinedDrugs":
                    UnrefinedDrugs += quantity;
                    return;
                case "RefinedDrugs":
                    RefinedDrugs += quantity;
                    return;
            }
        }

        public int CarParts
        {
            set
            {
                carParts = value;
            }
            get
            {
                return carParts;
            }
        }

        public int UnrefinedDrugs
        {
            set
            {
                unrefinedDrugs = value;
            }
            get
            {
                return unrefinedDrugs;
            }
        }

        public int RefinedDrugs
        {
            set
            {
                refinedDrugs = value;
            }
            get
            {
                return refinedDrugs;
            }
        }


    }
}
