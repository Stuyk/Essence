using Essence.classes.missions;
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
    public class LongRangeTrucking : Script
    {
        private Vector3 startPoint = new Vector3(-1124.136, -2164.126, 12.39399);
        private Vector3 truckSpawn = new Vector3(-1097.77, -2216.578, 12.32819);
        private Vector3 trailerSpawn = new Vector3(-1079.76, -2153.527, 12.48212);

        private Vector3 midPoint = new Vector3(-2737.794, 2248.115, 20.26052);
        private Vector3 endPoint = new Vector3(-1097.77, -2216.578, 12.32819);
        private List<Vector3> locations = new List<Vector3>();
        private List<SpawnInfo> spawns = new List<SpawnInfo>();
        private const int reward = 125;

        private DateTime lastTimeCheck = DateTime.Now;

        public LongRangeTrucking()
        {
            generatePoint();
            loadLocations();
            loadSpawns();
            API.onUpdate += API_onUpdate;
        }

        private void generatePoint()
        {
            PointInfo point = PointHelper.addNewPoint();
            point.BlipColor = 3;
            point.BlipType = 477;
            point.Text = "Long Range Trucking";
            point.DrawLabel = true;
            point.ID = "JOB_LONG_RANGE_TRUCKING";
            point.InteractionEnabled = true;
            point.Position = startPoint;
        }

        private void API_onUpdate()
        {
            truckHelper();
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

        private void truckHelper()
        {
            List<NetHandle> trucks = API.getAllVehicles();
            foreach (NetHandle truck in trucks)
            {
                if (!API.hasEntityData(truck, "Mission_Truck_Trailer"))
                {
                    continue;
                }

                if (!API.hasEntityData(truck, "Mission_Truck_Trailer_Attachment"))
                {
                    continue;
                }

                NetHandle trailer = API.getEntityData(truck, "Mission_Truck_Trailer_Attachment");

                if (API.hasEntityData(truck, "Mission_Truck_Sync_Time") && API.hasEntityData(truck, "Mission_Truck_Sync_Distance"))
                {
                    DateTime lastTime = API.getEntityData(truck, "Mission_Truck_Sync_Time");
                    Vector3 lastPos = API.getEntityData(truck, "Mission_Truck_Sync_Distance");
                    if (DateTime.Now < lastTime)
                    {
                        return;
                    }

                    if (API.getEntityPosition(truck).DistanceTo(lastPos) < 50)
                    {
                        return;
                    }
                    API.setEntityData(truck, "Mission_Truck_Sync_Time", DateTime.Now.AddMilliseconds(10000));
                    API.setEntityData(truck, "Mission_Truck_Sync_Distance", API.getEntityPosition(truck));
                    API.setEntityPosition(trailer, API.getEntityPosition(truck));
                    API.setEntityRotation(trailer, API.getEntityRotation(truck));
                    API.sendNativeToAllPlayers((long)Hash.ATTACH_VEHICLE_TO_TRAILER, truck, trailer, 1000f);
                }
            }
        }

        private void loadLocations()
        {
            locations = Utility.pullLocationsFromFile("resources/Essence/data/longrangetrucking.txt");
        }

        // This generates spawns based on every other line. First Line Pos, Second Line Rotation, Reset.
        private void loadSpawns()
        {
            List<Vector3> locs = Utility.pullLocationsFromFile("resources/Essence/data/longrangetruckingspawns.txt");
            List<Vector3> rots = Utility.pullLocationsFromFile("resources/Essence/data/longrangetruckingspawnsrotations.txt");

            int count = 0;

            foreach (Vector3 loc in locs)
            {
                SpawnInfo spawn = new SpawnInfo(loc, rots[count]);
                spawns.Add(spawn);
                count++;
            }
        }

        [Command("getmethere")]
        public void cmdgjlkjsa(Client player)
        {
            API.setEntityPosition(player, startPoint);
        }

        public void startLongRangeTruckingJob(Client player)
        {
            if (player.position.DistanceTo(startPoint) >= 5)
            {
                return;
            }

            if (API.hasEntityData(player, "Mission"))
            {
                API.sendChatMessageToPlayer(player, "~r~You're already in a party, ~w~/leaveparty ~r~if you want to abandon your allies.");
                return;
            }

            // Basic Setup.
            Mission mission = new Mission();
            mission.MissionReward = reward;
            mission.MissionTitle = "Long Distance Trucking";

            API.setEntitySyncedData(player, "Mission_New_Instance", true);
            API.setEntityData(player, "Mission", mission);
            mission.addPlayer(player);

            // Try and find an open spot.
            SpawnInfo openSpot = null;
            while (openSpot == null)
            {
                foreach (SpawnInfo spawn in spawns)
                {
                    if (!spawn.Occupied)
                    {
                        openSpot = spawn;
                        openSpot.Occupied = true;
                        openSpot.Vehicle = player;
                        break;
                    }
                }

                if (openSpot != null)
                {
                    break;
                }
            }

            // Generate a unique ID for our objectives.
            int uniqueVehicleID = new Random().Next(1, 20000);

            // 1 - Major Objective - Go to the vehicle.
            Objective objective = mission.addEmptyObjective(mission);
            ObjectiveInfo objectiveInfo = objective.addEmptyObjectiveInfo();
            objectiveInfo.Location = openSpot.Position;
            objectiveInfo.Type = Objective.ObjectiveTypes.RetrieveVehicle;
            objectiveInfo.UniqueVehicleID = uniqueVehicleID;
            NetHandle vehicle = objective.addObjectiveVehicle(mission, openSpot.Position, VehicleHash.Packer, openSpot.Rotation, uniqueID: uniqueVehicleID);
            API.delay(20000, true, () =>
            {
                NetHandle trailer = objective.addObjectiveVehicle(mission, openSpot.Position.Add(new Vector3(0, 0, 0)), VehicleHash.Trailers, openSpot.Rotation, uniqueID: uniqueVehicleID);
            });
            
            // All of this will go away on Client Update.
            //API.setEntityData(vehicle, "Mission_Truck_Trailer", true);
            //API.setEntityData(vehicle, "Mission_Truck_Sync_Time", DateTime.Now.AddSeconds(3));
            //API.setEntityData(vehicle, "Mission_Truck_Sync_Distance", truckSpawn);
           // API.setEntityData(vehicle, "Mission_Truck_Trailer_Attachment", trailer);

            // 2 - Major Objective - Drive Vehicle Out
            objective = mission.addEmptyObjective(mission);
            objectiveInfo = objective.addEmptyObjectiveInfo();
            objectiveInfo.Location = new Vector3(-1080.812, -2231.04, 12.25332);
            objectiveInfo.Type = Objective.ObjectiveTypes.VehicleLocation;
            objectiveInfo.UniqueVehicleID = uniqueVehicleID;

            // 3 - Major Objective - Halfway Point
            objective = mission.addEmptyObjective(mission);
            objectiveInfo = objective.addEmptyObjectiveInfo();
            objectiveInfo.Location = midPoint;
            objectiveInfo.Type = Objective.ObjectiveTypes.VehicleLocation;
            objectiveInfo.UniqueVehicleID = uniqueVehicleID;

            // 4 - Major Objective - Get Random Point
            int locationIndex = new Random().Next(0, locations.Count);
            objective = mission.addEmptyObjective(mission);
            objectiveInfo = objective.addEmptyObjectiveInfo();
            objectiveInfo.Location = locations[locationIndex];
            objectiveInfo.Type = Objective.ObjectiveTypes.VehicleCapture;
            objectiveInfo.UniqueVehicleID = uniqueVehicleID;

            // 5 - Major Objective - Go back to depot.
            objective = mission.addEmptyObjective(mission);
            objectiveInfo = objective.addEmptyObjectiveInfo();
            objectiveInfo.Location = endPoint;
            objectiveInfo.Type = Objective.ObjectiveTypes.VehicleLocation;
            objectiveInfo.UniqueVehicleID = uniqueVehicleID;

            // 6 - Start the mission.
            mission.startMission();
        }
    }
}
