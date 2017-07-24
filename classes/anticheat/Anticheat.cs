using Essence.classes.discord;
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

namespace Essence.classes.anticheat
{
    public class AnticheatInfo
    {
        public Client PlayerClient { get; set; }
        public int Model { get; set; }
        public Vector3 LastPosition { get; set; }
        public bool DiedRecently { get; set; }
        public int CurrentStrikes { get; set; }
        public bool LeftCarRecently { get; set; }
        public bool HealthChangedRecently { get; set; }
        public bool ArmorChangedRecently { get; set; }

        public AnticheatInfo(Client client)
        {
            this.PlayerClient = client;
            this.Model = client.model;
            this.LastPosition = client.position;
            this.DiedRecently = false;
            this.CurrentStrikes = 0;
            this.HealthChangedRecently = false;
            this.ArmorChangedRecently = false;
            client.setData("Anticheat", this);
            API.shared.consoleOutput($"Anticheat Model: {this.Model}");
        }
    }

    public static class Anticheat
    {
        private static List<string> peopleNames = new List<string>();

        private static List<AnticheatInfo> AntiCheatPlayers = new List<AnticheatInfo>();
        private static Timer timer = new Timer(3000);

        public static AnticheatInfo addPlayer(Client player)
        {
            foreach (AnticheatInfo info in AntiCheatPlayers)
            {
                if (info.PlayerClient == player)
                {
                    info.CurrentStrikes = 0;
                    return info;
                }
            }

            AnticheatInfo antiInfo = new AnticheatInfo(player);
            AntiCheatPlayers.Add(antiInfo);
            return antiInfo;
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
                player.PlayerClient.kick("Anticheat");
            }
        }

        public static void checkHealth(Client player, int oldValue)
        {
            if (!player.hasData("Anticheat"))
            {
                return;
            }

            AnticheatInfo info = player.getData("Anticheat");

            if (player.health > oldValue)
            {
                if (info.HealthChangedRecently)
                {
                    info.HealthChangedRecently = false;
                    return;
                } else {
                    info.CurrentStrikes += 1;
                    DiscordBot.sendMessageToServer(string.Format("[Anticheat] {0}, possible health hacking.", player.name));
                }
            }
        }

        private static void setHealth(Client player, int value)
        {
            if (!player.hasData("Anticheat"))
            {
                return;
            }

            AnticheatInfo info = player.getData("Anticheat");
            info.HealthChangedRecently = true;
            player.health += value;
        }

        public static void checkArmor(Client player, int oldValue)
        {
            if (!player.hasData("Anticheat"))
            {
                return;
            }

            AnticheatInfo info = player.getData("Anticheat");

            if (player.armor > oldValue)
            {
                if (info.HealthChangedRecently)
                {
                    info.ArmorChangedRecently = false;
                    return;
                }
                else
                {
                    info.CurrentStrikes += 1;
                    DiscordBot.sendMessageToServer(string.Format("[Anticheat] {0}, possible armor hacking.", player.name));
                }
            }
        }

        private static void setArmor(Client player, int value)
        {
            if (!player.hasData("Anticheat"))
            {
                return;
            }

            AnticheatInfo info = player.getData("Anticheat");
            info.ArmorChangedRecently = true;
            player.armor += value;
        }

        private static void isModelHacking(AnticheatInfo player)
        {
            if (player.PlayerClient.model != player.Model)
            {
                player.CurrentStrikes += 5;
                DiscordBot.sendMessageToServer(string.Format("[Anticheat] {0}, 5 strikes added for model change.", player.PlayerClient.name));
            }
        }

        private static void isPlayerTeleportHacking(AnticheatInfo player)
        {
            if (player.LeftCarRecently)
            {
                player.LeftCarRecently = false;
                player.LastPosition = player.PlayerClient.position;
                return;
            }
            // Player on foot can cover about 21 feet per 3 seconds.
            if (!player.PlayerClient.isInVehicle)
            {
                if (player.PlayerClient.position.DistanceTo2D(player.LastPosition) > 28)
                {
                    player.CurrentStrikes += 1;
                    DiscordBot.sendMessageToServer(string.Format("[Anticheat] {0}, strike added for Teleporting.", player.PlayerClient.name));
                }
            } else {
                if (player.PlayerClient.position.DistanceTo2D(player.LastPosition) > 180)
                {
                    player.CurrentStrikes += 1;
                    DiscordBot.sendMessageToServer(string.Format("[Anticheat] {0}, strike added for Teleporting.", player.PlayerClient.name));
                }
            }
            player.LastPosition = player.PlayerClient.position;
        }

        public static void playerLeftVehicle(Client player)
        {
            foreach (AnticheatInfo info in AntiCheatPlayers)
            {
                if (info.PlayerClient == player)
                {
                    info.LeftCarRecently = true;
                }
            }
        }
    }
}
