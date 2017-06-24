using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes
{
    public class Mission : Script
    {
        // Main storage for all of our mission objectives.
        private List<NetHandle> vehicles;
        private List<Objective> objectives;
        private List<Client> players;
        private bool pauseState = true; // Used to determine if the mission should be running yet.
        private int objectiveCompletion = 0; // Used as a 'Ticket' system for similarily written capture objectives.
        private DateTime objectiveCooldown; // DateTime comparison broken down into Milliseconds.
        private int partyInstance;
        private int missionReward;
        private string missionTitle;
        
        /** Main Constructor */
        public Mission()
        {
            pauseState = true;
            objectives = new List<Objective>();
            vehicles = new List<NetHandle>();
            players = new List<Client>();
            objectiveCooldown = DateTime.Now;
            partyInstance = new Random().Next(0, 9000000);
            missionReward = 0;
        }

        /****************************************************************
         * Create an objective. (Vector3, Objective.ObjectiveTypes.Type)
         * *************************************************************/
        public Objective CreateNewObjective(Vector3 location, Objective.ObjectiveTypes type)
        {
            Objective objective = new Objective();
            objective.setupObjective(location, type);
            objectives.Add(objective);
            return objective;
        }

        public void forceEmptyMission()
        {
            objectives = new List<Objective>();
            forceRemoveVehicles();
            objectiveCooldown = DateTime.Now;
        }

        /*************************************
         * Returns the party instance number.
         * **********************************/
        public int PartyInstance
        {
            get
            {
                return partyInstance;
            }
        }

        /** Should we run the mission now? */
        public bool PauseState
        {
            set
            {
                pauseState = value;
            }
            get
            {
                return pauseState;
            }
        }

        public int NumberOfPartyMembers
        {
            get
            {
                return players.Count;
            }
        }

        public int MissionReward
        {
            set
            {
                missionReward = value;
            }
            get
            {
                return missionReward;
            }
        }

        public string MissionTitle
        {
            set
            {
                missionTitle = value;
            }
            get
            {
                return missionTitle;
            }
        }

        /** Add a player to the mission. */
        public void addPlayer(Client player)
        {
            if (players.Contains(player))
            {
                setupTeamSync();
            }

            players.Add(player);
            API.consoleOutput(string.Format("[{0}] {1} has joined a mission.", partyInstance, player.name));
            API.setEntitySyncedData(player, "Mission", true);
            foreach (Client ally in players)
            {
                API.sendChatMessageToPlayer(ally, string.Format("{0} ~g~has joined the mission.", player.name));
            }

            setupTeamSync();
        }

        public void setupTeamSync()
        {
            setPauseState(true);
            foreach (Client player in players)
            {
                API.triggerClientEvent(player, "Mission_Cleanup_Players");
                foreach (Client ally in players)
                {
                    API.triggerClientEvent(player, "Mission_Add_Player", ally.handle); // <--- Try NetHandle
                }
            }
            setPauseState(false);
        }

        public void setPauseState(bool value)
        {
            foreach (Client ally in players)
            {
                API.triggerClientEvent(ally, "Mission_Pause_State", value);
            }
        }

        /** Abandon a mission. */
        public void abandonMission(Client player)
        {
            if (players.Contains(player))
            {
                players.Remove(player);
            }

            API.triggerClientEvent(player, "Mission_Abandon");
            API.triggerClientEvent(player, "Mission_Head_Notification", "~r~Abandoned the Party", "Fail");

            setupTeamSync();

            API.consoleOutput(string.Format("[{0}] {1} has left a mission.", partyInstance, player.name));

            if (players.Count <= 0)
            {
                if (objectives.Count > 0)
                {
                    foreach (Objective objective in objectives)
                    {
                        forceRemoveVehicles();
                    }
                }
            }

            API.resetEntityData(player, "Mission");
            API.resetEntitySyncedData(player, "Mission");
        }

        /***********************************************************
         * Removes objective markers, blips, etc. based on location.
         * ********************************************************/
        public void removeObjectiveForAll(Vector3 location)
        {
            foreach(Client ally in players)
            {
                API.triggerClientEvent(ally, "Mission_Remove_Objective", location);
            }
        }

        public void updateObjectiveProgressionForAll(Vector3 location, int currentProgression)
        {
            foreach(Client ally in players)
            {
                API.triggerClientEvent(ally, "Mission_Update_Progression", location, currentProgression);
            }
        }

        /*************************************************************
         * Forcefully remove any mission vehicles.
         * *********************************************/
        public void forceRemoveVehicles()
        {
            foreach (NetHandle vehicle in vehicles)
            {
                API.deleteEntity(vehicle);
            }
            vehicles = new List<NetHandle>();
        }

        public void addVehicle(NetHandle handle)
        {
            if (!vehicles.Contains(handle))
            {
                vehicles.Add(handle);
            }
        }

        public void startMission()
        {
            foreach (Client player in players)
            {
                objectives[0].syncObjectiveToPlayer(player);
                API.triggerClientEvent(player, "Mission_Pause_State", false);
                API.triggerClientEvent(player, "Mission_Head_Notification", "~y~Started: ~w~" + MissionTitle, "NewObjective");
            }
        }

        public void verifyObjective(Client player)
        {
            if (objectives.Count <= 0)
            {
                return;
            }

            objectives[0].verifyObjective(player);
        }

        public void goToNextObjective()
        {
            objectives.RemoveAt(0);

            if (objectives.Count <= 0)
            {
                finishMission();
                return;
            }

            foreach (Client player in players)
            {
                objectives[0].syncObjectiveToPlayer(player);
                API.triggerClientEvent(player, "Mission_Head_Notification", "~o~Major Objective Complete", "ObjectivesComplete");
            }
        }

        public void finishMission()
        {
            objectives = new List<Objective>();

            forceRemoveVehicles();

            foreach (Client player in players)
            {
                API.triggerClientEvent(player, "Mission_Head_Notification", "~y~Awarded: ~w~$" + MissionReward, "Finish");

                Player instance = API.getEntityData(player, "Instance");
                instance.Money += MissionReward;
            }

            MissionReward = 0;
        }

        /** Specifically syncs the players objective completion rate. **/
        public void syncPlayersObjectiveCompletion()
        {
            foreach (Client player in players)
            {
                API.setEntitySyncedData(player, "Mission_Status", objectiveCompletion);
            }
        }

        public void teleportAllPlayers(Vector3 location)
        {
            foreach (Client player in players)
            {
                API.setEntityPosition(player, location.Around(5).Add(new Vector3(0, 0, 2)));
            }
        }

        public void setPlayersIntoVehicles()
        {
            foreach (NetHandle veh in vehicles)
            {
                int seat = -1;
                Client[] occupants = API.getVehicleOccupants(veh);
                if (occupants.Length > 0)
                {
                    return;
                }
                foreach(Client player in players)
                {
                    if (player.isInVehicle)
                    {
                        continue;
                    }
                    API.setPlayerIntoVehicle(player, veh, seat);
                    seat++;
                }
            }
        }
    }
}
