using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.utility
{
    public class PointInfo {
        private int color;
        private int blipType;
        private Vector3 position;
        private string text;
        private bool draw;
        private string id;

        public PointInfo() { }
        public PointInfo(int blipColor, int blipStyle, Vector3 blipPosition, string blipText, bool drawText, string newID)
        {
            color = blipColor;
            blipType = blipStyle;
            position = blipPosition;
            text = blipText;
            draw = drawText;
            id = newID;
        }

        public int Color
        {
            get
            {
                return color;
            }
        }

        public int Type
        {
            get
            {
                return blipType;
            }
        }
        
        public Vector3 Position
        {
            get
            {
                return position;
            }
        }

        public string Text
        {
            get
            {
                return text;
            }
        }

        public bool Draw
        {
            get
            {
                return draw;
            }
        }

        public string ID
        {
            get
            {
                return id;
            }
        }
    }

    public static class PointHelper
    {
        public static List<PointInfo> points = new List<PointInfo>();

        public static void addNewPoint(int blipColor, int blipStyle, Vector3 blipLocation, string blipText, bool drawText, string id)
        {
            PointInfo info = new PointInfo(blipColor, blipStyle, blipLocation, blipText, drawText, id);
            points.Add(info);
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
