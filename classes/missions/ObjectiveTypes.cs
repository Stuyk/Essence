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
    public static class ObjectiveTypes
    {
        [Flags]
        public enum AnimationFlags
        {
            Loop = 1 << 0,
            StopOnLastFrame = 1 << 1,
            OnlyAnimateUpperBody = 1 << 4,
            AllowPlayerControl = 1 << 5,
            Cancellable = 1 << 7
        }

        /// <summary>
        /// Objective of type location functionality.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        public static bool objectiveLocation(Client player, ObjectiveInfo objInfo)
        {
            if (player.position.DistanceTo(objInfo.Location) <= 5)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Objective of type Vehicle Location functionality.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        public static bool objectiveVehicleLocation(Client player, ObjectiveInfo objInfo)
        {
            if (!player.isInVehicle)
            {
                return false;
            }

            if (!player.vehicle.hasData("Mission_UID"))
            {
                return false;
            }

            if (player.vehicle.getData("Mission_UID") != objInfo.UniqueVehicleID)
            {
                return false;
            }

            if (player.position.DistanceTo(objInfo.Location) >= 5)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Objective of type Destruction functionality.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        public static bool objectiveDestroy(Client player, ObjectiveInfo objInfo)
        {
            if (!player.isAiming)
            {
                return false;
            }

            if (!isCoolDownOver(player))
            {
                return false;
            }

            objInfo.Progress += 5;

            if (objInfo.Progress < 100)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Objective of type Retrieve Vehicle functionality.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        public static bool objectiveRetrieveVehicle(Client player, ObjectiveInfo objInfo)
        {
            if (!player.isInVehicle)
            {
                return false;
            }

            if (!player.vehicle.hasData("Mission_UID"))
            {
                return false;
            }

            if (player.vehicle.getData("Mission_UID") != objInfo.UniqueVehicleID)
            {
                return false;
            }

            objInfo.Status = true;
            return true;
        }

        /// <summary>
        /// Objective of type Vehicle Capture functionality.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        public static bool objectiveVehicleCapture(Client player, ObjectiveInfo objInfo)
        {
            if (!player.isInVehicle)
            {
                return false;
            }
            
            if (!player.vehicle.hasData("Mission_UID"))
            {
                return false;
            }

            if (player.vehicle.getData("Mission_UID") != objInfo.UniqueVehicleID)
            {
                return false;
            }

            if (!isCloseToObjective(player, objInfo.Location, 8))
            {
                return false;
            }

            if (!isCoolDownOver(player))
            {
                return false;
            }

            objInfo.Progress += 5;

            if (objInfo.Progress < 100)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Objective of type player Capture functionality.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        public static bool objectiveCapture(Client player, ObjectiveInfo objInfo)
        {
            if (!isCloseToObjective(player, objInfo.Location, 8))
            {
                return false;
            }

            if (!isCoolDownOver(player))
            {
                return false;
            }

            objInfo.Progress += 5;

            if (objInfo.Progress < 100)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Used for breaking into vehicles. Acts as a 'capture' point.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        /// <returns></returns>
        public static bool objectiveBreakIntoVehicle(Client player, ObjectiveInfo objInfo)
        {
            if (!isCloseToObjective(player, objInfo.Location, 5))
            {
                return false;
            }

            if (objInfo.Lockpick.GameInfo.Score >= 100)
            {
                objInfo.Status = true;
                return true;
            }

            API.shared.triggerClientEvent(player, "Start_Lock_Pick_Minigame");
            return false;
        }

        /// <summary>
        /// Objective of type Pickup functionality.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="objInfo"></param>
        public static bool objectivePickupObject(Client player, ObjectiveInfo objInfo)
        {
            if (player.isInVehicle)
            {
                return false;
            }

            if (!isCloseToObjective(player, objInfo.Location, 4))
            {
                return false;
            }

            player.playAnimation("pickup_object", "pickup_low", (int)(AnimationFlags.StopOnLastFrame));

            API.shared.delay(1500, true, () =>
            {
                player.stopAnimation();
            });
            
            return true;
        }

        /// <summary>
        /// Used to check if the player's objective cooldown is over.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private static bool isCoolDownOver(Client player)
        {
            if (!player.hasData("Mission_Cooldown_Check"))
            {
                player.setData("Mission_Cooldown_Check", DateTime.Now.AddMilliseconds(3000));
            }

            DateTime now = DateTime.Now;
            if (now < player.getData("Mission_Cooldown_Check"))
            {
                return false;
            }

            player.setData("Mission_Cooldown_Check", DateTime.Now.AddMilliseconds(3000));
            return true;
        }

        /// <summary>
        /// Used to determine if the player is close to the objective.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="target"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        private static bool isCloseToObjective(Client player, Vector3 target, int distance)
        {
            if (player.position.DistanceTo(target) <= distance)
            {
                return true;
            }
            return false;
        }
    }
}
