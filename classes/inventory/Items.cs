using Essence.classes.datahandles;
using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.inventory
{
    public static class Items
    {
        private static List<InventoryItem> items = new List<InventoryItem>();

        /// <summary>
        /// Get all of the items that are currently spawned.
        /// </summary>
        public static List<InventoryItem> ActiveItems
        {
            get
            {
                return items;
            }
        }

        /// <summary>
        /// Used to pickup an item.
        /// </summary>
        /// <param name="netValue"></param>
        public static void PickupItem(Client player, NetHandle netValue)
        {
            if (!API.shared.doesEntityExist(netValue))
            {
                return;
            }

            if (player.position.DistanceTo(API.shared.getEntityPosition(netValue)) >= 5)
            {
                return;
            }

            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].AttachedObject == netValue)
                {
                    if (API.shared.doesEntityExist(items[i].AttachedObject))
                    {
                        API.shared.deleteEntity(items[i].AttachedObject);
                        Player instance = player.getData("Instance");
                        Inventory inventory = instance.PlayerInventory;
                        inventory.addBasedOnType(items[i].Type, items[i].Quantity);
                        API.shared.triggerClientEvent(player, "HeadNotification", string.Format("Picked up {0}. Total: {1}", items[i].Type, items[i].Quantity));
                        items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Add an item to the item list.
        /// </summary>
        /// <param name="item"></param>
        public static void AddItem(InventoryItem item)
        {
            if (!items.Contains(item))
            {
                items.Add(item);
            }
        }

        /// <summary>
        /// Remove an item from the item list.
        /// </summary>
        /// <param name="item"></param>
        public static void RemoveItem(InventoryItem item)
        {
            if (!items.Contains(item))
            {
                items.Remove(item);
            }
        }

        /// <summary>
        /// Create a new item and spit it out.
        /// </summary>
        /// <param name="type"></param>
        public static void NewItem(Client player, string type, Vector3 coords, int quantity)
        {
            if (coords.DistanceTo(player.position) > 6)
            {
                API.shared.sendChatMessageToPlayer(player, "~r~You're attempting to drop an item too far away. Try aiming down.");
                return;
            }

            Player instance = player.getData("Instance");
            Inventory inventory = instance.PlayerInventory;
            InventoryItem newItem;
            switch (type)
            {
                case "CarParts":
                    if (inventory.CarParts >= quantity)
                    {
                        inventory.CarParts -= quantity;
                        newItem = new InventoryItem(player, type, quantity, coords);
                        AddItem(newItem);
                    } else {
                        API.shared.sendChatMessageToPlayer(player, "~r~You don't have that much to drop. Re-open your inventory.");
                    }
                    return;
                case "UnrefinedDrugs":
                    if (inventory.UnrefinedDrugs >= quantity)
                    {
                        inventory.UnrefinedDrugs -= quantity;
                        newItem = new InventoryItem(player, type, quantity, coords);
                        AddItem(newItem);
                    }
                    else
                    {
                        API.shared.sendChatMessageToPlayer(player, "~r~You don't have that much to drop. Re-open your inventory.");
                    }
                    return;
                case "RefinedDrugs":
                    if (inventory.RefinedDrugs >= quantity)
                    {
                        inventory.RefinedDrugs -= quantity;
                        newItem = new InventoryItem(player, type, quantity, coords);
                        AddItem(newItem);
                    }
                    else
                    {
                        API.shared.sendChatMessageToPlayer(player, "~r~You don't have that much to drop. Re-open your inventory.");
                    }
                    return;
            }
        }
    }
}
