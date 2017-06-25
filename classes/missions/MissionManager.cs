using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Essence.classes
{
    public class MissionManager : Script
    {
        DateTime lastTime = DateTime.Now.AddMilliseconds(5000);

        public MissionManager()
        {
            API.onPlayerDisconnected += API_onPlayerDisconnected;
        }

        private void API_onPlayerDisconnected(Client player, string reason)
        {
            if (API.hasEntityData(player, "Mission"))
            {
                Mission mission = API.getEntityData(player, "Mission");
                mission.abandonMission(player);
            }
        }

        /** Check if our player is in a mission. */
        private bool checkIfInMission(Client player)
        {
            if(!API.hasEntityData(player, "Mission")) {
                return false;
            }

            return true;
        }

        // Invite a player to a mission.
        [Command("invite")]
        public void cmdInvitePlayerToMission(Client player, string target)
        {
            Mission mission = API.getEntityData(player, "Mission");

            if (!mission.PauseState)
            {
                API.sendChatMessageToPlayer(player, "~r~A mission is currently running, you cannot invite others.");
                API.sendChatMessageToPlayer(player, "~r~You can always ~w~/leaveparty ~r~if you want to abandon your allies.");
                return;
            }

            Client targetPlayer = null;

            foreach (Client p in API.getAllPlayers())
            {
                if (p.name.Contains(target))
                {
                    targetPlayer = p;
                    break;
                }
            }

            if (targetPlayer == null)
            {
                API.triggerClientEvent(player, "Mission_Head_Notification", "~r~Player does not exist.", "Error");
                return;
            }

            if (targetPlayer == player)
            {
                API.triggerClientEvent(player, "Mission_Head_Notification", "~r~Stop trying to invite yourself you fuck.", "Error");
                return;
            }

            if (API.hasEntityData(targetPlayer, "Mission"))
            {
                API.sendChatMessageToPlayer(player, "~r~That player is already in a party. Tell them to ~w~/leaveparty~r~, if you want them to join.");
                return;
            }

            API.sendChatMessageToPlayer(targetPlayer, string.Format("{0} invited you to a mission. Type '/joinparty' to join.", player.name));
            API.sendChatMessageToPlayer(player, string.Format("Invited {0} to your party.", targetPlayer.name));
            API.setEntityData(targetPlayer, "Mission", mission);
        }

        // Abandon any currently running mission a player is in.
        [Command("leaveparty")]
        public void cmdLeavePlayerMission(Client player)
        {
            if (API.hasEntityData(player, "Mission"))
            {
                Mission mission = API.getEntityData(player, "Mission");
                mission.abandonMission(player);
            } else {
                API.triggerClientEvent(player, "Mission_Head_Notification", "~r~You are not in a party.", "Error");
            }
        }

        // Add a player who has an invitation to a party instance.
        [Command("joinparty")]
        public void cmdAcceptMission(Client player)
        {
            if (!API.hasEntityData(player, "Mission"))
            {
                API.sendChatMessageToPlayer(player, "~r~You don't have any missions to join.");
                return;
            }

            Mission mission = API.getEntityData(player, "Mission");
            mission.addPlayer(player);
        }

        [Command("createparty")]
        public void cmdCreateMission(Client player)
        {
            if (checkIfInMission(player))
            {
                API.sendChatMessageToPlayer(player, "~r~You're already in a party, ~w~/leaveparty ~r~if you want to abandon your allies.");
                return;
            }

            Mission mission = new Mission();
            API.setEntitySyncedData(player, "Mission_New_Instance", true);
            API.setEntityData(player, "Mission", mission);
            mission.addPlayer(player);
            API.triggerClientEvent(player, "Mission_Head_Notification", "New Party Created", "NewObjective");
        }
        
        
        
        
    }
}
