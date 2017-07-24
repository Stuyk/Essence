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

namespace Essence.classes.utility
{
    public class PointHelperManager : Script
    {
        public PointHelperManager()
        {
            API.onPlayerFinishedDownload += API_onPlayerFinishedDownload;
        }

        private void API_onPlayerFinishedDownload(Client player)
        {
            List<PointInfo> points = PointHelper.Points;
            foreach (PointInfo point in points)
            {
                API.triggerClientEvent(player, "Add_New_Point", point.Position, point.BlipType, point.BlipColor, point.Text, point.DrawLabel, point.ID, point.InteractionEnabled, point.BlipEnabled, point.Dimension);
                // Position, Type, Color, Text, Draw, ID, Interactable, show the blip?, Blip Dimension
            }
        }
    }
}
