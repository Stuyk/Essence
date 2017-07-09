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

        private enum OneOffItems
        {
            Radio,
            Phone
        }

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
        public static void PickupItem(Client player, params object[] arguments)
        {
            if (arguments.Length <= 0)
            {
                return;
            }

            NetHandle netValue = (NetHandle)arguments[0];


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
                        Player instance = player.getData("Instance");
                        Inventory inventory = instance.PlayerInventory;
                        string itemType = items[i].Type;
                        foreach (OneOffItems type in Enum.GetValues(typeof(OneOffItems)))
                        {
                            if (type.ToString() == itemType)
                            {
                                int value = inventory.getItemCountByType(itemType);
                                if (value <= 0)
                                {
                                    break;
                                }
                                return;
                            }
                        }
                        API.shared.deleteEntity(items[i].AttachedObject);
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

        public static void UseItem(Client player, params object[] arguments)
        {
            if (arguments.Length <= 0)
            {
                return;
            }

            string type = arguments[0].ToString();

            Player instance = player.getData("Instance");
            Inventory inventory = instance.PlayerInventory;
            // We specify our item types here.
            switch (type)
            {
                case "RefinedDrugs":
                    if (inventory.RefinedDrugs <= 0)
                    {
                        return;
                    }
                    inventory.RefinedDrugs -= 1;
                    API.shared.sendChatMessageToPlayer(player, "You consumed some drugs.");
                    player.armor += 10;
                    return;
            }
        }

        public static void GetItems(Client player, params object[] arguments)
        {
            if (!player.hasData("Instance"))
            {
                return;
            }

            Player instance = player.getData("Instance");
            instance.PlayerInventory.LoadItemsToLocal();
            return;
        }

        /// <summary>
        /// Create a new item and spit it out.
        /// </summary>
        /// <param name="type"></param>
        public static void NewItem(Client player, params object[] arguments)
        {
            if (arguments.Length <= 0)
            {
                return;
            }

            string type = arguments[0].ToString();
            Vector3 coords = (Vector3)arguments[1];
            int quantity = Convert.ToInt32(arguments[2]);

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
                case "Radio":
                    if (inventory.Radio >= quantity)
                    {
                        inventory.Radio -= quantity;
                        newItem = new InventoryItem(player, type, quantity, coords);
                        AddItem(newItem);
                    }
                    else
                    {
                        API.shared.sendChatMessageToPlayer(player, "~r~You don't have that much to drop. Re-open your inventory.");
                    }
                    return;
                case "Phone":
                    if(inventory.Phone >= quantity)
                    {
                        inventory.Phone -= quantity;
                        newItem = new InventoryItem(player, type, quantity, coords);
                        AddItem(newItem);
                    }
                    else
                    {
                        API.shared.sendChatMessageToPlayer(player, "~r~You don't have that much to drop. Re-open your inventory");
                    }
                    return;
            }
        }
    }
}
