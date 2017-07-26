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
    public class PointInfo {
        public int BlipColor { get; set; }
        public int BlipType { get; set; }
        public Vector3 Position { get; set; }
        public string Text { get; set; }
        public bool DrawLabel { get; set; }
        public string Id { get; set; }
        public bool InteractionEnabled { get; set; }
        public bool BlipEnabled { get; set; }
        public bool StashType { get; set; }
        public int Dimension { get; set; }

        /// <summary>
        /// Generate an interactive location for a player.
        /// </summary>
        public PointInfo()
        {
            BlipColor = 0;
            BlipType = 1;
            Position = new Vector3();
            Text = "You didn't set this shit up right.";
            DrawLabel = true;
            Id = "No_Label";
            InteractionEnabled = true;
            BlipEnabled = true;
            StashType = false;
            Dimension = 0;
        }
    }

    public static class PointHelper
    {
        public static List<PointInfo> points = new List<PointInfo>();

        /// <summary>
        /// Created a new blip, specify properties after creating the new instance.
        /// </summary>
        /// <returns></returns>
        public static PointInfo addNewPoint()
        {
            PointInfo info = new PointInfo();
            points.Add(info);
            return info;
        }

        /// <summary>
        /// Used to update a PointHelper based on ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        public static void updatePointInfo(string id, string text)
        {
            foreach (PointInfo point in points)
            {
                if (point.Id == id)
                {
                    point.Text = text;
                }
            }
        }

        public static List<PointInfo> Points
        {
            get
            {
                return points;
            }
        }
    }
}
