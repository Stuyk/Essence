using Discord;
using Discord.WebSocket;
using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.discord
{
    public static class DiscordBot
    {
        public static DiscordSocketClient client = new DiscordSocketClient();

        private static ulong token = 331237092623515660;

        public async static void startBot()
        {
            try
            {
                await client.LoginAsync(TokenType.Bot, "MzMxMjEzNDA0MjU1NjgyNTYx.DDshcA.OoaxhwGTm4y8ANGuvUoQj8X1tXQ");
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
            SocketTextChannel channel = client.GetChannel(token) as SocketTextChannel;
            channel.SendMessageAsync(message);
        }
    }
}
