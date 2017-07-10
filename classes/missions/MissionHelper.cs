using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.missions
{
    public static class MissionHelper
    {
        /// <summary>
        /// Verify if a player is completing an objective. Used mainly from trigger events.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="arguments"></param>
        public static void CheckObjective(Client player, params object[] arguments)
        {
            if (!player.hasData("Mission"))
            {
                return;
            }
            Mission mission = player.getData("Mission");
            mission.verifyObjective(player);
        }
    }
}
