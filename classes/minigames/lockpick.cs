using Essence.classes.missions;
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
        private List<Client> players;
        private MinigameInfo minigameInfo;
        private DateTime timeSinceLastCheck;
        private ObjectiveInfo objInfo;

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
            players = new List<Client>();
        }

        public ObjectiveInfo ObjectiveInformation
        {
            set
            {
                objInfo = value;
            }
        }

        public MinigameInfo GameInfo
        {
            get
            {
                return minigameInfo;
            }
        }

        public void addPlayer(Client player)
        {
            if (players.Contains(player))
            {
                return;
            }

            players.Add(player);
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

        public void checkScore(Client client, int playerInput)
        {
            if (DateTime.Now < timeSinceLastCheck)
            {
                return;
            }

            int leadroom = 2;

            if (client.ping > 100 && client.ping < 150)
            {
                leadroom = 3;
            }


            if (client.ping >= 150 && client.ping <= 200)
            {
                leadroom = 4;
            }

            if (client.ping >= 201)
            {
                leadroom = 5;
            }
            

            timeSinceLastCheck = DateTime.Now.AddSeconds(1);

            if (outside - leadroom < playerInput && outside + leadroom > playerInput)
            {
                minigameInfo.Score += 5;
                foreach (Client player in players)
                {
                    API.shared.playSoundFrontEnd(player, "CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET");
                    API.shared.setEntitySyncedData(player, "Lockpick_Score", minigameInfo.Score);
                }
            }

            foreach (Client player in players)
            {
                API.shared.setEntitySyncedData(player, "Lockpick_Score", minigameInfo.Score);

                if (minigameInfo.Score >= 100)
                {
                    timer.Stop();
                    if (player.hasData("Mission"))
                    {
                        Mission mission = player.getData("Mission");
                        mission.verifyObjective(player);
                    }
                }
            }
        }

        private void lockPickAdjuster(object sender, ElapsedEventArgs e)
        {
            if (minigameInfo.Score >= 100)
            {
                timer.Stop();
                objInfo.Status = true;
                foreach (Client player in players)
                {
                    if (player.hasData("Mission"))
                    {
                        Mission mission = player.getData("Mission");
                        mission.verifyObjective(player);
                    }
                }


                return;
            }

            changeDirection();
            adjustOutside();
        }

        private void changeDirection()
        {
            if (DateTime.Now > nextDirectionChange)
            {
                nextDirectionChange = DateTime.Now.AddSeconds(1);

                int random = new Random().Next(0, 2);

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
            foreach (Client player in players)
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
