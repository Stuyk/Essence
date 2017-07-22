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
using System.Timers;

namespace Essence.classes.inventory
{
    public class InventoryManager : Script
    {
        public InventoryManager()
        {
            API.onResourceStart += API_onResourceStart;
        }

        // Start a timer that loops through Inventory Items when the server starts.
        private void API_onResourceStart()
        {
            Timer timer = new Timer();
            timer.Interval = 10000;
            timer.Enabled = true;
            timer.Elapsed += ItemDeletionChecker;
        }

        // This loops through all the ActiveItems backwards and deletes any that are past their 'expiration date'
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
