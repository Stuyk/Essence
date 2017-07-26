using Essence.classes.doors;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.commands
{
    public class Commands : Script
    {
        public Commands()
        {
            // Nothing
        }

        [Command("forcelogin")]
        public void cmdForceLogin(Client player, string username, string password)
        {
            Login.cmdLogin(player, username, password);
        }

        [Command("forceregistration")]
        public void cmdForceRegistration(Client player, string username, string password, string playername)
        {
            Register.cmdRegister(player, username, password, playername);
        }

        [Command("doorlock")]
        public void cmdDoorLock(Client player)
        {
            DoorManager.ToggleDoor(player);
        }
    }
}
