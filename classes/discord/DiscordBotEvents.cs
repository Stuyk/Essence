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

namespace Essence.classes.discord
{
    public class DiscordBotEvents : Script
    {
        public DiscordBotEvents()
        {
            API.onPlayerConnected += API_onPlayerConnected;
            API.onPlayerDisconnected += API_onPlayerDisconnected;
            API.onEntityDataChange += API_onEntityDataChange;
            API.onPlayerDeath += API_onPlayerDeath;
            API.onResourceStart += API_onResourceStart;
            API.onResourceStop += API_onResourceStop;
        }

        private void API_onResourceStart()
        {
            DiscordBot.startBot();
            API.delay(2000, true, () =>
            {
                DiscordBot.sendMessageToServer(string.Format("Started the server at: {0}", DateTime.Now));
                DiscordBot.sendMessageToServer(string.Format("Current Address: {0}", new ImpatientWebClient().DownloadString("http://icanhazip.com")));
            });
        }

        private void API_onResourceStop()
        {
            DiscordBot.sendMessageToServer(string.Format("Stopped the server at: {0}", DateTime.Now));
        }

        private void API_onPlayerDeath(Client player, NetHandle entityKiller, int weapon)
        {
            if (API.getEntityType(entityKiller) == EntityType.Player)
            {
                Client killer = API.getPlayerFromHandle(entityKiller);
                DiscordBot.sendMessageToServer(string.Format("{0} was killed by {1}", player.name, killer.name));
                return;
            }

            if (API.getEntityType(entityKiller) == EntityType.Vehicle)
            {
                DiscordBot.sendMessageToServer(string.Format("{0} was killed by a vehicle.", player.name));
                return;
            }

            DiscordBot.sendMessageToServer(string.Format("{0} was killed by unnatural causes.", player.name));
        }

        private void API_onPlayerDisconnected(Client player, string reason)
        {
            DiscordBot.sendMessageToServer(string.Format("{0} disconnected.", player.name));
        }

        private void API_onPlayerConnected(Client player)
        {
            DiscordBot.sendMessageToServer(string.Format("{0} connected.", player.name));
        }

        private void API_onEntityDataChange(NetHandle entity, string key, object oldValue)
        {
            if (API.getEntityType(entity) != EntityType.Player)
            {
                return;
            }

            Client player = API.getPlayerFromHandle(entity);

            switch (key)
            {
                case "Instance":
                    DiscordBot.sendMessageToServer(string.Format("{0} logged in.", player.name));
                    return;
                case "Mission":
                    DiscordBot.sendMessageToServer(string.Format("{0} joined a mission.", player.name));
                    return;
            }
        }
    }
}
