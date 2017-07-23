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
    public class OverworldItem
    {
        private NetHandle attachedObject;
        private DateTime expirationTime;
        private Item item;

        public OverworldItem(Client player, Item item, Vector3 aimPos)
        {
            this.item = item;
            expirationTime = DateTime.Now.ToUniversalTime().AddMinutes(2);

            switch (item.Type)
            {
                case Items.ItemTypes.COCAINE:
                    attachedObject = API.shared.createObject(1049338225, aimPos.Add(new Vector3(0, 0, 0.3)), player.rotation, player.dimension).handle;
                    API.shared.setEntitySyncedData(attachedObject, "DROPPED_OBJECT", true);
                    return;

                case Items.ItemTypes.RADIO:
                    attachedObject = API.shared.createObject(-1964402432, aimPos, player.rotation, player.dimension).handle;
                    API.shared.setEntitySyncedData(attachedObject, "DROPPED_OBJECT", true);
                    return;

                case Items.ItemTypes.HOT_DOG:
                    attachedObject = API.shared.createObject(-1729226035, aimPos, player.rotation, player.dimension).handle;
                    API.shared.setEntitySyncedData(attachedObject, "DROPPED_OBJECT", true);
                    return;


                    /*
                    case "CarParts":
                        attachedObject = API.createObject(232216084, aimPos, player.rotation, player.dimension).handle;
                        API.setEntitySyncedData(attachedObject, "DROPPED_OBJECT", true);
                        return;
                    case "UnrefinedDrugs":
                        attachedObject = API.createObject(371570974, aimPos, player.rotation, player.dimension).handle;
                        API.setEntitySyncedData(attachedObject, "DROPPED_OBJECT", true);
                        return;
                    case "RefinedDrugs":
                        attachedObject = API.createObject(1049338225, aimPos.Add(new Vector3(0, 0, 0.3)), player.rotation, player.dimension).handle;
                        API.setEntitySyncedData(attachedObject, "DROPPED_OBJECT", true);
                        return;
                    case "Radio":
                        attachedObject = API.createObject(-1964402432, aimPos, player.rotation, player.dimension).handle;
                        API.setEntitySyncedData(attachedObject, "DROPPED_OBJECT", true);
                        return;
                    case "Phone":
                        attachedObject = API.createObject(974883178, aimPos, player.rotation, player.dimension).handle;
                        API.setEntitySyncedData(attachedObject, "DROPPED_OBJECT", true);
                        return;
                    */
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

        public Item Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
            }
        }


    }
}
