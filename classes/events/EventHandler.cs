using Essence.classes.commands;
using Essence.classes.inventory;
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

namespace Essence.classes.events
{
    public class EventHandler : Script
    {
        public EventHandler()
        {
            API.onClientEventTrigger += API_onClientEventTrigger;
            API.onResourceStart += API_onResourceStart;
        }

        private void API_onResourceStart()
        {
            EventManager.SetupEvents();
        }

        private void API_onClientEventTrigger(Client player, string eventName, params object[] arguments)
        {
            API.shared.consoleOutput($"Attempting Event: {eventName}");
            EventInfo eventInfo = EventManager.GetEvent(eventName.ToLower());
            if (eventInfo != null)
            {
                API.call(eventInfo.ClassName, eventInfo.ClassFunction, player, arguments);
                API.shared.consoleOutput($"Fired Event: {eventInfo.ClassName} - {eventInfo.ClassFunction} - {arguments}");
            } else {
                API.shared.consoleOutput($"Event Did Not Fire: {eventName}");
            }

            /*
                switch (eventName)
                {
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

                }
            */
        }
    }
}
