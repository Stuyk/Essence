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

        public ShortRangeTrucking()
        {
            loadLocations();
            setupBlip();
        }

        private void loadLocations()
        {
            locations = Utility.pullLocationsFromFile("resources/Essence/data/shortrangetrucking.txt");
        }

        private void setupBlip()
        {
            Blip blip = API.createBlip(startPoint);
            API.setBlipSprite(blip, 477);
            API.setBlipName(blip, "Short Range Trucking");
            API.setBlipColor(blip, 16);
            API.setBlipShortRange(blip, true);
        }

        [Command("startJob")]
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
            // Set Our Start Location
            objective = mission.CreateNewObjective(startPoint, Objective.ObjectiveTypes.Location);
            // Add Our Random Mission Vehicle
            int random = new Random().Next(1, 5);
            switch (random)
            {
                case 1:
                    objective.addObjectiveVehicle(mission, startPoint, VehicleHash.Rumpo);
                    break;
                case 2:
                    objective.addObjectiveVehicle(mission, startPoint, VehicleHash.Rumpo2);
                    break;
                case 3:
                    objective.addObjectiveVehicle(mission, startPoint, VehicleHash.Rumpo3);
                    break;
                case 4:
                    objective.addObjectiveVehicle(mission, startPoint, VehicleHash.Burrito);
                    break;
                case 5:
                    objective.addObjectiveVehicle(mission, startPoint, VehicleHash.Burrito3);
                    break;
            }
            objective = mission.CreateNewObjective(midPoint, Objective.ObjectiveTypes.Location);
            objective.setupObjective(new Vector3(), Objective.ObjectiveTypes.SetIntoVehicle);
            // Tell our player to get to the mid point.
            
            // Pull a random location.
            random = new Random().Next(0, locations.Count);
            objective = mission.CreateNewObjective(locations[random], Objective.ObjectiveTypes.Capture);
            // Setup end point.
            objective = mission.CreateNewObjective(endPoint, Objective.ObjectiveTypes.Location);
            // Deliver truck back to end points
            objective = mission.CreateNewObjective(startPoint, Objective.ObjectiveTypes.Location);
            mission.startMission();
            
        }
    }
}
