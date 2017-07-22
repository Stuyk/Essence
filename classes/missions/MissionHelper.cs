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

namespace Essence.classes.missions
{
    public class MissionHelper : Script
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
