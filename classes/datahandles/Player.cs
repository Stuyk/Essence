using Essence.classes.anticheat;
using Essence.classes.datahandles;
using Essence.classes.utility;
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
using System.Threading;
using System.Threading.Tasks;

namespace Essence.classes
{
    public class Player : Script
    {
        Database db = new Database();

        private int id;
        private int money;
        private int bank;
        private Vector3 lastPosition;
        private Client player;
        private Clothing playerClothing;
        private Skin playerSkin;
        private Inventory playerInventory;
        private AnticheatInfo anticheatInfo;
        private List<Vehicle> vehicles;
        public bool IsAdmin { get; set; }

        // Return Player Vehicles
        public List<Vehicle> PlayerVehicles
        {
            get
            {
                return vehicles;
            }
        }

        public Inventory PlayerInventory
        {
            get
            {
                return playerInventory;
            }
        }

        public AnticheatInfo CheatInfo
        {
            get
            {
                return anticheatInfo;
            }
            set
            {
                anticheatInfo = value;
            }
        }

        // Active player client.
        public Client PlayerClient
        {
            set
            {
                player = value;
            }
            get
            {
                return player;
            }
        }

        //Player ID Number from Database
        public int ID
        {
            set
            {
                id = value;
            }
            get
            {
                return id;
            }
        }

        //Player Money Number from Database
        public int Money
        {
            set
            {
                money = value;
                API.setEntitySyncedData(PlayerClient, "ESS_Money", money);
                updatePlayerMoney();
            }
            get
            {
                return money;
            }
        }

        //Player Bank Number from Database
        public int Bank
        {
            set
            {
                bank = value;
            }
            get
            {
                return bank;
            }
        }

        //Player Last Position from Database
        public Vector3 LastPosition
        {
            set
            {
                lastPosition = value;
            }
            get
            {
                return lastPosition;
            }
        }

        public Clothing PlayerClothing
        {
            get
            {
                return playerClothing;
            }
        }

        public Skin PlayerSkin
        {
            get
            {
                return playerSkin;
            }
        }

        public Player() { }

        public Player(Client player, DataRow db)
        {
            // Used for drawing hud items -->
            API.setEntitySyncedData(player, "ESS_LoggedIn", true);
            // Setup Class Data.
            API.setEntityData(player, "Instance", this);
            PlayerClient = player;
            ID = Convert.ToInt32(db["ID"]);
            bank = Convert.ToInt32(db["Bank"]);
            money = Convert.ToInt32(db["Money"]);
            player.health = Convert.ToInt32(db["Health"]);
            player.armor = Convert.ToInt32(db["Armor"]);
            player.name = $"{db["Name"]}";
            IsAdmin = Convert.ToBoolean(db["IsAdmin"]);
            API.setEntitySyncedData(PlayerClient, "ESS_Money", money);
            lastPosition = new Vector3(Convert.ToSingle(db["X"]), Convert.ToSingle(db["Y"]), Convert.ToSingle(db["Z"]));
            // Don't move on until the players dimension is ready.

            // Make our player controllable again.
            API.freezePlayer(player, false);
            API.setEntityPosition(player, LastPosition);

            // Setup Player Vehicles
            createPlayerVehicles();

            // Setup Player Skin
            playerSkin = new Skin(player, this);
            playerSkin.loadPlayerFace();

            // Setup Player Clothing
            playerClothing = new Clothing(player, this);
            playerClothing.loadPlayerClothes();
            playerClothing.updatePlayerClothes();

            // Setup Player Inventory
            playerInventory = new Inventory(player, this);
            playerInventory.LoadInventory();

            // Set our entity dimension.
            API.setEntityDimension(player, 0);

            // Setup our anticheat info.
            anticheatInfo = Anticheat.addPlayer(player);

            // Login Console
            // Pass the login to the console.
            API.consoleOutput($"{player.name} has logged in.");
        }

        /** Spawn Player Vehicles */
        public void createPlayerVehicles()
        {
            vehicles = new List<Vehicle>();

            string[] varNames = { "Owner" };
            string before = "SELECT * FROM Vehicles WHERE";
            object[] data = { ID };
            DataTable result = db.compileSelectQuery(before, varNames, data);

            if (result.Rows.Count < 1)
            {
                return;
            }

            foreach (DataRow row in result.Rows)
            {
                Vehicle vehInfo = new Vehicle(row);
                vehicles.Add(vehInfo);
            }
        }

        /** Remove Player Vehicles **/
        public void removePlayerVehicles()
        {
            if (vehicles.Count <= 0)
            {
                return;
            }

            foreach (Vehicle vehInfo in vehicles)
            {
                // Update vehicle position before deleting.
                vehInfo.LastPosition = API.getEntityPosition(vehInfo.Handle);
                // Delete the entity.
                API.deleteEntity(vehInfo.Handle);
            }
        }

        /** Deposit money into the player's bank. */
        public void depositMoney(int amount)
        {
            Bank += amount;
            Money -= amount;

            updatePlayerMoney();
        }

        /** Withdraw money from the player's bank. */
        public void withdrawMoney(int amount)
        {
            Bank -= amount;
            Money += amount;

            updatePlayerMoney();
        }

        // Generally how you save database information.
        private void updatePlayerMoney()
        {
            string target = "UPDATE Players SET";
            string where = string.Format("WHERE Id='{0}'", ID);
            string[] variables = { "Money", "Bank" };
            object[] data = { Money, Bank };
            Payload.addNewPayload(target, where, variables, data);
        }

        public void updatePlayerPosition()
        {
            string target = "UPDATE Players SET";
            string where = string.Format("WHERE Id='{0}'", id);
            string[] variables = { "LoggedIn", "X", "Y", "Z" };
            object[] data = { "0", player.position.X, player.position.Y, player.position.Z };
            Payload.addNewPayload(target, where, variables, data);
        }

    }
}
