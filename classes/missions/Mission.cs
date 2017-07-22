using Essence.classes.missions;
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
using System.Timers;

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
        private DateTime startTime; // The time when the player started the mission officially.
        private int partyInstance;
        private int missionReward;
        private WeaponHash missionWeaponReward;
        private string missionTitle;
        private Timer timer;
        private int maxMissionTime;
        private StashInfo stashInfo;
        private bool removeFromMissionOnDeath;
        
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
            maxMissionTime = -1;
            missionWeaponReward = WeaponHash.Unarmed;
            stashInfo = null;
            removeFromMissionOnDeath = false;
        }

        /// <summary>
        /// Enables a timer for the mission. Use 'MissionTime' to set how long the mission is available for.
        /// </summary>
        public void useTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Enabled = false;
            }

            timer = new Timer();
            timer.Interval = 500;
            timer.Elapsed += Updater;
            timer.Enabled = true;
        }

        private void Updater(object sender, ElapsedEventArgs e)
        {
            if (players.Count <= 0)
            {
                timer.Stop();
                return;
            }

            foreach (Client player in players)
            {
                API.setEntitySyncedData(player, "Current_Position", player.position);
            }

            if (maxMissionTime != -1)
            {
                foreach (Client player in players)
                {
                    API.setEntitySyncedData(player, "Mission_Timer", startTime.AddSeconds(maxMissionTime).Subtract(DateTime.Now).TotalSeconds);
                }

                if (DateTime.Now > startTime.AddSeconds(maxMissionTime))
                {
                    maxMissionTime = -1;
                    foreach (Client player in players)
                    {
                        API.triggerClientEvent(player, "Mission_Finish");
                        API.triggerClientEvent(player, "Mission_Head_Notification", "~r~Mission Failed", "Fail");
                        API.triggerClientEvent(player, "End_Lock_Pick_Minigame");
                    }
                    forceEmptyMission();
                }
            }
        }

        public bool missionContainsPlayer(Client player)
        {
            if (players.Contains(player))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns an empty Objective to add ObjectiveInfo into.
        /// </summary>
        /// <returns></returns>
        public Objective addEmptyObjective(Mission mission)
        {
            Objective objective = new Objective();
            objective.MissionInstance = mission;
            objectives.Add(objective);
            return objective;
        }

        public void forceEmptyMission()
        {
            objectives = new List<Objective>();
            forceRemoveVehicles();
            objectiveCooldown = DateTime.Now;
        }

        /// <summary>
        /// Returns the party instance number.
        /// </summary>
        public int PartyInstance
        {
            get
            {
                return partyInstance;
            }
        }

        public bool RemoveFromMissionOnDeath
        {
            set
            {
                removeFromMissionOnDeath = value;
            }
            get
            {
                return removeFromMissionOnDeath;
            }
        }

        /// <summary>
        /// Sets the mission time. If set, enable the timer by using useTimer();
        /// </summary>
        public int MissionTime
        {
            set
            {
                maxMissionTime = value;
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

        /// <summary>
        /// Return the number of party members.
        /// </summary>
        public int NumberOfPartyMembers
        {
            get
            {
                return players.Count;
            }
        }

        /// <summary>
        /// Attach a stash to this mission.
        /// </summary>
        public StashInfo AttachStashInfo
        {
            set
            {
                stashInfo = value;
            }
            get
            {
                return stashInfo;
            }
        }

        /// <summary>
        /// Set the reward for the mission when it is complete. Each player will recieve this reward.
        /// </summary>
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

        /// <summary>
        /// Set the weapon reward for the mission when it is complete. Each player will recieve this weapon reward.
        /// </summary>
        public WeaponHash MissionWeaponReward
        {
            set
            {
                missionWeaponReward = value;
            }
            get
            {
                return missionWeaponReward;
            }
        }

        /// <summary>
        /// Set the title of the mission.
        /// </summary>
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

        /// <summary>
        /// Returns how many objectivese are currently active in this party.
        /// </summary>
        public int MissionObjectiveCount
        {
            get
            {
                return objectives.Count;
            }
        }

        /// <summary>
        /// Add a player to the party.
        /// </summary>
        /// <param name="player"></param>
        public void addPlayer(Client player)
        {
            if (players.Contains(player))
            {
                setupTeamSync();
                return;
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

        /// <summary>
        /// Forces mission synchronization to all the players.
        /// </summary>
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

        /// <summary>
        /// Abandons the mission for a player, kicking them out of the party. Anyone left inside can continue the mission. If not the mission kills itself.
        /// </summary>
        /// <param name="player"></param>
        public void abandonMission(Client player, bool died = false)
        {
            if (players.Contains(player))
            {
                players.Remove(player);
            }

            API.triggerClientEvent(player, "Mission_Abandon");
            API.triggerClientEvent(player, "Mission_Head_Notification", "~r~Abandoned the Party", "Fail");

            setupTeamSync();

            if (died)
            {
                API.sendChatMessageToPlayer(player, string.Format("{0} have died, and were removed from the mission.", player.name));
            }

            if (players.Count <= 0)
            {
                if (objectives.Count > 0)
                {
                    foreach (Objective objective in objectives)
                    {
                        forceRemoveVehicles();
                        objective.removeAllAttachedObjects();
                    }
                }
                if (timer != null)
                {
                    timer.Enabled = false;
                    timer.Stop();
                }
                
            } else {
                if (died)
                {
                    foreach (Client ally in players)
                    {
                        API.sendChatMessageToPlayer(ally, string.Format("{0} has died and has been removed from the mission.", player.name));
                    }
                }
            }

            API.setEntitySyncedData(player, "Mission_Timer", -1);
            API.resetEntityData(player, "Mission");
            API.resetEntitySyncedData(player, "Mission");
        }


        /// <summary>
        /// Removes an objective based on location.
        /// </summary>
        /// <param name="location"></param>
        public void removeObjectiveForAll(ObjectiveInfo objInfo)
        {
            foreach(Client ally in players)
            {
                if (objInfo.Type == Objective.ObjectiveTypes.BreakIntoVehicle)
                {
                    API.triggerClientEvent(ally, "End_Lock_Pick_Minigame");
                }

                API.triggerClientEvent(ally, "Mission_Remove_Objective", objInfo.ObjectiveID);
                API.triggerClientEvent(ally, "Mission_Head_Notification", "~b~Minor Objective Complete", "Objective");
            }
        }

        /// <summary>
        /// Updates objective progression for everyone. This is mostly for capture points, destruction, etc. Percentage based.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="currentProgression"></param>
        public void updateObjectiveProgressionForAll(Vector3 location, int currentProgression)
        {
            foreach(Client ally in players)
            {
                API.triggerClientEvent(ally, "Mission_Update_Progression", location, currentProgression);
            }
        }

        /// <summary>
        /// Forcefully removes all vehicles from the mission.
        /// </summary>
        public void forceRemoveVehicles()
        {
            foreach (NetHandle vehicle in vehicles)
            {
                API.setVehicleLocked(vehicle, true);

                Client[] passengers = API.getVehicleOccupants(vehicle);
                foreach (Client passenger in passengers)
                {
                    API.sendNativeToPlayer(passenger, (ulong)Hash.TASK_LEAVE_VEHICLE, passenger, vehicle, 0);
                }

                API.delay(5000, true, () =>
                {

                    API.deleteEntity(vehicle);
                });
                
            }
            vehicles = new List<NetHandle>();
        }

        /// <summary>
        /// Unlock all vehicles in the mission instance.
        /// </summary>
        public void unlockAllVehicles()
        {
            foreach (NetHandle vehicle in vehicles)
            {
                API.setVehicleLocked(vehicle, false);
            }
        }

        /// <summary>
        /// Adds a vehicle to the mission.
        /// </summary>
        /// <param name="handle"></param>
        public void addVehicle(NetHandle handle)
        {
            if (!vehicles.Contains(handle))
            {
                vehicles.Add(handle);
            }
        }

        /// <summary>
        /// This should be ran after the mission is completely setup.
        /// </summary>
        public void startMission()
        {
            startTime = DateTime.Now;

            foreach (Client player in players)
            {
                API.triggerClientEvent(player, "Mission_New_Instance");
                objectives[0].syncObjectiveToPlayer(player);
                API.triggerClientEvent(player, "Mission_Pause_State", false);
                API.triggerClientEvent(player, "Mission_Head_Notification", "~y~Started: ~w~" + MissionTitle, "NewObjective");
            }

            setupTeamSync();
        }

        /// <summary>
        /// Used to verify if the player is currently completing an objective.
        /// </summary>
        /// <param name="player"></param>
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
            timer.Stop();
            timer.Dispose();
            objectives = new List<Objective>();

            forceRemoveVehicles();
            var finalReward = Math.Floor(Convert.ToDouble(MissionReward / players.Count));

            foreach (Client player in players)
            {
                API.setEntitySyncedData(player, "Mission_Timer", -1);
                API.triggerClientEvent(player, "Mission_Head_Notification", "~y~Awarded: ~w~$" + MissionReward, "Finish");
                API.triggerClientEvent(player, "Play_Screen_FX", "SuccessNeutral", 5000, false);
                API.triggerClientEvent(player, "End_Lock_Pick_Minigame");
                Player instance = API.getEntityData(player, "Instance");
                instance.Money += Convert.ToInt32(finalReward);

                if (MissionWeaponReward != WeaponHash.Unarmed)
                {
                    API.givePlayerWeapon(player, MissionWeaponReward, 20, false, true);
                }
            }

            MissionReward = 0;

            if (stashInfo != null)
            {
                stashInfo.Quantity += 5;
            }
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
