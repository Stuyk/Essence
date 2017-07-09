using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.datahandles
{
    class Chat : Script
    {
        //Chat Ranges
        private const float whisperRange = 5f;
        private const float phoneRange = 10f;
        private const float shortRange = 30f;
        private const float longRange = 60f;

        //Chat Colors
        private const string roleplay = "~#B8A5D8~";        //Light Purple
        private const string megaphone = "~#FFE100~";       //Yellow
        private const string faction = "~#42DCF4~";          //Cyan
        private const string radio = "~#8CFF90~";            //Reflective Green

        public Chat()
        {
            API.onChatMessage += API_onChatMessage;
            API.onResourceStart += API_onResourceStart;
        }

        private void API_onResourceStart()
        {
            
        }

        //Standard Chat Message
        private void API_onChatMessage(Client player, string message, CancelEventArgs msgEvent)
        {
            //Cancel Default Chat
            msgEvent.Cancel = true;

            standardChat(player, message);
        }

        //Regular Chat (No command)
        public void standardChat(Client player, string message, bool self = true)
        {
            string msg = player.name + " says: " + message;
            List<Client> nearbyPlayers = API.getPlayersInRadiusOfPlayer(shortRange, player);

            foreach (Client c in nearbyPlayers)
            {
                if (!API.hasEntityData(c, "Instance"))
                {
                    return;
                }

                if(c == player && self == false)
                {
                    return;
                }

                float distanceSenderToReceiver = player.position.DistanceTo(c.position);
                string chatColor = calculateChatColor(distanceSenderToReceiver, shortRange);

                API.sendChatMessageToPlayer(c, chatColor, msg);
            }
        }

        //Local OOC Chat
        [Command("b", GreedyArg = true)]
        public void cmdLocalOocChat(Client player, string message)
        {
            string msg = player.name + ": (( " + message + " ))";
            List<Client> nearbyPlayers = API.getPlayersInRadiusOfPlayer(shortRange, player);

            foreach (Client c in nearbyPlayers)
            {
                if (!API.hasEntityData(c, "Instance"))
                {
                    return;
                }

                float distanceSenderToReceiver = player.position.DistanceTo(c.position);
                string chatColor = calculateChatColor(distanceSenderToReceiver, shortRange);

                API.sendChatMessageToPlayer(c, chatColor, msg);
            }
        }


        //Shout (Double Range)
        [Command("shout", Alias = "s", GreedyArg = true)]
        public void cmdShoutChat(Client player, string message)
        {
            string msg = player.name + " shouts: " + message + "!";
            List<Client> nearbyPlayers = API.getPlayersInRadiusOfPlayer(shortRange, player);

            foreach (Client c in nearbyPlayers)
            {
                if (!API.hasEntityData(c, "Instance"))
                {
                    return;
                }

                float distanceSenderToReceiver = player.position.DistanceTo(c.position);
                string chatColor = calculateChatColor(distanceSenderToReceiver, longRange);

                API.sendChatMessageToPlayer(c, chatColor, msg);
            }
        }

        //Me
        [Command("me", GreedyArg = true)]
        public void cmdMeChat(Client player, string action)
        {
            string msg = "* " + player.name + " " + action;
            List<Client> nearbyPlayers = API.getPlayersInRadiusOfPlayer(shortRange, player);

            foreach (Client c in nearbyPlayers)
            {
                if (!API.hasEntityData(c, "Instance"))
                {
                    return;
                }

                string chatColor = roleplay;

                API.sendChatMessageToPlayer(c, chatColor, msg);
            }
        }

        //Do
        [Command("do", GreedyArg = true)]
        public void cmdDoChat(Client player, string action)
        {
            string msg = "* " + action + " (( " + player.name + " ))";
            List<Client> nearbyPlayers = API.getPlayersInRadiusOfPlayer(shortRange, player);

            foreach (Client c in nearbyPlayers)
            {
                if (!API.hasEntityData(c, "Instance"))
                {
                    return;
                }

                string chatColor = roleplay;

                API.sendChatMessageToPlayer(c, chatColor, msg);
            }
        }

        //Whisper (Whisper Range)
        [Command("whisper", Alias = "w", GreedyArg = true)]
        public void cmdWhisperChat(Client player, string message)
        {
            string msg = player.name + " whispers: " + message + "...";
            List<Client> nearbyPlayers = API.getPlayersInRadiusOfPlayer(whisperRange, player);

            foreach (Client c in nearbyPlayers)
            {
                if (!API.hasEntityData(c, "Instance"))
                {
                    return;
                }

                float distanceSenderToReceiver = player.position.DistanceTo(c.position);
                string chatColor = calculateChatColor(distanceSenderToReceiver, whisperRange);

                API.sendChatMessageToPlayer(c, chatColor, msg);
            }
        }

        //Radio
        [Command("radio", Alias = "r", GreedyArg = true)]
        public void cmdRadioChat(Client player, string message)
        {
            if (!API.hasEntityData(player, "Instance"))
            {
                return;
            }

            Player instance = player.getData("Instance");

            //Find Radio
            if(instance.PlayerInventory.Radio <= 0)
            {
                API.sendChatMessageToPlayer(player, "~r~You do not have a radio.");
                return;
            }

            string msg = "** Radio (" + instance.PlayerInventory.RadioFrequency.ToString() + "kHz) ** " + player.name + ": " + message;
            string chatColor = radio;

            List<Client> allPlayers = API.getAllPlayers();
            foreach(Client c in allPlayers)
            {
                if (!API.hasEntityData(c, "Instance"))
                {
                    return;
                }

                Player i = c.getData("Instance");
                if (i.PlayerInventory.Radio != 0)
                {
                    if (instance.PlayerInventory.RadioFrequency == i.PlayerInventory.RadioFrequency)
                    {
                        API.sendChatMessageToPlayer(c, radio, msg);
                    } 
                }   
            }

            //Send chat to all nearby players like a regular chat
            standardChat(player, message, false);

        }

        //Set Radio Frequency
        [Command("frequency", Alias = "freq")]
        public void cmdRadioFreq(Client player, int frequency)
        {
            if (!API.hasEntityData(player, "Instance"))
            {
                return;
            }

            Player instance = player.getData("Instance");

            //Find Radio
            if (instance.PlayerInventory.Radio <= 0)
            {
                API.sendChatMessageToPlayer(player, "~r~You do not have a radio.");
                return;
            }
            
            //Check length (max 6 digits)
            if(frequency < 1 || frequency > 999999)
            {
                API.sendChatMessageToPlayer(player, "~r~Frequency is out of range.");
                return;
            }

            instance.PlayerInventory.RadioFrequency = frequency;
        }

        //Find Chat Hex Color based on distance between sender and receiver
        private string calculateChatColor(float distance, float chatRange = shortRange)
        {
            int amount = 0;

            switch (chatRange)
            {
                case whisperRange:
                    amount = Convert.ToInt32(Remap(distance, 0f, whisperRange, -100f, 100f));
                    break;
                case shortRange:
                    amount = Convert.ToInt32(Remap(distance, 0f, shortRange, -100f, 100f));
                    break;

                case longRange:
                    amount = Convert.ToInt32(Remap(distance, 0f, longRange, -100f, 100f));
                    break;
            }

            return "~" + mixHex("#444444", "#FFFFFF", amount) + "~";
        }

        //Remap value to different range
        private static float Remap(float value, float from1, float to1, float from2, float to2)
        {
            return from2 + (value - from1) * (to2 - from2) / (to1 - from1);
        }

        //Hex Color Conversion Methods
        private string mixHex(string hexOne, string hexTwo, int amount = 0)
        {
            RGB colorOne = hexToRgb(hexOne);
            RGB colorTwo = hexToRgb(hexTwo);

            RGB mixed = mixRgb(colorOne, colorTwo, amount);

            return rgbToHex(mixed);
        }

        private RGB mixRgb(RGB colorOne, RGB colorTwo, int amount = 0)
        {
            float r1 = (amount + 100f) / 100f;
            float r2 = 2 - r1;

            RGB rgb = new RGB();

            rgb.R = Convert.ToInt32(((colorOne.R * r1) + (colorTwo.R * r2)) / 2);
            rgb.G = Convert.ToInt32(((colorOne.G * r1) + (colorTwo.G * r2)) / 2);
            rgb.B = Convert.ToInt32(((colorOne.B * r1) + (colorTwo.B * r2)) / 2);

            return rgb;
        }

        private RGB hexToRgb(string hexColor)
        {
            RGB rgb = new RGB();

            hexColor = hexColor.TrimStart('#');

            string R = hexColor.Substring(0,2);
            string G = hexColor.Substring(2, 2);
            string B = hexColor.Substring(4, 2);

            rgb.R = Convert.ToInt32(R, 16);
            rgb.G = Convert.ToInt32(G, 16);
            rgb.B = Convert.ToInt32(B, 16);

            return rgb;
        } 

        private string rgbToHex(RGB rgb)
        {
            string hexRed = rgb.R.ToString("X");
            string hexGreen = rgb.G.ToString("X");
            string hexBlue = rgb.B.ToString("X");

            string hexColor = "#" + hexRed + hexGreen + hexBlue;

            return hexColor;
        }

        //RGB Color Data
        struct RGB
        {
            public int R;
            public int G;
            public int B;
        }
    }
}
