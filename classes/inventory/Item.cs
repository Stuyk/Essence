using Essence.classes.utility;
using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.inventory
{
    public class Item
    {
        Database db = new Database();

        //Item ID as saved in DB
        private int id;

        private Client client;
        private Player player;

        private Items.ItemTypes type;
        private string data;
        private int quantity;


        public Item(Items.ItemTypes type, int quantity = 1, string data = "")
        {
            this.type = type;
            this.quantity = quantity;
            this.data = data;
        }

        public Item(Client client, Player player, int id, Items.ItemTypes type, int quantity = 1, string data = "")
        {
            this.player = player;
            this.client = client;
            this.id = id;
            this.type = type;
            this.quantity = quantity;
            this.data = data;
        }

        public void insertItem()
        {
            string[] variables = { "Owner", "ItemType", "Quantity", "Data" };
            string tableName = "Items";
            string[] data = { this.player.ID.ToString(), this.type.ToString(), this.quantity.ToString(), this.data };
            int primaryKey = db.compileInsertQueryReturnLastId(tableName, variables, data);

            this.id = primaryKey;
        }

        public void saveItem()
        {
            string target = "UPDATE Items SET";
            string where = string.Format("WHERE Id='{0}'", this.id);
            string[] variables = { "Owner", "ItemType", "Quantity", "Data" };
            object[] data = { this.player.ID, this.type, this.quantity, this.data };
            Payload.addNewPayload(target, where, variables, data);
        }

        public void deleteItem()
        {
            string query = string.Format("DELETE FROM items WHERE Id='{0}'", this.id);
            db.executeQuery(query);
        }

        //===================//
        // Getters & Setters
        //===================//
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public Client Client
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
            }
        }

        public Player Player
        {
            get
            {
                return player;
            }
            set
            {
                player = value;
            }
        }

        public Items.ItemTypes Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public string Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }

    }
}
