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
            }
            catch
            {
                Console.WriteLine("Could not connect properly.");
                return;
            }
            await Task.Delay(3000);
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
