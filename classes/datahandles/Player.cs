using GTANetworkServer;
using GTANetworkShared;
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

        public Player() { }

        public Player(Client player, DataRow db)
        {
            // Pass the login to the console.
            API.consoleOutput(string.Format("{0} has successfully logged in.", player.name));
            // Setup Class Data.
            API.setEntityData(player, "Instance", this);
            PlayerClient = player;
            ID = Convert.ToInt32(db["ID"]);
            Bank = Convert.ToInt32(db["Bank"]);
            Money = Convert.ToInt32(db["Money"]);
            LastPosition = new Vector3(Convert.ToSingle(db["X"]), Convert.ToSingle(db["Y"]), Convert.ToSingle(db["Z"]));

            // Make sure our player gets to Dimension zero.
            DateTime dimensionTimeout = DateTime.Now.AddMilliseconds(5000);
            Thread thread = new Thread(() =>
            {
                while (API.getEntityDimension(player) != 0)
                {
                    API.setEntityDimension(player, 0);

                    if (DateTime.Now > dimensionTimeout)
                    {
                        API.kickPlayer(player, "~r~Your dimension was not set properly. Try logging in again.");
                        return;
                    }
                }
            });
            thread.Start();
            thread.Join();
            // Don't move on until the players dimension is ready.

            // Make our player controllable again.
            API.freezePlayer(player, false);
            API.setEntityTransparency(player, 255);
            API.setEntityPosition(player, LastPosition);

            API.consoleOutput(string.Format("{0} has been set to dimension 0.", player.name));
        }

        /** Deposit money into the player's bank. */
        public void depositMoney(int amount)
        {
            Bank += amount;
            Money -= amount;

            string before = "UPDATE Players SET";
            string[] varNames = { "Money", "Bank"};
            string after = string.Format("WHERE Id='{0}'", ID);
            object[] args = { Money, Bank };
            db.compileQuery(before, after, varNames, args);
        }

        /** Withdraw money from the player's bank. */
        public void withdrawMoney(int amount)
        {
            Bank -= amount;
            Money += amount;

            string before = "UPDATE Players SET";
            string[] varNames = { "Money", "Bank" };
            string after = string.Format("WHERE Id='{0}'", ID);
            object[] args = { Money, Bank };
            db.compileQuery(before, after, varNames, args);
        }

    }
}
