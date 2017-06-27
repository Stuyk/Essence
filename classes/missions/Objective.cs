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
    public class ObjectiveInfo : Script
    {
        private int progress;
        private Vector3 location;
        private Vector3 direction;
        private Objective.ObjectiveTypes type;
        private bool completed;
        private Vector3 nullVector = new Vector3(0, 0, 0);
        private int uniqueID;
        private NetHandle attachedObject;

        /// <summary>
        /// Generate a new ObjectiveInfo and assign your information for the objective. Default Progress: 0, Completed: False, UniqueID is -1, No attached objects.
        /// </summary>
        public ObjectiveInfo()
        {
            progress = 0;
            location = new Vector3();
            direction = new Vector3();
            type = Objective.ObjectiveTypes.None;
            completed = false;
            uniqueID = -1;
        }

        /// <summary>
        /// Set the current progress of this objective. Always set this to zero.
        /// </summary>
        public int Progress
        {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
            }
        }

        /// <summary>
        /// Set the location of this objective. This is 100% required to make an objective function properly.
        /// </summary>
        public Vector3 Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        /// <summary>
        /// Set the type of objective this should be.
        /// </summary>
        public Objective.ObjectiveTypes Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        /// <summary>
        /// Set the current completion status of this objective. If it's true, it'll remove itself. Don't set it to true. God dammit.
        /// </summary>
        public bool Status
        {
            get
            {
                return completed;
            }
            set
            {
                completed = value;
                if (value == true)
                {
                    removeAttachedObject();
                }
            }
        }

        /// <summary>
        /// Used to set a direction the marker should face.
        /// </summary>
        public Vector3 Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        /// <summary>
        /// A unique ID assigned to a vehicle that is required for this objective. Only effective if using VehicleLocation or VehicleCapture.
        /// </summary>
        public int UniqueVehicleID
        {
            get
            {
                return uniqueID;
            }
            set
            {
                uniqueID = value;   
            }
        }

        /// <summary>
        /// Add an object that will be removed from this objective once complete. Mostly for visual queue's.
        /// </summary>
        public NetHandle AddObject
        {
            set
            {
                attachedObject = value;
            }
            get
            {
                return attachedObject;
            }
        }

        /// <summary>
        /// Remove an attached object from this ObjectiveInfo.
        /// </summary>
        public void removeAttachedObject()
        {
            if (attachedObject != null)
            {
                API.deleteEntity(attachedObject);
            }
        }
    }

    public class Objective : Script
    {
        private List<NetHandle> objectiveTargets;
        private List<NetHandle> objectiveVehicles;
        private List<ObjectiveInfo> objectives;
        private int objectiveCount;
        private int objectivesComplete;
        private bool checkingObjective;
        private bool pauseState;
        private DateTime objectiveCooldown;

        [Flags]
        public enum AnimationFlags
        {
            Loop = 1 << 0,
            StopOnLastFrame = 1 << 1,
            OnlyAnimateUpperBody = 1 << 4,
            AllowPlayerControl = 1 << 5,
            Cancellable = 1 << 7
        }

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
            checkingObjective = false;
            pauseState = false;
            objectiveTargets = new List<NetHandle>();
            objectiveVehicles = new List<NetHandle>();
            objectiveCooldown = DateTime.UtcNow;
            objectives = new List<ObjectiveInfo>();
        }

        /// <summary>
        /// Add new objective info to your Major Objective.
        /// </summary>
        /// <returns></returns>
        public ObjectiveInfo addEmptyObjectiveInfo()
        {
            ObjectiveInfo objInfo = new ObjectiveInfo();
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
        /// Syncs all objective into to a player.
        /// </summary>
        /// <param name="player"></param>
        public void syncObjectiveToPlayer(Client player)
        {
            Mission instance = API.getEntityData(player, "Mission");

            List<ObjectiveInfo> removeables = new List<ObjectiveInfo>();

            foreach (ObjectiveInfo objInfo in objectives)
            {
                if (objInfo.Type == ObjectiveTypes.SetIntoVehicle)
                {
                    instance.setPlayersIntoVehicles();
                    removeables.Add(objInfo);
                    continue;
                }

                API.triggerClientEvent(player, "Mission_New_Objective", objInfo.Location, objInfo.Type.ToString());
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
        /// Updates the objective progression for a player.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="location"></param>
        /// <param name="progression"></param>
        private void updateObjectiveProgression(Client player, Vector3 location, int progression)
        {
            Mission mission = API.getEntityData(player, "Mission");
            mission.updateObjectiveProgressionForAll(location, progression);
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

            Mission mission = API.getEntityData(player, "Mission");

            // Get the closest objective to the player.
            ObjectiveInfo closestObjective = null;
            foreach (ObjectiveInfo objInfo in objectives)
            {
                if (objInfo.Type == ObjectiveTypes.Teleport)
                {
                    closestObjective = objInfo;
                    break;
                }
                else
                {
                    // Capture / Location for On-Foot
                    if (objInfo.Type == ObjectiveTypes.Location || objInfo.Type == ObjectiveTypes.Capture)
                    {
                        if (player.position.DistanceTo(objInfo.Location) <= 5)
                        {
                            closestObjective = objInfo;
                            break;
                        }
                    }

                    // Capture / Location for In-Vehicle
                    if (objInfo.Type == ObjectiveTypes.VehicleCapture || objInfo.Type == ObjectiveTypes.VehicleLocation)
                    {
                        if (!player.isInVehicle)
                        {
                            continue;
                        }

                        if (player.position.DistanceTo(objInfo.Location) <= 5)
                        {
                            closestObjective = objInfo;
                            break;
                        }
                    }
                    
                    // Destroy Objective.
                    if (objInfo.Type == ObjectiveTypes.Destroy)
                    {
                        if (player.position.DistanceTo(objInfo.Location) >= 20)
                        {
                            continue;
                        }

                        if (player.isAiming)
                        {
                            closestObjective = objInfo;
                            break;
                        }
                    }

                    // Pickup Objective
                    if (objInfo.Type == ObjectiveTypes.PickupObject)
                    {
                        if (player.position.DistanceTo(objInfo.Location) >= 10)
                        {
                            continue;
                        }

                        if (player.position.DistanceTo(objInfo.Location) <= 2)
                        {
                            closestObjective = objInfo;
                            break;
                        }
                    }
                    
                    // Retrieve Vehicle
                    if (objInfo.Type == ObjectiveTypes.RetrieveVehicle)
                    {
                        if (player.isInVehicle)
                        {
                            closestObjective = objInfo;
                        }
                    }
                }
            }

            // If our closestObjective doesn't seem to exist, we'll just return.
            if (closestObjective == null)
            {
                return;
            }

            // Send our objective information out, and wait for it to finish or hit a dead end.
            checkForCompletion(player, closestObjective);

            // Check if our tuple returned false.
            if (!closestObjective.Status)
            {
                return;
            }

            // Get the players mission instance.
            mission.removeObjectiveForAll(closestObjective.Location);

            API.triggerClientEvent(player, "Mission_Head_Notification", "~b~Minor Objective Complete", "Objective");

            // Remove dead objectives.
            objectives.Remove(closestObjective);

            mission.setupTeamSync();

            // Check if all of our objectives are complete.
            if (objectives.Count >= 1)
            {
                return;
            }

            mission.goToNextObjective();

            pauseState = false;
        }

        /// <summary>
        /// Switches through objectives and determines which one to check, and checks for completion.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        private void checkForCompletion(Client player, ObjectiveInfo objInfo)
        {
            switch (objInfo.Type)
            {
                case ObjectiveTypes.Location:
                    objectiveLocation(player, objInfo);
                    return;
                case ObjectiveTypes.Teleport:
                    objectiveTeleport(player, objInfo);
                    return;
                case ObjectiveTypes.Capture:
                    objectiveCapture(player, objInfo);
                    return;
                case ObjectiveTypes.Destroy:
                    objectiveDestroy(player, objInfo);
                    return;
                case ObjectiveTypes.VehicleCapture:
                    objectiveVehicleCapture(player, objInfo);
                    return;
                case ObjectiveTypes.VehicleLocation:
                    objectiveVehicleLocation(player, objInfo);
                    return;
                case ObjectiveTypes.PickupObject:
                    objectivePickupObject(player, objInfo);
                    return;
                case ObjectiveTypes.RetrieveVehicle:
                    objectiveRetrieveVehicle(player, objInfo);
                    return;
            }
        }

        /// <summary>
        /// Objective of type location functionality.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        private void objectiveLocation(Client player, ObjectiveInfo objInfo)
        {
            if (player.position.DistanceTo(objInfo.Location) <= 5)
            {
                objInfo.Status = true;
            }
        }

        /// <summary>
        /// Objective of type Vehicle Location functionality.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        private void objectiveVehicleLocation(Client player, ObjectiveInfo objInfo)
        {
            if (!player.isInVehicle)
            {
                return;
            }

            NetHandle vehicle = API.getPlayerVehicle(player);

            if (!API.hasEntityData(vehicle, "Mission_UID"))
            {
                return;
            }

            if (API.getEntityData(vehicle, "Mission_UID") != objInfo.UniqueVehicleID)
            {
                return;
            }

            if (player.position.DistanceTo(objInfo.Location) <= 5)
            {
                objInfo.Status = true;
            }
        }

        /// <summary>
        /// Objective of type Teleport functionality.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        private void objectiveTeleport(Client player, ObjectiveInfo objInfo)
        {
            Mission mission = API.getEntityData(player, "Mission");
            mission.teleportAllPlayers(objInfo.Location);
            objInfo.Status = true;
        }

        /// <summary>
        /// Objective of type Capture functionality.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        private void objectiveCapture(Client player, ObjectiveInfo objInfo)
        {
            if (player.position.DistanceTo(objInfo.Location) <= 8)
            {
                if (!isCoolDownOver(player))
                {
                    return;
                }
                objInfo.Progress += 5;
                updateObjectiveProgression(player, objInfo.Location, objInfo.Progress);
                if (objInfo.Progress > 100)
                {
                    objInfo.Status = true;
                }
            }
        }

        /// <summary>
        /// Objective of type Destruction functionality.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        private void objectiveDestroy(Client player, ObjectiveInfo objInfo)
        {
            if (!player.isAiming)
            {
                return;
            }

            if (!isCoolDownOver(player))
            {
                return;
            }
            objInfo.Progress += 5;
            updateObjectiveProgression(player, objInfo.Location, objInfo.Progress);
            if (objInfo.Progress > 100)
            {
                objInfo.Status = true;
            }
        }

        /// <summary>
        /// Objective of type Pickup functionality.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        private void objectivePickupObject(Client player, ObjectiveInfo objInfo)
        {
            if (player.isInVehicle)
            {
                return;
            }

            if (player.position.DistanceTo(objInfo.Location) <= 3)
            {
                objInfo.Status = true;
                API.playPlayerAnimation(player, (int)(AnimationFlags.StopOnLastFrame), "pickup_object", "pickup_low");
                API.delay(1500, true, () =>
                {
                    API.stopPlayerAnimation(player);
                });
            }
        }

        /// <summary>
        /// Objective of type Vehicle Capture functionality.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        private void objectiveVehicleCapture(Client player, ObjectiveInfo objInfo)
        {
            if (!player.isInVehicle)
            {
                return;
            }

            NetHandle vehicle = API.getPlayerVehicle(player);

            if (!API.hasEntityData(vehicle, "Mission_UID"))
            {
                return;
            }

            if (API.getEntityData(vehicle, "Mission_UID") != objInfo.UniqueVehicleID)
            {
                return;
            }

            if (player.position.DistanceTo(objInfo.Location) <= 8)
            {
                if (!isCoolDownOver(player))
                {
                    return;
                }
                objInfo.Progress += 5;
                updateObjectiveProgression(player, objInfo.Location, objInfo.Progress);
                if (objInfo.Progress > 100)
                {
                    objInfo.Status = true;
                }
            }
        }

        /// <summary>
        /// Objective of type Retrieve Vehicle functionality.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        private void objectiveRetrieveVehicle(Client player, ObjectiveInfo objInfo)
        {
            if (!player.isInVehicle)
            {
                return;
            }

            NetHandle vehicle = API.getPlayerVehicle(player);

            if (!API.hasEntityData(vehicle, "Mission_UID"))
            {
                return;
            }

            if (API.getEntityData(vehicle, "Mission_UID") != objInfo.UniqueVehicleID)
            {
                return;
            }

            objInfo.Status = true;
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

        /// <summary>
        /// Used to set the player cooldown so they can't spam objectives.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private bool isCoolDownOver(Client player)
        {
            if (!API.hasEntityData(player, "Mission_Cooldown_Check"))
            {
                API.setEntityData(player, "Mission_Cooldown_Check", DateTime.Now.AddMilliseconds(3000));
            }

            DateTime lastCheck = API.getEntityData(player, "Mission_Cooldown_Check");
            DateTime now = DateTime.Now;
            if (now < lastCheck)
            {
                return false;
            }
            API.setEntityData(player, "Mission_Cooldown_Check", DateTime.Now.AddMilliseconds(3000));
            return true;
        }
    }
}
