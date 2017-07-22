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

namespace Essence.classes.utility
{
    public class SpawnInfo : Script
    {
        private Client player;
        private Vector3 position;
        private Vector3 rotation;
        private bool occupied;

        public SpawnInfo() { }

        public SpawnInfo(Vector3 pos, Vector3 rot)
        {
            position = pos;
            rotation = rot;
            occupied = false;
            player = null;
        }

        public bool Occupied
        {
            set
            {
                occupied = value;
            }
            get
            {
                return occupied;
            }
        }

        public Client Vehicle
        {
            set
            {
                player = value;
            }
            get
            {
                return player;
            }
        }

        public Vector3 Position
        {
            get
            {
                return position;
            }
        }

        public Vector3 Rotation
        {
            get
            {
                return rotation;
            }
        }

        public void checkOccupied()
        {
            if (player == null)
            {
                return;
            }

            if (!API.hasEntityData(player, "Mission"))
            {
                Occupied = false;
                player = null;
                return;
            }


            if (player.isInVehicle)
            {
                if (API.getEntityPosition(player.vehicle).DistanceTo(position) > 20)
                {
                    if (API.hasEntityData(player, "Mission"))
                    {
                        Occupied = false;
                        player = null;
                        return;
                    }
                }
            }
        }
    }
}
