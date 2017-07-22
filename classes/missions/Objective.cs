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
using System.Threading;
using System.Threading.Tasks;

namespace Essence.classes
{
    public class Objective : Script
    {
        private List<NetHandle> objectiveTargets;
        private List<NetHandle> objectiveVehicles;
        private List<ObjectiveInfo> objectives;
        private List<ObjectiveInfo> removeableObjectives;
        private int objectiveCount;
        private int objectivesComplete;
        private DateTime objectiveCooldown;
        private Mission mission;

        public enum ObjectiveTypes
        {
            Location,
            Capture,
            Teleport,
            Destroy,
            SetIntoVehicle,
            VehicleCapture,
            VehicleLocation,
            PickupObject,
            RetrieveVehicle,
            KillPlayer,
            UnlockVehicles,
            BreakIntoVehicle,
            None
        }

        /** The main constructor for a new objective.
         * Adds a single new objective. Use addObjective to add additional objectives to this instance. */
        public Objective()
        {
            setupLists();
        }

        private void setupLists()
        {
            objectiveCount = 0;
            objectivesComplete = 0;
            objectiveTargets = new List<NetHandle>();
            objectiveVehicles = new List<NetHandle>();
            objectiveCooldown = DateTime.UtcNow;
            objectives = new List<ObjectiveInfo>();
            removeableObjectives = new List<ObjectiveInfo>();
        }

        /// <summary>
        /// Assigned on emptyObjective creation.
        /// </summary>
        public Mission MissionInstance
        {
            set
            {
                mission = value;
            }
            get
            {
                return mission;
            }
        }

        /// <summary>
        /// Add new objective info to your Major Objective.
        /// </summary>
        /// <returns></returns>
        public ObjectiveInfo addEmptyObjectiveInfo()
        {
            ObjectiveInfo objInfo = new ObjectiveInfo();
            objInfo.MissionInstance = mission;
            objectives.Add(objInfo);
            return objInfo;
        }

        /// <summary>
        /// Returns a list of all the ObjectiveInfo in the master objective.
        /// </summary>
        public List<ObjectiveInfo> Objectives {
            get {
                return objectives;
            }
        }

        /// <summary>
        /// Removes a specific ObjectiveInfo from the master objective.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mission"></param>
        public void forceRemoveObjective(ObjectiveInfo obj, Mission mission)
        {
            if (objectives.Contains(obj))
            {
                objectives.Remove(obj);
            }

            mission.setupTeamSync();

            if (objectives.Count >= 1)
            {
                return;
            }

            mission.goToNextObjective();
        }

        /// <summary>
        /// Adds a vehicle that is attached to the Mission.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="location"></param>
        /// <param name="type"></param>
        /// <param name="rotation"></param>
        /// <param name="uniqueID"></param>
        /// <returns></returns>
        public NetHandle addObjectiveVehicle(Mission instance, Vector3 location, VehicleHash type, Vector3 rotation = null, int uniqueID = -1)
        {
            if (rotation == null)
            {
                NetHandle veh = API.createVehicle(type, location, new Vector3(), 52, 52);
                API.setEntityData(veh, "Mission", instance);
                instance.addVehicle(veh);
                API.setEntityData(veh, "Mission_UID", uniqueID);
                return veh;
            } else
            {
                NetHandle veh = API.createVehicle(type, location, rotation, 52, 52);
                API.setEntityData(veh, "Mission", instance);
                instance.addVehicle(veh);
                API.setEntityData(veh, "Mission_UID", uniqueID);
                return veh;
            }
            
        }

        /// <summary>
        /// Add a specific vehicle that will not despawn to the mission instance.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="vehicle"></param>
        /// <param name="uniqueID"></param>
        public void addPlayerVehicle(Mission instance, NetHandle vehicle, int uniqueID)
        {
            API.setEntityData(vehicle, "Mission", instance);
            API.setEntityData(vehicle, "Mission_UID", uniqueID);
        }

