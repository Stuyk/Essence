using Essence.classes.utility;
using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.jobs
{
    public class ShortRangeTrucking : Script
    {
        private Vector3 startPoint = new Vector3(-1328.738, -212.1859, 42.38577);
        private Vector3 midPoint = new Vector3(-1264.165, -306.5757, 36.02926);
        private Vector3 endPoint = new Vector3(-1263.66, -307.4156, 35.51007);
        private List<Vector3> locations = new List<Vector3>();
        private const int reward = 18;
        // For Spawns
        private DateTime lastTimeCheck = DateTime.Now;
        private List<SpawnInfo> spawns = new List<SpawnInfo>();

        public ShortRangeTrucking()
        {
            PointHelper.addNewPoint(3, 477, startPoint, "Short Range Trucking", true, "JOB_SHORT_RANGE_TRUCKING");
            loadLocations();
            loadSpawns();
            API.onUpdate += API_onUpdate;
        }

        private void API_onUpdate()
        {
            spawnChecker();
        }

        private void spawnChecker()
        {
            if (DateTime.Now > lastTimeCheck)
            {
                lastTimeCheck = DateTime.Now.AddMilliseconds(10000);
                foreach (SpawnInfo spawn in spawns)
                {
                    spawn.checkOccupied();
                }
            }
        }

        private void loadLocations()
        {
            locations = Utility.pullLocationsFromFile("resources/Essence/data/shortrangetrucking.txt");
        }

        private void loadSpawns()
        {
            List<Vector3> locs = Utility.pullLocationsFromFile("resources/Essence/data/shortrangetruckingspawns.txt");
            List<Vector3> rots = Utility.pullLocationsFromFile("resources/Essence/data/shortrangetruckingspawnsrotations.txt");

            int count = 0;

            foreach (Vector3 loc in locs)
            {
                SpawnInfo spawn = new SpawnInfo(loc, rots[count]);
                spawns.Add(spawn);
                count++;
            }
        }

        public void startShortRangeTruckingJob(Client player)
        {
            if (player.position.DistanceTo(startPoint) >= 5)
            {
                return;
            }

            Mission mission;

            if (API.hasEntityData(player, "Mission"))
            {
                mission = API.getEntityData(player, "Mission");
                if (mission.MissionObjectiveCount > 0)
                {
                    API.sendChatMessageToPlayer(player, "~r~You seem to already have a mission running.");
                    return;
                }
            } else {
                mission = new Mission();
            }

            // Basic Setup.
            mission.useTimer();
            mission.MissionTime = 60 * 5;
            mission.MissionReward = reward;
            mission.MissionTitle = "Short Range Trucking";

            API.setEntitySyncedData(player, "Mission_New_Instance", true);
            API.setEntityData(player, "Mission", mission);
            mission.addPlayer(player);

            // Queue System
            DateTime startTime = DateTime.Now;
            SpawnInfo openSpot = null;
            while (openSpot == null)
            {
                openSpot = Utility.findOpenSpawn(spawns, mission);

                if (openSpot != null)
                {
                    openSpot.Vehicle = player;
                    break;
                }
            }
            // Setup a unique ID for the vehicle we'll be using.
            int uniqueID = new Random().Next(1, 50000);
            // == First Major Objective
            // = Create an empty objective to house information. Create an empty ObjectiveInfo Add our first objective, and associate the Vehicle ID with our objective.
            Objective objective = mission.addEmptyObjective();

            objective.addObjectiveVehicle(mission, openSpot.Position, VehicleHash.Youga2, rotation: openSpot.Rotation, uniqueID: uniqueID);

            ObjectiveInfo objectiveInfo = objective.addEmptyObjectiveInfo();
            objectiveInfo.Location = openSpot.Position;
            objectiveInfo.Type = Objective.ObjectiveTypes.RetrieveVehicle;
            objectiveInfo.UniqueVehicleID = uniqueID;

            objectiveInfo = objective.addEmptyObjectiveInfo();
            NetHandle boxObject = API.createObject(-719727517, new Vector3(-1321.264, -253.4067, 41.13453), new Vector3());
            objectiveInfo.Location = new Vector3(-1321.264, -253.4067, 41.13453);
            objectiveInfo.Type = Objective.ObjectiveTypes.PickupObject;
            objectiveInfo.AddObject = boxObject;

            // == Second Major Objective
            objective = mission.addEmptyObjective();
            objectiveInfo = objective.addEmptyObjectiveInfo();
            objectiveInfo.Location = midPoint;
            objectiveInfo.Type = Objective.ObjectiveTypes.VehicleLocation;
            objectiveInfo.UniqueVehicleID = uniqueID;

            // == Third Major Objective
            int missionIndex = new Random().Next(0, locations.Count);
            objective = mission.addEmptyObjective();
            objectiveInfo = objective.addEmptyObjectiveInfo();
            objectiveInfo.Location = locations[missionIndex];
            objectiveInfo.Type = Objective.ObjectiveTypes.VehicleCapture;
            objectiveInfo.UniqueVehicleID = uniqueID;

            // == Fourth Major Objective
            objective = mission.addEmptyObjective();
            objectiveInfo = objective.addEmptyObjectiveInfo();
            objectiveInfo.Location = endPoint;
            objectiveInfo.Type = Objective.ObjectiveTypes.VehicleLocation;
            objectiveInfo.UniqueVehicleID = uniqueID;

            // == Fifth Major Objective
            objective = mission.addEmptyObjective();
            objectiveInfo = objective.addEmptyObjectiveInfo();
            objectiveInfo.Location = startPoint;
            objectiveInfo.Type = Objective.ObjectiveTypes.VehicleLocation;
            objectiveInfo.UniqueVehicleID = uniqueID;
            
            // == Start Mission
            mission.startMission();
        }
    }
}
