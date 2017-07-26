using Discord;
using Discord.Commands;
using Discord.WebSocket;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.discord
{
    public static class DiscordBot
    {
        private static DiscordSocketClient client = new DiscordSocketClient();
        private static CommandService commands = new CommandService();
        private static IServiceProvider services = new ServiceCollection().BuildServiceProvider();

        private static ulong channelToken = 331237092623515660; // In-Game Channel
        private static ulong testerChannelToken = 338812869481332739; // Tester Channel

        public async static void startBot()
        {
            string token = "MzMxMjEzNDA0MjU1NjgyNTYx.DDshcA.OoaxhwGTm4y8ANGuvUoQj8X1tXQ";

            try
            {
                await client.LoginAsync(TokenType.Bot, token);
                await client.StartAsync();
                client.MessageReceived += Client_MessageReceived;
            }
            catch
            {
                Console.WriteLine("Could not connect properly.");
                return;
            }
            await Task.Delay(3000);
        }

        private async static Task Client_MessageReceived(SocketMessage message)
        {
            if (message.Content.Contains("!ping"))
            {
                await message.Channel.SendMessageAsync("Pong");
            }

            if (message.Content[0] != '#')
            {
                return;
            }

            if (message.Content.Contains("#fetchdata"))
            {
                string[] user = message.Content.Split(null);
                List<Client> players = API.shared.getAllPlayers();
                Client target = null;
                foreach (Client player in players)
                {
                    if (player.name.ToLower() == user[1].ToLower())
                    {

                        target = player;
                        break;
                    }
                }

                if (target != null)
                {
                    string[] data = API.shared.getAllEntityData(target.handle);
                    foreach (string dat in data)
                    {
                        sendMessageToServer(string.Format("[{0} - EntityData] {1}", user[1], dat));
                    }
                    string[] entityData = API.shared.getAllEntitySyncedData(target.handle);
                    foreach (string dat in entityData)
                    {
                        sendMessageToServer(string.Format("[{0} - EntitySyncedData] {1}", user[1], dat));
                    }
                } else
                {
                    await message.Channel.SendMessageAsync("Failed to find user: " + user[1]);
                }
            }
        }

        public static void sendMessageToServer(string message)
        {
            SocketTextChannel channel = client.GetChannel(channelToken) as SocketTextChannel;
            channel.SendMessageAsync(message);
        }

        public static void sendMessageToTesters(string message)
        {
            SocketTextChannel channel = client.GetChannel(testerChannelToken) as SocketTextChannel;
            channel.SendMessageAsync(message);
        }
    }
}
