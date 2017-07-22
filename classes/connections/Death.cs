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

namespace Essence.classes.connections
{
    class Death : Script
    {
        public Death()
        {
            API.onPlayerRespawn += API_onPlayerRespawn;
            API.onPlayerDeath += API_onPlayerDeath;
        }

        private void API_onPlayerDeath(Client player, NetHandle entityKiller, int weapon)
        {
            //resyncIfInMission(player);
            kickOutOfMission(player);
        }

        private void API_onPlayerRespawn(Client player)
        {
            //resyncIfInMission(player);
        }

        /// <summary>
        /// Used for Mission Hits, verifies if the target has the data on him. If he dies from a mission player. Reward is given.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="entityKiller"></param>
        private void hitCheck(Client player, NetHandle entityKiller)
        {
            if (!API.hasEntityData(player, "Mission_Hit"))
            {
                return;
            }

            Mission mission = API.getEntityData(player, "Mission_Hit");

            if (API.getEntityType(entityKiller) == EntityType.Player)
            {
                Client killer = API.getPlayerFromHandle(entityKiller);

                if (mission.missionContainsPlayer(killer))
                {
                    mission.finishMission();
                    API.resetEntityData(player, "Mission_Hit");
                }
            }

            if (API.getEntityType(entityKiller) == EntityType.Vehicle)
            {
                Client[] occupants = API.getVehicleOccupants(entityKiller);

                foreach (Client passenger in occupants)
                {
                    if (mission.missionContainsPlayer(passenger))
                    {
                        mission.finishMission();
                        API.resetEntityData(player, "Mission_Hit");
                        break;
                    }
                }
            }
        }

        private void kickOutOfMission(Client player)
        {
            if (!player.hasData("Mission"))
            {
                return;
            }

            Mission mission = player.getData("Mission");
            API.triggerClientEvent(player, "endLockPickMiniGame");


            if (mission.RemoveFromMissionOnDeath)
            {
                mission.abandonMission(player, true);
            }
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
