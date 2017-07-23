using Essence.classes;
using Essence.classes.anticheat;
using Essence.classes.discord;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
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
            API.setGamemodeName("~g~By ~b~Stuyk");
            API.setServerName("~g~Essence ~b~Pure ~b~Roleplay");
            API.onResourceStart += API_onResourceStart;
        }

        private void API_onResourceStart()
        {
            Anticheat.startAnticheat();
        }

        [Command("tryAnimation")]
        public void tryAnim(Client player, string dic, string target)
        {
            API.playPlayerAnimation(player, 0, dic, target);
            API.delay(5000, true, () =>
            {
                player.stopAnimation();
            });
        }

        
    }
}
