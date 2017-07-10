using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.inventory
{
    public class ItemCalls : Script
    {
        public ItemCalls()
        {
            return;
        }

        public void DropItem(Client player, params object[] arguments)
        {
            Items.NewItem(player, arguments);
        }

        public void PickupItem(Client player, params object[] arguments)
        {
            Items.PickupItem(player, arguments);
        }

        public void GetItems(Client player, params object[] arguments)
        {
            Items.GetItems(player, arguments);
        }

        public void UseItem(Client player, params object[] arguments)
        {
            Items.UseItem(player, arguments);
        }
    }
}
