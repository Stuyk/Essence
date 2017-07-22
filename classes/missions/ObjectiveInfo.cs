using Essence.classes.minigames;
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
    public class ObjectiveInfo : Script
    {
        private Mission mission;
        private int progress;
        private Vector3 location;
        private Vector3 direction;
        private Objective.ObjectiveTypes type;
        private bool completed;
        private Vector3 nullVector = new Vector3(0, 0, 0);
        private int uniqueID;
        private NetHandle attachedObject;
        private Objective objective;
        private NetHandle target;
        private Lockpick lockpick;
        private int objectiveID;

        public ObjectiveInfo() { }

        public ObjectiveInfo(Objective obj)
        {
            objective = obj;
            progress = 0;
            location = new Vector3();
            direction = new Vector3();
            type = Objective.ObjectiveTypes.None;
            completed = false;
            uniqueID = -1;
            objectiveID = new Random().Next(1, 999999);
        }

        /// <summary>
        /// Used by objInfo, needs to be assigned. Assigned on empty creation.
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

        public NetHandle PlayerTarget
        {
            set
            {
                target = value;
                if (!target.IsNull)
                {
                    API.setEntityData(target, "Mission_Hit", mission);
                }
            }
            get
            {
                return target;
            }
        }

        public int ObjectiveID
        {
            get
            {
                return objectiveID;
            }
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
                updateProgression();
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

        public Lockpick Lockpick
        {
            set
            {
                lockpick = value;
                lockpick.ObjectiveInformation = this;
            }
            get
            {
                return lockpick;
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

        public void attemptToVerify(Client player)
        {
            switch (type)
            {
                case Objective.ObjectiveTypes.Location:
                    Status = ObjectiveTypes.objectiveLocation(player, this);
                    break;
                case Objective.ObjectiveTypes.Capture:
                    Status = ObjectiveTypes.objectiveCapture(player, this);
                    break;
                case Objective.ObjectiveTypes.Destroy:
                    Status = ObjectiveTypes.objectiveDestroy(player, this);
                    break;
                case Objective.ObjectiveTypes.VehicleCapture:
                    Status = ObjectiveTypes.objectiveVehicleCapture(player, this);
                    break;
                case Objective.ObjectiveTypes.VehicleLocation:
                    Status = ObjectiveTypes.objectiveVehicleLocation(player, this);
                    break;
                case Objective.ObjectiveTypes.PickupObject:
                    Status = ObjectiveTypes.objectivePickupObject(player, this);
                    break;
                case Objective.ObjectiveTypes.RetrieveVehicle:
                    Status = ObjectiveTypes.objectiveRetrieveVehicle(player, this);
                    break;
                case Objective.ObjectiveTypes.BreakIntoVehicle:
                    Status = ObjectiveTypes.objectiveBreakIntoVehicle(player, this);
                    break;
            }
        }

        /// <summary>
        /// Used by Progression when it gets a value assigned to it.
        /// </summary>
        private void updateProgression()
        {
            mission.updateObjectiveProgressionForAll(location, progress);
        }
    }
}
