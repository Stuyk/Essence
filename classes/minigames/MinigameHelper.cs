using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.minigames
{
    public static class MinigameHelper
    {
        public static void CheckLockPickScore(Client player, params object[] arguments)
        {
            if (!player.hasData("Minigame"))
            {
                return;
            }
            int playerInput = Convert.ToInt32(arguments[0]); 
            Lockpick minigame = player.getData("Minigame");
            minigame.checkScore(player, playerInput);
        }
    }
}
