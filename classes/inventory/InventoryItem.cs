using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.inventory
{
    public class InventoryItem : Script
    {
        private NetHandle attachedObject;
        private DateTime expirationTime;
        private int quantity;
        private string type;

        public InventoryItem() { }
        public InventoryItem(Client player, string type, int quantity, Vector3 aimPos)
        {
            expirationTime = DateTime.Now.ToUniversalTime().AddMinutes(2);
            this.type = type;
            this.quantity = quantity;
            switch (type)
            {
                case "CarParts":
                    attachedObject = API.createObject(232216084, aimPos, player.rotation, player.dimension).handle;
                    API.setEntitySyncedData(attachedObject, "DROPPED_OBJECT", true);
                    return;
                case "UnrefinedDrugs":
                    attachedObject = API.createObject(371570974, aimPos, player.rotation, player.dimension).handle;
                    API.setEntitySyncedData(attachedObject, "DROPPED_OBJECT", true);
                    return;
                case "RefinedDrugs":
                    attachedObject = API.createObject(1049338225, aimPos, player.rotation, player.dimension).handle;
                    API.setEntitySyncedData(attachedObject, "DROPPED_OBJECT", true);
                    return;
            }
            
        }

        public string Type
        {
            get
            {
                return type;
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }
        }

        public NetHandle AttachedObject
        {
            get
            {
                return attachedObject;
            }
        }

        public DateTime ExpirationTime
        {
            get
            {
                return expirationTime;
            }
        }


    }
}
