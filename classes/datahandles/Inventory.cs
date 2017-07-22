using Essence.classes.utility;
using Essence.classes.inventory;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.datahandles
{
    public class Inventory
    {
        Database db = new Database();

        private Client client;
        private Player player;

        //List of all the players items
        private List<Item> items = new List<Item>();
        public List<Item> CurrentItems
        {
            get
            {
                return items;
            }
        }

        public Inventory(Client client, Player player)
        {
            this.player = player;
            this.client = client;
        }

        // Load Inventory Items from DB
        public void LoadInventory()
        {
            string[] varNames = { "Owner" };
            string before = "SELECT * FROM Items WHERE";
            object[] data = { player.ID };
            DataTable result = db.compileSelectQuery(before, varNames, data);

            foreach(DataRow row in result.Rows)
            {
                bool addedToInventory = false;

                int itemId = Convert.ToInt32(row["Id"]);
                Items.ItemTypes ItemType = (Items.ItemTypes)Enum.Parse(typeof(Items.ItemTypes), row["ItemType"].ToString());
                int ItemQuantity = Convert.ToInt32(row["Quantity"]);
                string ItemData = row["Data"].ToString();

                API.shared.consoleOutput("Loading Item for " + API.shared.getPlayerName(client) + ": " + ItemType.ToString() + " [" + ItemQuantity.ToString() + "]");

                //Check if item already in inventory and stackable (if empty data string item is stackable)
                foreach(Item i in this.items)
                {
                    if(i.Type == ItemType)
                    {
                        if (i.Data.Length <= 0)
                        {
                            //Stack the item
                            i.Quantity += 1;
                            addedToInventory = true;
                            break;
                        }
                        else
                        {
                            Item item = new Item(this.client, this.player, itemId, ItemType, ItemQuantity, ItemData);
                            items.Add(item);
                            addedToInventory = true;
                            break;
                        }
                    }
                    else
                    {
                        Item item = new Item(this.client, this.player, itemId, ItemType, ItemQuantity, ItemData);
                        items.Add(item);
                        addedToInventory = true;
                        break;
                    } 
                }

                //Insert Item if not yet added
                if (!addedToInventory)
                {
                    Item item = new Item(this.client, this.player, itemId, ItemType, ItemQuantity, ItemData);
                    items.Add(item);
                    addedToInventory = true;
                }
            }
        }

        public void LoadItemsToLocal()
        {
            API.shared.consoleOutput("Loading items to client. " + this.items.Count.ToString());

            foreach(Item i in this.items)
            {
                //If item is consumable (index 400 or below in ItemTypes Enum)
                bool consumable = false;
                if((int)i.Type <= 400)
                {
                    consumable = true;
                }

                API.shared.consoleOutput("Item added clientside: " + i.Type.ToString());

                // Type of Item, Amount of Item, is It Consumeable?
                API.shared.triggerClientEvent(client, "Add_Inventory_Item", i.ID, i.Type.ToString("g"), i.Quantity, consumable);
            }
        }

        private void saveInventory(string item, int amount)
        {
            foreach(Item i in items)
            {
                i.saveItem();
            }
        }

        public void addItem(Item item)
        {
            //Check if item already in inventory and stackable (if empty data string item is stackable)
            foreach (Item i in this.items)
            {
                if (i.Type == item.Type)
                {
                    if (i.Data.Length <= 0)
                    {
                        //Stack the item
                        i.Quantity += item.Quantity;

                        //Delete old item
                        item.deleteItem(); 
                        break;
                    }
                    else
                    {
                        items.Add(item);
                        break;
                    }
                }
                else
                {
                    items.Add(item);
                    break;
                }
            }
        }
    }
}
