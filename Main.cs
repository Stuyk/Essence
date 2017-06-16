using Essence.classes;
using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes
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

        [Command("randomClothes")]
        public void randomClothes(Client player)
        {
            Player client = API.getEntityData(player, "Instance");
            API.setPlayerSkin(player, PedHash.FreemodeMale01);
            client.PlayerClothing.Top = new Random().Next(0, 50);
            client.PlayerClothing.Legs = new Random().Next(0, 50);
            client.PlayerClothing.Feet = new Random().Next(0, 50);

        }
    }
}
