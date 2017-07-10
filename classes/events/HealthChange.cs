using Essence.classes.anticheat;
using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.events
{
    public class HealthChange : Script
    {
        public HealthChange()
        {
            API.onPlayerHealthChange += API_onPlayerHealthChange;
        }

        private void API_onPlayerHealthChange(Client player, int oldValue)
        {
            Anticheat.checkHealth(player, oldValue);
        }

        
    }
}
