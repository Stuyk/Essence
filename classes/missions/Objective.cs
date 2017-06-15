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

        // What to assign to this class when it's created as a new instance.
        public ObjectiveInfo()
        {
            progress = 0;
            location = new Vector3();
            direction = new Vector3();
            type = Objective.ObjectiveTypes.None;
            completed = false;
            
        }

        /** Get / Set the current progress of this objective. */
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

        /** Get / Set the current location of this objective. */
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

        /** Get / Set the current type of this objective. */
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

        /** Set the current completion status for this objective. */
        public bool Status
        {
            get
            {
                return completed;
            }
            set
            {
                completed = value;
            }
        }

        /** Set the current direction for this objective. */
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
    }

    public class Objective : Script
    {
        private List<NetHandle> objectiveTargets;
        private List<NetHandle> objectiveVehicles;
        //private Dictionary<Vector3, int> objectiveProgression;
        //private Dictionary<Vector3, ObjectiveTypes> objectives;

        private List<ObjectiveInfo> objectives;


        private int objectiveCount;
        private int objectivesComplete;
        private bool checkingObjective;
        private bool pauseState;
        private DateTime objectiveCooldown;

        public enum ObjectiveTypes
        {
            Location,
            Capture,
            Teleport,
            Destroy,
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
            //objectiveProgression = new Dictionary<Vector3, int>();
            objectiveCooldown = DateTime.UtcNow;
            objectives = new List<ObjectiveInfo>();
            //objectives = new Dictionary<Vector3, ObjectiveTypes>();
        }

        /**
         * Setup a new objective.
         * */
        public void setupObjective(Vector3 location, ObjectiveTypes type, Vector3 direction = null)
        {
            ObjectiveInfo objInfo = new ObjectiveInfo();
            objInfo.Location = location;
            objInfo.Type = type;
            objInfo.Progress = 0;
            objectives.Add(objInfo);

            if (direction != null)
            {
                objInfo.Direction = direction;
            }
        }

        public void addObjectiveVehicle(Mission instance, Vector3 location, VehicleHash type)
        {
            NetHandle veh = API.createVehicle(type, location.Around(5), new Vector3(), 52, 52);
            API.setEntityData(veh, "Mission", instance);
            instance.addVehicle(veh);
        }

        public void syncObjectiveToPlayer(Client player)
        {
            foreach (ObjectiveInfo objInfo in objectives)
            {
                API.triggerClientEvent(player, "Mission_New_Objective", objInfo.Location, objInfo.Type.ToString());
            }

            /*
            foreach (Vector3 location in objectives.Keys)
            {
                API.triggerClientEvent(player, "Mission_New_Objective", location, objectives[location].ToString());
            }*/

            API.triggerClientEvent(player, "Mission_Setup_Objectives");
            API.triggerClientEvent(player, "Mission_Head_Notification", "~o~New Objective", "NewObjective");
        }

        private void updateObjectiveProgression(Client player, Vector3 location, int progression)
        {
            Mission mission = API.getEntityData(player, "Mission");
            mission.updateObjectiveProgressionForAll(location, progression);
        }

        public void verifyObjective(Client player)
        {
            Thread thread = new Thread(() =>
            {
                // Just to prevent an error from occuring where too many requests get sent. */
                if (objectives.Count <= 0)
                {
                    return;
                }

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
                        if (objInfo.Type == ObjectiveTypes.Location || objInfo.Type == ObjectiveTypes.Capture)
                        {
                            if (player.position.DistanceTo(objInfo.Location) <= 5)
                            {
                                closestObjective = objInfo;
                                break;
                            }
                        }

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
                Mission mission = API.getEntityData(player, "Mission");
                mission.removeObjectiveForAll(closestObjective.Location);

                API.triggerClientEvent(player, "Mission_Head_Notification", "~b~Minor Objective Complete", "Objective");

                // Remove dead objectives.
                objectives.Remove(closestObjective);

                // Check if all of our objectives are complete.
                if (objectives.Count >= 1)
                {
                    return;
                }

                mission.goToNextObjective();

                pauseState = false;
            });
            thread.Start();
        }

        /*********************************************
         * Verify based on objective type.
         * ******************************************/
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
            }
        }

        /*************************************************
         *                OBJECTIVE TYPES
         * **********************************************/
        private void objectiveLocation(Client player, ObjectiveInfo objInfo)
        {
            if (player.position.DistanceTo(objInfo.Location) <= 5)
            {
                objInfo.Status = true;
            }
        }

        private void objectiveTeleport(Client player, ObjectiveInfo objInfo)
        {
            Mission mission = API.getEntityData(player, "Mission");
            mission.teleportAllPlayers(objInfo.Location);
            objInfo.Status = true;
        }

        private void objectiveCapture(Client player, ObjectiveInfo objInfo)
        {
            if (player.position.DistanceTo(objInfo.Location) <= 8)
            {
                double sinceWhen = objectiveCooldown.TimeOfDay.TotalSeconds;
                double timeNow = DateTime.UtcNow.TimeOfDay.TotalSeconds;
                if (sinceWhen + 3 > timeNow)
                {
                    return;
                }
                objectiveCooldown = DateTime.UtcNow;
                objInfo.Progress += 5;
                updateObjectiveProgression(player, objInfo.Location, objInfo.Progress);
                if (objInfo.Progress > 100)
                {
                    objInfo.Status = true;
                }
            }
        }

        private void objectiveDestroy(Client player, ObjectiveInfo objInfo)
        {
            if (!player.isAiming)
            {
                return;
            }

            double sinceWhen = objectiveCooldown.TimeOfDay.TotalMilliseconds;
            double timeNow = DateTime.UtcNow.TimeOfDay.TotalMilliseconds;
            if (sinceWhen + 100 > timeNow)
            {
                return;
            }
            objectiveCooldown = DateTime.UtcNow;
            objInfo.Progress += 5;
            updateObjectiveProgression(player, objInfo.Location, objInfo.Progress);
            if (objInfo.Progress > 100)
            {
                objInfo.Status = true;
            }
        }

        /** Get the total objective count. */
        public int ObjectiveCount
        {
            get
            {
                return objectiveCount;
            }
        }

        /** Get the total amount of objectives complete. */
        public int CompletedObjectives
        {
            get
            {
                return objectivesComplete;
            }
        }
    }
}
