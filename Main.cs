using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disorder_District
{
    public class Main : Script
    {
        public Main()
        {
            API.setGamemodeName("~w~Essence Pure Roleplay");
            API.setServerName("~w~Essence ~b~Pure ~b~Roleplay");
        }

        [Command("car")]
        public void cmdCar(Client player, VehicleHash car)
        {
            if (API.hasEntityData(player, "LastCar"))
            {
                API.deleteEntity(API.getEntityData(player, "LastCar"));
            }

            NetHandle vehicle = API.createVehicle(car, player.position, new Vector3(), 0, 0);
            API.setEntityData(player, "LastCar", vehicle);
            API.setPlayerIntoVehicle(player, vehicle, -1);
        }
    }
}
