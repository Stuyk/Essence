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
        private Database db = new Database();

        public int Id { get; set; }
        public int Bank { get; set; }
        public Vector3 LastPosition { get; set; }
        public Client PlayerClient { get; set; }
        public Clothing PlayerClothing { get; set; }
        public Skin PlayerSkin { get; set; }
        public Inventory PlayerInventory { get; set; }
        public AnticheatInfo AnticheatInfo { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public bool IsAdmin { get; set; }

        private int money;
        public int Money
        {
            set
            {
                this.money = value;
                API.setEntitySyncedData(PlayerClient, "ESS_Money", Money);
                updatePlayerMoney();
            }
            get { return this.money; }
        }

        public Player() { }

        public Player(Client player, DataRow db)
        {
            // Used for drawing hud items -->
            API.setEntitySyncedData(player, "ESS_LoggedIn", true);
            // Setup Class Data.
            API.setEntityData(player, "Instance", this);
            PlayerClient = player;
            Id = Convert.ToInt32(db["ID"]);
            Bank = Convert.ToInt32(db["Bank"]);
            Money = Convert.ToInt32(db["Money"]);
            player.health = Convert.ToInt32(db["Health"]);
            player.armor = Convert.ToInt32(db["Armor"]);
            player.name = $"{db["Name"]}";
            IsAdmin = Convert.ToBoolean(db["IsAdmin"]);
            API.setEntitySyncedData(PlayerClient, "ESS_Money", Money);
            LastPosition = new Vector3(Convert.ToSingle(db["X"]), Convert.ToSingle(db["Y"]), Convert.ToSingle(db["Z"]));
            // Don't move on until the players dimension is ready.

            // Make our player controllable again.
            API.freezePlayer(player, false);
            API.setEntityPosition(player, LastPosition);

            // Setup Player Vehicles
            createPlayerVehicles();

            // Setup Player Skin
            PlayerSkin = new Skin(player, this);
            PlayerSkin.loadPlayerFace();

            // Setup Player Clothing
            PlayerClothing = new Clothing(player, this);
            PlayerClothing.loadPlayerClothes();
            PlayerClothing.updatePlayerClothes();

            // Setup Player Inventory
            PlayerInventory = new Inventory(player, this);
            PlayerInventory.LoadInventory();

            // Set our entity dimension.
            API.setEntityDimension(player, 0);

            // Setup our anticheat info.
            AnticheatInfo = Anticheat.addPlayer(player);

            // Login Console
            // Pass the login to the console.
            API.consoleOutput($"{player.name} has logged in.");
        }

        /** Spawn Player Vehicles */
        public void createPlayerVehicles()
        {
            Vehicles = new List<Vehicle>();

            string[] varNames = { "Owner" };
            string before = "SELECT * FROM Vehicles WHERE";
            object[] data = { Id };
            DataTable result = db.compileSelectQuery(before, varNames, data);

            if (result.Rows.Count < 1)
                return;

            foreach (DataRow row in result.Rows)
            {
                Vehicle vehInfo = new Vehicle(row);
                Vehicles.Add(vehInfo);
            }
        }

        /** Remove Player Vehicles **/
        public void removePlayerVehicles()
        {
            if (Vehicles.Count <= 0)
                return;

            foreach (Vehicle vehInfo in Vehicles)
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
            string where = string.Format("WHERE Id='{0}'", Id);
            string[] variables = { "Money", "Bank" };
            object[] data = { Money, Bank };
            Payload.addNewPayload(target, where, variables, data);
        }

        public void updatePlayerPosition()
        {
            string target = "UPDATE Players SET";
            string where = string.Format("WHERE Id='{0}'", Id);
            string[] variables = { "LoggedIn", "X", "Y", "Z" };
            object[] data = { "0", PlayerClient.position.X, PlayerClient.position.Y, PlayerClient.position.Z };
            Payload.addNewPayload(target, where, variables, data);
        }

    }
}
