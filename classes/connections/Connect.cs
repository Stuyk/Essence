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

namespace Essence.classes
{
    class Connect : Script
    {
        public Connect()
        {
            API.onPlayerConnected += API_onPlayerConnected;
            API.onPlayerFinishedDownload += API_onPlayerFinishedDownload;
        }

        private void API_onPlayerFinishedDownload(Client player)
        {
            API.triggerClientEvent(player, "ShowLogin");
        }

        private void API_onPlayerConnected(Client player)
        {
            API.setEntityDimension(player, new Random().Next(90000, 99999));
            API.freezePlayer(player, true);
        }
    }
}
