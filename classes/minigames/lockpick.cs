using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Essence.classes.minigames
{
    public class Lockpick
    {
        private Timer timer;
        private int outside;
        private bool running;
        private Direction direction;
        private DateTime nextDirectionChange;
        private Client player;
        private MinigameInfo minigameInfo;
        private DateTime timeSinceLastCheck;

        public enum Direction
        {
            Left,
            Right
        }

        public Lockpick()
        {
            running = false;
            outside = 0;
            direction = Direction.Left;
            nextDirectionChange = DateTime.Now.AddSeconds(2);
            minigameInfo = new MinigameInfo();
            minigameInfo.Type = MinigameInfo.MinigameType.Lockpick;
            timeSinceLastCheck = DateTime.Now.AddSeconds(1);
        }

        public Client AssignedPlayer
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

        public bool isRunning
        {
            set
            {
                running = value;
                checkTimer();
            }
            get
            {
                return running;
            }
        }

        public void checkTimer()
        {
            if (!running)
            {
                if (timer != null)
                {
                    timer.Stop();
                }
            }
        }

        public void startLockpick()
        {
            timer = new Timer();
            timer.Interval = 50;
            timer.Enabled = true;
            timer.Elapsed += lockPickAdjuster;
        }

        public void checkScore(int playerInput)
        {
            if (DateTime.Now < timeSinceLastCheck)
            {
                return;
            }
            timeSinceLastCheck = DateTime.Now.AddSeconds(1);

            if (outside - 2 < playerInput && outside + 2 > playerInput)
            {
                minigameInfo.Score += 5;
                API.shared.playSoundFrontEnd(player, "Hack_Success", "DLC_HEIST_BIOLAB_PREP_HACKING_SOUNDS");
            }

            API.shared.setEntitySyncedData(player, "Lockpick_Score", minigameInfo.Score);

            if (minigameInfo.Score >= 100)
            {
                timer.Stop();
                isRunning = false;
            }
        }

        private void lockPickAdjuster(object sender, ElapsedEventArgs e)
        {
            changeDirection();
            adjustOutside();
        }

        private void changeDirection()
        {
            if (DateTime.Now > nextDirectionChange)
            {
                nextDirectionChange = DateTime.Now.AddSeconds(1);

                int random = new Random().Next(0, 2);

                API.shared.consoleOutput(random.ToString());

                switch (random)
                {
                    case 0:
                        direction = Direction.Left;
                        break;
                    case 1:
                        direction = Direction.Right;
                        break;
                }
            }
        }

        private void adjustOutside()
        {
            if (player != null)
            {
                switch (direction)
                {
                    case Direction.Left:
                        outside -= 1;
                        if (outside < 0)
                        {
                            outside = 359;
                        }
                        API.shared.setEntitySyncedData(player, "Lockpick_Value", outside);
                        break;
                    case Direction.Right:
                        outside += 1;
                        if (outside > 359)
                        {
                            outside = 0;
                        }
                        API.shared.setEntitySyncedData(player, "Lockpick_Value", outside);
                        break;
                }
            }
        }
    }
}
