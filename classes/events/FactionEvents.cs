﻿using Essence.classes.factions;
using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.events
{
    public class FactionEvents : Script
    {
        public FactionEvents()
        {
            API.onPlayerWeaponSwitch += FactionWeaponSwitch;
        }

        private void FactionWeaponSwitch(Client player, WeaponHash oldValue)
        {
            // Player doesn't have a faction, just remove the weapon for now.
            if (!player.hasData("Faction"))
            {
                API.removePlayerWeapon(player, player.currentWeapon);
                return;
            }

            FactionInfo info = player.getData("Faction");

            if (!Modules.isValidArmsForFaction(player, info))
            {
                API.removePlayerWeapon(player, player.currentWeapon);
                return;
            }
        }
    }
}
