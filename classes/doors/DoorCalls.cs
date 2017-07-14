using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.doors
{
    public class DoorCalls : Script
    {
        public DoorCalls()
        {
            API.onResourceStart += LoadDoors;
        }

        private void LoadDoors()
        {
            Interiors.loadAllInteriors();
            DoorManager.LoadAllDoors();
        }

        public void EnterInterior(Client player, params object[] arguments)
        {
            DoorManager.EnterDoor(player, arguments);
        }
    }
}
