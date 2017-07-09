using Essence.classes.discord;
using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Essence.classes.anticheat
{
    public class AnticheatInfo
    {
        private Client client;
        private int model;
        private Vector3 lastPosition;
        private bool diedRecently;
        private int currentStrikes;
        private bool leftCarRecently;
        
        public AnticheatInfo(Client client)
        {
            this.client = client;
            this.model = client.model;
            this.lastPosition = client.position;
            this.diedRecently = false;
            this.currentStrikes = 0;
            client.setData("Anticheat", this);
        }

        public int CurrentStrikes
        {
            get
            {
                return currentStrikes;
            }
            set
            {
                currentStrikes = value;
            }
        }

        public Client GetClient
        {
            get
            {
                return client;
            }
        }

        public int Model
        {
            set
            {
                model = value;
            }
            get
            {
                return model;
            }
        }

        public Vector3 LastPosition
        {
            set
            {
                lastPosition = value;
            }
            get
            {
                return lastPosition;
            }
        }

        public bool DiedRecently
        {
            get
            {
                return diedRecently;
            }
            set
            {
                diedRecently = value;
            }
        }

        public bool LeftCarRecently
        {
            get
            {
                return leftCarRecently;
            }
            set
            {
                leftCarRecently = value;
            }
        }
    }

    public static class Anticheat
    {
        private static List<AnticheatInfo> AntiCheatPlayers = new List<AnticheatInfo>();
        private static Timer timer = new Timer(3000);

        public static void addPlayer(Client player)
        {
            foreach (AnticheatInfo info in AntiCheatPlayers)
            {
                if (info.GetClient == player)
                {
                    info.CurrentStrikes = 0;
                    return;
                }
            }

            AnticheatInfo antiInfo = new AnticheatInfo(player);
            AntiCheatPlayers.Add(antiInfo);
        }

        public static void startAnticheat()
        {
            timer.Start();
            timer.Elapsed += Timer_Elapsed;
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < AntiCheatPlayers.Count; i++)
            {
                isPlayerTeleportHacking(AntiCheatPlayers[i]);
                isModelHacking(AntiCheatPlayers[i]);
                strikeCheck(AntiCheatPlayers[i]);
            }
        }

        private static void strikeCheck(AnticheatInfo player)
        {
            if (player.CurrentStrikes >= 5)
            {
                player.CurrentStrikes = 0;
                player.GetClient.kick("Anticheat");
            }
        }
        
        private static void isModelHacking(AnticheatInfo player)
        {
            if (player.GetClient.model != player.Model)
            {
                player.CurrentStrikes += 5;
                DiscordBot.sendMessageToServer(string.Format("[Anticheat] {0}, 5 strikes added for model change.", player.GetClient.name));
            }
        }

        private static void isPlayerTeleportHacking(AnticheatInfo player)
        {
            if (player.LeftCarRecently)
            {
                player.LeftCarRecently = false;
                player.LastPosition = player.GetClient.position;
                return;
            }
            // Player on foot can cover about 21 feet per 3 seconds.
            if (!player.GetClient.isInVehicle)
            {
                if (player.GetClient.position.DistanceTo2D(player.LastPosition) > 28)
                {
                    player.CurrentStrikes += 1;
                    DiscordBot.sendMessageToServer(string.Format("[Anticheat] {0}, strike added for Teleporting.", player.GetClient.name));
                }
            } else {
                if (player.GetClient.position.DistanceTo2D(player.LastPosition) > 180)
                {
                    player.CurrentStrikes += 1;
                    DiscordBot.sendMessageToServer(string.Format("[Anticheat] {0}, strike added for Teleporting.", player.GetClient.name));
                }
            }
            player.LastPosition = player.GetClient.position;
        }

        public static void playerLeftVehicle(Client player)
        {
            foreach (AnticheatInfo info in AntiCheatPlayers)
            {
                if (info.GetClient == player)
                {
                    info.LeftCarRecently = true;
                }
            }
        }
    }
}
