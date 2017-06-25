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
            loadLocations();
            loadSpawns();
            setupBlip();
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

        private void setupBlip()
        {
            Blip blip = API.createBlip(startPoint);
            API.setBlipSprite(blip, 477);
            API.setBlipName(blip, "Short Range Trucking");
            API.setBlipColor(blip, 16);
            API.setBlipShortRange(blip, true);
        }

        [Command("startJobShortRange")]
        public void cmdStartTruckingJob(Client player)
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
            mission.MissionTitle = "Short Range Trucking";

            API.setEntitySyncedData(player, "Mission_New_Instance", true);
            API.setEntityData(player, "Mission", mission);
            mission.addPlayer(player);

            // Mission Framework
            Objective objective;
            // Queue System
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
            // Setup a unique ID for the vehicle.
            int unID = new Random().Next(1, 50000);
            // Set Our Start Location
            objective = mission.CreateNewObjective(openSpot.Position, Objective.ObjectiveTypes.VehicleLocation);
            // Add Our Random Mission Vehicle
            objective.addObjectiveVehicle(mission, openSpot.Position, VehicleHash.Youga2, rotation: openSpot.Rotation, uniqueID: unID);
            // Set into vehicle.
            objective.setupObjective(new Vector3(), Objective.ObjectiveTypes.SetIntoVehicle);
            // Add the unique id.
            objective.addUniqueIDToAllObjectives(unID);
            // Mid Point
            objective = mission.CreateNewObjective(midPoint, Objective.ObjectiveTypes.VehicleLocation);
            objective.addUniqueIDToAllObjectives(unID);
            // Pull a random location.
            int random = new Random().Next(0, locations.Count);
            objective = mission.CreateNewObjective(locations[random], Objective.ObjectiveTypes.VehicleCapture);
            objective.addUniqueIDToAllObjectives(unID);
            // Setup end point.
            objective = mission.CreateNewObjective(endPoint, Objective.ObjectiveTypes.VehicleLocation);
            objective.addUniqueIDToAllObjectives(unID);
            // Deliver truck back to end points
            objective = mission.CreateNewObjective(startPoint, Objective.ObjectiveTypes.VehicleLocation);
            objective.addUniqueIDToAllObjectives(unID);
            mission.startMission();
            
        }
    }
}
