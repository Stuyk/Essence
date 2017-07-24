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

        public void ExitInterior(Client player, params object[] arguments)
        {
            DoorManager.ExitDoor(player, arguments);
        }
    }
}
