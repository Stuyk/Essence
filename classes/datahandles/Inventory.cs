using Essence.classes.utility;
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
        private int radio;
        private int phone;

        // Item Properties.
        private int radioFrequency;
        private int phoneNumber;
        

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

            //Load Item Properties
            loadItemProperties();
        }

        private void convertInventory(DataRow inv)
        {
            carParts = Convert.ToInt32(inv["CarParts"]);
            unrefinedDrugs = Convert.ToInt32(inv["UnrefinedDrugs"]);
            refinedDrugs = Convert.ToInt32(inv["RefinedDrugs"]);
            radio = Convert.ToInt32(inv["Radio"]);
        }

        public int getItemCountByType(string type)
        {
            switch (type)
            {
                case "CarParts":
                    return CarParts;
                case "UnrefinedDrugs":
                    return UnrefinedDrugs;
                case "RefinedDrugs":
                    return RefinedDrugs;
                case "Radio":
                    return Radio;
                case "Phone":
                    return Phone;
            }
            return -1;
        }

        //Item Properties Loading
        private void loadItemProperties()
        {
            string[] varNames = { "Owner" };
            string before = "SELECT * FROM ItemProperties WHERE";
            object[] data = { player.ID };
            DataTable result = db.compileSelectQuery(before, varNames, data);

            if (result.Rows.Count <= 0)
            {
                string[] names = { "Owner" };
                string[] invData = { player.ID.ToString() };
                db.compileInsertQuery("ItemProperties", names, invData);

                result = db.compileSelectQuery(before, varNames, data);
            }

            convertItemProperties(result.Rows[0]);
        }

        private void convertItemProperties(DataRow props)
        {
            radioFrequency = Convert.ToInt32(props["RadioFrequency"]);
            phoneNumber = Convert.ToInt32(props["PhoneNumber"]);
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

            if (Radio != 0)
            {
                API.triggerClientEvent(client, "Add_Inventory_Item", "Radio", Radio, false);
            }

            if (Radio != 0)
            {
                API.triggerClientEvent(client, "Add_Inventory_Item", "Phone", Phone, false);
            }
        }

        private void saveInventory(string item, int amount)
        {
            string target = "UPDATE Inventory SET";
            string where = string.Format("WHERE Owner='{0}'", player.ID);
            string[] variables = { item };
            object[] data = { amount };
            Payload.addNewPayload(target, where, variables, data);
        }

        private void saveItemProperties(string prop, string propData)
        {
            string target = "UPDATE ItemProperties SET";
            string where = string.Format("WHERE Owner='{0}'", player.ID);
            string[] variables = { prop };
            object[] data = { propData };
            Payload.addNewPayload(target, where, variables, data);
        }

        public void addBasedOnType(string type, int quantity)
        {
            switch (type)
            {
                case "CarParts":
                    CarParts += quantity;
                    saveInventory(type, CarParts);
                    return;
                case "UnrefinedDrugs":
                    UnrefinedDrugs += quantity;
                    saveInventory(type, UnrefinedDrugs);
                    return;
                case "RefinedDrugs":
                    RefinedDrugs += quantity;
                    saveInventory(type, RefinedDrugs);
                    return;
                case "Radio":
                    Radio += quantity;
                    saveInventory(type, Radio);
                    return;
                case "Phone":
                    Phone += quantity;
                    saveInventory(type, Phone);
                    return;
            }
        }
        // =====================================
        // GETTERS AND SETTERS FOR OUR INVENTORY ITEMS
        // ANYTHING YOU ADD HERE MUST BE ADDED INTO THE DATABASE TABLE AS WELL.
        // ALSO EDIT ITEMS.CS, INVENTORYITEM.CS, and INVENTORYMANAGER.CS
        // =====================================
        public int CarParts
        {
            set
            {
                carParts = value;
                saveInventory("CarParts", carParts);
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
                saveInventory("UnrefinedDrugs", unrefinedDrugs);
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
                saveInventory("RefinedDrugs", refinedDrugs);
            }
            get
            {
                return refinedDrugs;
            }
        }

        public int Radio
        {
            set
            {
                radio = value;
                saveInventory("Radio", radio);
            }
            get
            {
                return radio;
            }
        }

        public int Phone
        {
            set
            {
                phone = value;
                saveInventory("Phone", phone);
            }
            get
            {
                return phone;
            }
        }

        //Item Properties

        public int RadioFrequency
        {
            set
            {
                radioFrequency = value;
                saveItemProperties("RadioFrequency", radioFrequency.ToString());
            }
            get
            {
                return radioFrequency;
            }
        }

        public int PhoneNumber
        {
            set
            {
                phoneNumber = value;
                saveItemProperties("PhoneNumber", phoneNumber.ToString());
            }
            get
            {
                return phoneNumber;
            }
        }


    }
}
