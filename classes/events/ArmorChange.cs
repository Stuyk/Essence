using Essence.classes.anticheat;
using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.events
{
    public class ArmorChange : Script
    {
        public ArmorChange()
        {
            API.onPlayerArmorChange += API_onPlayerArmorChange;
        }

        private void API_onPlayerArmorChange(Client player, int oldValue)
        {
            Anticheat.checkArmor(player, oldValue);
        }
    }
}
