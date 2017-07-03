using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Essence.classes.inventory
{
    public class InventoryManager : Script
    {
        public InventoryManager()
        {
            API.onResourceStart += API_onResourceStart;
        }

        private void API_onResourceStart()
        {
            Timer timer = new Timer();
            timer.Interval = 10000;
            timer.Enabled = true;
            timer.Elapsed += ItemDeletionChecker;
        }

        private void ItemDeletionChecker(object sender, ElapsedEventArgs e)
        {
            for (int i = Items.ActiveItems.Count - 1; i >= 0; i--)
            {
                if (DateTime.Now.ToUniversalTime() > Items.ActiveItems[i].ExpirationTime)
                {
                    if (API.shared.doesEntityExist(Items.ActiveItems[i].AttachedObject))
                    {
                        API.deleteEntity(Items.ActiveItems[i].AttachedObject);
                    }
                    Items.ActiveItems.RemoveAt(i);
                    continue;
                }
            }
        }
    }
}
