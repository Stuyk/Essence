using Essence.classes.missions;
using Essence.classes.utility;
using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.jobs
{
    public class ChopShopSpawn : Script
    {
        private Vector3 location;
        private Vector3 rotation;

        public ChopShopSpawn() { }

        public ChopShopSpawn(Vector3 loc, Vector3 rot)
        {
            location = loc;
            rotation = rot;
        }

        public Vector3 Location
        {
            get
            {
                return location;
            }
        }

        public Vector3 Rotation
        {
            get
            {
                return rotation;
            }
        }
    }

    public class ChopShop : Script
    {
        private Vector3 startPoint = new Vector3(1569.829, -2130.04, 77.33018);
        private Vector3 midPoint = new Vector3(1291.224, -2027.781, 43.06142);
        private Vector3 endPoint = new Vector3(1525.526, -2113.712, 74.92342);
        private List<ChopShopSpawn> locations = new List<ChopShopSpawn>();
        private List<string> vehicleList = new List<string>();
        private const int reward = 250;
        // For Spawns
        private DateTime lastTimeCheck = DateTime.Now;
        private List<SpawnInfo> spawns = new List<SpawnInfo>();

        public ChopShop()
        {
            generatePoint();
            loadSpawns();
            loadCars();
        }

        private void generatePoint()
        {
            PointInfo point = PointHelper.addNewPoint();
            point.BlipColor = 3;
            point.BlipType = 477;
            point.Text = "Chop Shop - Cars for Cash";
            point.DrawLabel = true;
            point.ID = "JOB_CHOP_SHOP";
            point.InteractionEnabled = true;
            point.Position = startPoint;
        }

        private void loadSpawns()
        {
            List<Vector3> locs = Utility.pullLocationsFromFile("resources/Essence/data/chopshoppositions.txt");
            List<Vector3> rots = Utility.pullLocationsFromFile("resources/Essence/data/chopshoprotations.txt");

            int count = 0;

            foreach (Vector3 location in locs)
            {
                ChopShopSpawn spawn = new ChopShopSpawn(location, rots[count]);
                locations.Add(spawn);
                count++;
            }

            API.consoleOutput("Loaded Chop Shop: " + count.ToString());
        }

        private void loadCars()
        {
            vehicleList = Utility.pullTypesFromFile("resources/Essence/data/chopshopvehicletypes.txt");
        }

        public void startChopShopJob(Client player)
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
                API.setEntitySyncedData(player, "Mission_New_Instance", true);
                API.setEntityData(player, "Mission", mission);
                mission.addPlayer(player);
            }
            else
            {
                mission = new Mission();
                API.setEntitySyncedData(player, "Mission_New_Instance", true);
                API.setEntityData(player, "Mission", mission);
                mission.addPlayer(player);
            }

            // Basic Setup.
            mission.useTimer();
            mission.MissionTime = 90;
            mission.MissionReward = reward;
            mission.MissionTitle = "Chop Shop";
            mission.RemoveFromMissionOnDeath = true;

            StashInfo stash = StashManager.getStashInfoByID(1);

            if (stash == null)
            {
                API.sendChatMessageToPlayer(player, "~r~Something went wrong with stashes. Contact an administrator.");
                return;
            }

            mission.AttachStashInfo = stash;

            // Setup a unique ID for the vehicle we'll be using.
            int uniqueID = new Random().Next(1, 50000);
            // == First Major Objective
            // = Create an empty objective to house information. Create an empty ObjectiveInfo Add our first objective, and associate the Vehicle ID with our objective.
            Objective objective = mission.addEmptyObjective(mission);
            ObjectiveInfo objectiveInfo = objective.addEmptyObjectiveInfo();
            objectiveInfo.Location = midPoint;
            objectiveInfo.Type = Objective.ObjectiveTypes.Location;
            objectiveInfo.Lockpick = new minigames.Lockpick();

            // == Second Major Objective
            int missionIndex = new Random().Next(0, locations.Count);
            int carIndex = new Random().Next(0, vehicleList.Count);
            Vector3 location = locations[missionIndex].Location;
            Vector3 rotation = locations[missionIndex].Rotation;
            objective = mission.addEmptyObjective(mission);
            objectiveInfo = objective.addEmptyObjectiveInfo();
            objectiveInfo.Location = location;
            objectiveInfo.Type = Objective.ObjectiveTypes.BreakIntoVehicle;
            objectiveInfo.Lockpick = new minigames.Lockpick();
            NetHandle vehicle = objective.addObjectiveVehicle(mission, location.Add(new Vector3(0, 0, 0.2)), API.vehicleNameToModel(vehicleList[carIndex]), rotation, uniqueID);
            API.setVehiclePrimaryColor(vehicle, new Random().Next(0, 159));
            API.setVehicleSecondaryColor(vehicle, new Random().Next(0, 159));

            API.setEntityPositionFrozen(vehicle, true);
            API.delay(5000, true, () =>
            {
                API.setVehicleLocked(vehicle, true);
                API.setVehicleEngineStatus(vehicle, false);
                API.setEntityPositionFrozen(vehicle, false);
            });
            
            // == Third Major Objective - Take Vehicle
            objective = mission.addEmptyObjective(mission);
            objectiveInfo = objective.addEmptyObjectiveInfo();
            objectiveInfo.Location = location;
            API.sendChatMessageToPlayer(player, "/gopos" + location.ToString());
            objectiveInfo.Type = Objective.ObjectiveTypes.RetrieveVehicle;
            objectiveInfo.UniqueVehicleID = uniqueID;
            objectiveInfo = objective.addEmptyObjectiveInfo();
            objectiveInfo.Type = Objective.ObjectiveTypes.UnlockVehicles;
            objectiveInfo.Location = new Vector3();
            objectiveInfo.UniqueVehicleID = uniqueID;

            // == Parse End Point
            objective = mission.addEmptyObjective(mission);
            objectiveInfo = objective.addEmptyObjectiveInfo();
            objectiveInfo.Location = endPoint;
            objectiveInfo.Type = Objective.ObjectiveTypes.VehicleLocation;
            objectiveInfo.UniqueVehicleID = uniqueID;

            // == Start Mission
            
            mission.startMission();
        }
    }
}
