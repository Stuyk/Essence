using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.datahandles
{
    class Chat : Script
    {
        public Chat()
        {
            API.onChatMessage += API_onChatMessage;
        }

        private void API_onChatMessage(Client player, string message, CancelEventArgs msgEvent)
        {
            if (!API.hasEntityData(player, "Instance"))
            {
                msgEvent.Cancel = true;
                return;
            }
        }
    }
}
