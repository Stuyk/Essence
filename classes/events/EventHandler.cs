using Essence.classes.commands;
using Essence.classes.inventory;
using Essence.classes.minigames;
using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.events
{
    public class EventHandler : Script
    {
        public EventHandler()
        {
            API.onClientEventTrigger += API_onClientEventTrigger;
        }

        private void API_onClientEventTrigger(Client player, string eventName, params object[] arguments)
        {
            // Login Switch
            switch (eventName)
            {
                case "clientLogin":
                    if (arguments.Length <= 0)
                    {
                        return;
                    }
                    API.call("Login", "cmdLogin", player, arguments[0].ToString(), arguments[1].ToString());
                    return;
                case "clientRegister":
                    if (arguments.Length <= 0)
                    {
                        return;
                    }
                    API.call("Register", "cmdRegister", player, arguments[0].ToString(), arguments[1].ToString());
                    return;
                default:
                    break;
            }

            // Mission Switch
            if (API.hasEntityData(player, "Mission"))
            {
                Mission mission = API.getEntityData(player, "Mission");

                switch (eventName)
                {
                    case "checkObjective":
                        mission.verifyObjective(player);
                        return;
                }
            }

            if (API.hasEntityData(player, "Minigame"))
            {
                switch(eventName)
                {
                    case "Check_Lockpick_Score":
                        if (player.hasData("Minigame"))
                        {
                            Lockpick minigame = API.getEntityData(player, "Minigame");
                            minigame.checkScore(player, Convert.ToInt32(arguments[0]));
                        }
                        return;
                }
            }

            // In Vehicle Switch
            if (player.isInVehicle)
            {
                switch (eventName)
                {
                    case "Vehicle_Engine":
                        API.call("VehicleCommands", "ToggleEngine", player);
                        return;
                    case "Vehicle_Trunk":
                        API.call("VehicleCommands", "ToggleTrunk", player);
                        return;
                    case "Vehicle_Hood":
                        API.call("VehicleCommands", "ToggleHood", player);
                        return;
                    case "Vehicle_Door_0":
                        API.call("VehicleCommands", "ToggleDoor", player, 0);
                        return;
                    case "Vehicle_Door_1":
                        API.call("VehicleCommands", "ToggleDoor", player, 1);
                        return;
                    case "Vehicle_Door_2":
                        API.call("VehicleCommands", "ToggleDoor", player, 2);
                        return;
                    case "Vehicle_Door_3":
                        API.call("VehicleCommands", "ToggleDoor", player, 3);
                        return;
                    case "Vehicle_Windows_Up":
                        API.call("VehicleCommands", "WindowState", player, false);
                        return;
                    case "Vehicle_Windows_Down":
                        API.call("VehicleCommands", "WindowState", player, true);
                        return;
                }
            }

            // Out of Vehicle Switch
            if (!player.isInVehicle)
            {
                switch (eventName)
                {
                    case "No_Label":
                        API.sendChatMessageToPlayer(player, "You didn't specify an ID for this point.");
                        return;
                    // ==========================
                    // SHOPS && JOBS
                    // ==========================
                    case "SHOP_ATM":
                        // !!! IMPLEMENT PLS !!!
                        API.sendChatMessageToPlayer(player, "Don't forget to implement this shit.");
                        return;
                    case "SHOP_BARBER":
                        API.call("Barber", "startBarberShop", player);
                        return;
                    case "SHOP_MASK":
                        API.call("Mask", "startMaskShop", player);
                        return;
                    case "SHOP_TATTOO":
                        API.call("Tattoo", "startTattooShop", player);
                        return;
                    case "JOB_LONG_RANGE_TRUCKING":
                        API.call("LongRangeTrucking", "startLongRangeTruckingJob", player);
                        return;
                    case "JOB_SHORT_RANGE_TRUCKING":
                        API.call("ShortRangeTrucking", "startShortRangeTruckingJob", player);
                        return;
                    case "JOB_CHOP_SHOP":
                        API.call("ChopShop", "startChopShopJob", player);
                        return;
                    // ==========================
                    // ANIMATION MENU
                    // ==========================
                    case "ANIM_SURRENDER":
                        PlayerCommands.playAnimation(player, "busted", "idle_2_hands_up", lastFrame: true);
                        return;
                    case "ANIM_HANDS_UP":
                        PlayerCommands.playAnimation(player, "mp_am_hold_up", "guard_handsup_loop", upperLoop: true);
                        return;
                    case "ANIM_CROUCH":
                        PlayerCommands.playAnimation(player, "move_crouch_proto", "idle", loop: true);
                        return;
                    case "ANIM_NOD_NO":
                        PlayerCommands.playAnimation(player, "gestures@m@standing@casual", "gesture_nod_no_hard", justUpper: true);
                        return;
                    case "ANIM_NOD_YES":
                        PlayerCommands.playAnimation(player, "gestures@f@standing@casual", "gesture_nod_yes_hard", normal: true);
                        return;
                    case "ANIM_GESTURE_HELLO":
                        PlayerCommands.playAnimation(player, "gestures@m@standing@casual", "gesture_hello", justUpper: true);
                        return;
                    case "ANIM_GESTURE_POINT":
                        PlayerCommands.playAnimation(player, "gestures@m@standing@casual", "gesture_point", justUpper: true);
                        return;
                    case "ANIM_GESTURE_SHRUG_HARD":
                        PlayerCommands.playAnimation(player, "gestures@f@standing@casual", "gesture_shrug_hard", normal: true);
                        return;
                    case "ANIM_GESTURE_DAMN":
                        PlayerCommands.playAnimation(player, "gestures@m@standing@casual", "gesture_damn", justUpper: true);
                        return;
                    case "ANIM_GESTURE_COME_HERE":
                        PlayerCommands.playAnimation(player, "gestures@m@standing@casual", "gesture_come_here_soft", justUpper: true);
                        return;
                    case "ANIM_STOP":
                        PlayerCommands.stopAnimation(player);
                        return;
                    // ==========================
                    // INVENTORY SYSTEM
                    // ==========================
                    case "DROP_ITEM":
                        Items.NewItem(player, arguments[0].ToString(), (Vector3)arguments[1], Convert.ToInt32(arguments[2]));
                        return;
                    case "PICKUP_ITEM":
                        Items.PickupItem(player, (NetHandle)arguments[0]);
                        return;
                    case "GET_ITEMS":
                        Player instance = API.getEntityData(player, "Instance");
                        instance.PlayerInventory.LoadItemsToLocal();
                        return;

                }
            }


            
        }
    }
}
