using Essence.classes.utility;
using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.missions
{
    public class StashInfo
    {
        private int quantity;
        private int id;
        private StashType type;
        private Vector3 location;

        public enum StashType
        {
            None,
            CarParts
        }

        public StashInfo()
        {
            quantity = -1;
            id = -1;
            type = StashType.None;
            location = new Vector3();
        }

        /// <summary>
        /// Get or set the Quantity of items in this stash.
        /// </summary>
        public int Quantity
        {
            set
            {
                if (quantity == -1)
                {
                    quantity = value;
                    return;
                }

                if (quantity != value)
                {
                    quantity = value;
                    PointHelper.updatePointInfo(ID.ToString(), string.Format("Contains - {0} ~n~ Quantity: {1}", Type.ToString(), value.ToString()));
                    API.shared.triggerClientEventForAll("Update_Stash_Info", id, string.Format("Contains - {0} ~n~ Quantity: {1}", Type.ToString(), value.ToString()));
                    updateStash();
                    return;
                }
                quantity = value;
            }
            get
            {
                return quantity;
            }
        }

        /// <summary>
        /// The SQLite storage ID.
        /// </summary>
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

        /// <summary>
        /// The type of stash.
        /// </summary>
        public StashType Type
        {
            set
            {
                type = value;
            }
            get
            {
                return type;
            }
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

        private void updateStash()
        {
            string target = "UPDATE Stash SET";
            string where = string.Format("WHERE Id='{0}'", ID);
            string[] variables = { "Quantity" };
            object[] data = { Quantity };
            Payload.addNewPayload(target, where, variables, data);
        }
    }
}
