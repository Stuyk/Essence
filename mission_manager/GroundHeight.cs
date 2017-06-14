using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disorder_District.mission_manager
{
    class GroundHeight : Script
    {
        public GroundHeight() {
            API.onClientEventTrigger += API_onClientEventTrigger;
        }

        private void API_onClientEventTrigger(Client player, string eventName, params object[] arguments)
        {
            if (eventName == "recieveGroundHeight")
            {
                API.setEntityData(player, "Ground_Height", arguments[0]);
                return;
            }
        }
    }
}
