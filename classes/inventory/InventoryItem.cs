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
        private int quantity;
        private string type;

        public InventoryItem() { }
        public InventoryItem(Client player, string type, int quantity)
        {
            this.type = type;
            this.quantity = quantity;
            switch (type)
            {
                case "CarParts":
                    attachedObject = API.createObject(232216084, player.position.Subtract(new Vector3(0, 0, 1)), player.rotation, player.dimension).handle;
                    API.setEntitySyncedData(attachedObject, "DROPPED_OBJECT", true);
                    return;
                case "UnrefinedDrugs":
                    attachedObject = API.createObject(371570974, player.position.Subtract(new Vector3(0, 0, 1)), player.rotation, player.dimension).handle;
                    API.setEntitySyncedData(attachedObject, "DROPPED_OBJECT", true);
                    return;
                case "RefinedDrugs":
                    attachedObject = API.createObject(1049338225, player.position.Subtract(new Vector3(0, 0, 1)), player.rotation, player.dimension).handle;
                    API.setEntitySyncedData(attachedObject, "DROPPED_OBJECT", true);
                    return;
            }

            API.delay(50000, true, () =>
            {
                if (API.doesEntityExist(attachedObject))
                {
                    API.deleteEntity(attachedObject);
                }
            });
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


    }
}
