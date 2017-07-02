using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.commands
{
    public static class PlayerCommands
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

        public static void playAnimation(Client player, string dictionary, string name, bool playerControl = false, bool loop = false, bool upperLoop = false, bool justUpper = false, bool lastFrame = false, bool normal = false)
        {
            if (loop)
            {
                player.playAnimation(dictionary, name, (int)(AnimationFlags.Loop));
            }

            if (upperLoop)
            {
                player.playAnimation(dictionary, name, (int)(AnimationFlags.Loop | AnimationFlags.OnlyAnimateUpperBody));
            }

            if (justUpper)
            {
                player.playAnimation(dictionary, name, (int)(AnimationFlags.OnlyAnimateUpperBody));
            }

            if (playerControl)
            {
                player.playAnimation(dictionary, name, (int)(AnimationFlags.AllowPlayerControl));
            }       
            
            if (lastFrame)
            {
                player.playAnimation(dictionary, name, (int)(AnimationFlags.StopOnLastFrame));
            }

            if (normal)
            {
                player.playAnimation(dictionary, name, 0);
                API.shared.delay(3000, true, () =>
                {
                    player.stopAnimation();
                });
            }
        }

        public static void stopAnimation(Client player)
        {
            player.stopAnimation();
        }
    }
}
