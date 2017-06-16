using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.connections
{
    class Death : Script
    {
        public Death()
        {
            API.onPlayerRespawn += API_onPlayerRespawn;
            API.onPlayerDeath += API_onPlayerDeath;
        }

        private void API_onPlayerDeath(Client player, GTANetworkShared.NetHandle entityKiller, int weapon)
        {
            resyncIfInMission(player);
        }

        private void API_onPlayerRespawn(Client player)
        {
            resyncIfInMission(player);
        }

        private void resyncIfInMission(Client player)
        {
            if (!API.hasEntityData(player, "Mission"))
            {
                return;
            }

            Mission mission = API.getEntityData(player, "Mission");
            mission.setPauseState(true);

            API.delay(5000, true, () =>
            {
                mission.setupTeamSync();
                mission.setPauseState(false);
            });
            
        }
    }
}
