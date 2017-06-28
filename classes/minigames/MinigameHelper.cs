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
        public static void setupLockpick(Client player)
        {
            Lockpick minigame = new Lockpick();
            API.shared.setEntityData(player, "Minigame", minigame);
            minigame.AssignedPlayer = player;
            minigame.isRunning = true;
            minigame.startLockpick();
        }
    }
}