        /// <summary>
        /// Removes all attached objects from ObjectiveInfo.
        /// </summary>
        public void removeAllAttachedObjects()
        {
            foreach (ObjectiveInfo obj in objectives)
            {
                obj.removeAttachedObject();
            }
        }

        /// <summary>
        /// Queue an objective to be removed.
        /// </summary>
        /// <param name="obj"></param>
        public void queueMinorObjectiveForRemoval(ObjectiveInfo obj)
        {
            if (removeableObjectives.Contains(obj))
            {
                return;
            }

            removeableObjectives.Add(obj);
        }

        /// <summary>
        /// Used to remove a single Objective from the Major Objective.
        /// </summary>
        /// <param name="obj"></param>
        public void removeMinorObjective(ObjectiveInfo obj)
        {
            if (objectives.Contains(obj))
            {
                mission.removeObjectiveForAll(obj);
                objectives.Remove(obj);
            }
        }

        /// <summary>
        /// Used to remove all minor objectives from the major objective that are complete.
        /// </summary>
        private void removeQueuedObjectives()
        {
            foreach (ObjectiveInfo obj in removeableObjectives)
            {
                removeMinorObjective(obj);
            }
            removeableObjectives = new List<ObjectiveInfo>();
        }

        /// <summary>
        /// Syncs all objective into to a player.
        /// </summary>
        /// <param name="player"></param>
        public void syncObjectiveToPlayer(Client player)
        {
            List<ObjectiveInfo> removeables = new List<ObjectiveInfo>();

            foreach (ObjectiveInfo objInfo in objectives)
            {
                if (objInfo.Type == ObjectiveTypes.SetIntoVehicle)
                {
                    mission.setPlayersIntoVehicles();
                    removeables.Add(objInfo);
                    continue;
                }

                if (objInfo.Type == ObjectiveTypes.Teleport)
                {
                    mission.teleportAllPlayers(objInfo.Location);
                    removeables.Add(objInfo);
                    continue;
                }

                if (objInfo.Type == ObjectiveTypes.UnlockVehicles)
                {
                    mission.unlockAllVehicles();
                    removeables.Add(objInfo);
                    continue;
                }

                if (objInfo.Type == ObjectiveTypes.BreakIntoVehicle)
                {
                    API.setEntityData(player, "Minigame", objInfo.Lockpick);
                    objInfo.Lockpick.addPlayer(player);
                    objInfo.Lockpick.startLockpick();
                    objInfo.Lockpick.isRunning = true;
                }

                API.triggerClientEvent(player, "Mission_New_Objective", objInfo.Location, objInfo.Type.ToString(), objInfo.ObjectiveID);
            }

            foreach (ObjectiveInfo objInfo in removeables)
            {
                if (!objectives.Contains(objInfo))
                {
                    continue;
                }
                objectives.Remove(objInfo);
            }

            API.triggerClientEvent(player, "Mission_Setup_Objectives");
            API.triggerClientEvent(player, "Mission_Head_Notification", "~o~New Objective", "NewObjective");
        }

        /// <summary>
        /// Checks if the objective is complete. Primary method to determine if a minor objective is completed or not.
        /// </summary>
        /// <param name="player"></param>
        public void verifyObjective(Client player)
        {
            // Just to prevent an error from occuring where too many requests get sent. */
            if (objectives.Count <= 0)
            {
                return;
            }

            foreach (ObjectiveInfo objInfo in objectives)
            {
                objInfo.attemptToVerify(player);
                if (objInfo.Status)
                {
                    queueMinorObjectiveForRemoval(objInfo);
                }
            }

            removeQueuedObjectives();

            mission.setupTeamSync();

            // Check if all of our objectives are complete.
            if (objectives.Count >= 1)
            {
                return;
            }

            mission.goToNextObjective();
        }

        /// <summary>
        /// Get total minor objective count.
        /// </summary>
        public int ObjectiveCount
        {
            get
            {
                return objectiveCount;
            }
        }

        /// <summary>
        /// Get completed objective count for this major objective.
        /// </summary>
        public int CompletedObjectives
        {
            get
            {
                return objectivesComplete;
            }
        }

        
    }
}
