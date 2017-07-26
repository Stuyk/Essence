using Essence.classes.utility;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.inventory
{
    public class Item
    {
        private static Database db = new Database();
        public int Id { get; set; }
        public Client PlayerClient { get; set; }
        public Player PlayerClass { get; set; }
        public Items.ItemTypes Type { get; set; }
        public string Data { get; set; }

        private int quantity;
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
                if (quantity <= 0)
                {
                    deleteItem();
                }
            }
        }

        //Create Item without Owner (Overworld Spawning)
        public Item(Items.ItemTypes type, int quantity = 1, string data = "")
        {
            this.Type = type;
            this.quantity = quantity;
            this.Data = data;
        }

        //Create Item with Owner (Inside Player Inventory)
        public Item(Client client, Player player, int id, Items.ItemTypes type, int quantity = 1, string data = "", bool save = false)
        {
            this.PlayerClass = player;
            this.PlayerClient = client;
            this.Id = id;
            this.Type = type;
            this.quantity = quantity;
            this.Data = data;

            if (save)
                insertItem();
        }

        public void insertItem()
        {
            string[] variables = { "Owner", "ItemType", "Quantity", "Data" };
            string tableName = "Items";
            object[] data = { this.PlayerClass.Id, (int)this.Type, this.quantity, this.Data };
            int primaryKey = db.compileInsertQueryReturnLastId(tableName, variables, data);

            this.Id = primaryKey;
        }

        public void saveItem()
        {
            API.shared.consoleOutput("Saving Item: " + this.Type.ToString() + " [" + this.quantity + "]");
            string target = "UPDATE Items SET";
            string where = string.Format("WHERE Id='{0}'", this.Id);
            string[] variables = { "Owner", "ItemType", "Quantity", "Data" };
            object[] data = { this.PlayerClass.Id, (int)this.Type, this.quantity, this.Data };
            Payload.addNewPayload(target, where, variables, data);
        }

        public void deleteItem()
        {
            string query = string.Format("DELETE FROM items WHERE Id='{0}'", this.Id);
            db.executeQuery(query);
        }
    }
}
