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
        private bool drawLabel;
        private string id;
        private bool interactionEnabled;

        /// <summary>
        /// Generate an interactive location for a player.
        /// </summary>
        public PointInfo()
        {
            color = 0;
            blipType = 1;
            position = new Vector3();
            text = "You didn't set this shit up right.";
            drawLabel = true;
            id = "No_Label";
            interactionEnabled = true;
        }

        /// <summary>
        /// The color our blip is going to be.
        /// </summary>
        public int BlipColor
        {
            set
            {
                color = value;
            }
            get
            {
                return color;
            }
        }

        /// <summary>
        /// The style / icon our blip is going to be.
        /// </summary>
        public int BlipType
        {
            set
            {
                blipType = value;
            }
            get
            {
                return blipType;
            }
        }
        
        /// <summary>
        /// The position our blip should be located at.
        /// </summary>
        public Vector3 Position
        {
            set
            {
                position = value;
            }
            get
            {
                return position;
            }
        }

        /// <summary>
        /// The text that will show up if DrawLabel is true.
        /// </summary>
        public string Text
        {
            set
            {
                text = value;
            }
            get
            {
                return text;
            }
        }

        /// <summary>
        /// Should we draw text near this position for the player to see?
        /// </summary>
        public bool DrawLabel
        {
            set
            {
                drawLabel = value;
            }
            get
            {
                return drawLabel;
            }
        }

        /// <summary>
        /// A special ID assigned to this label, so if a player pressed E a event will trigger in EventManager if hooked up. See EventManager.cs for more on this.
        /// </summary>
        public string ID
        {
            set
            {
                id = value;
            }
            get
            {
                return id;
            }
        }

        public bool InteractionEnabled
        {
            set
            {
                interactionEnabled = value;
            }
            get
            {
                return interactionEnabled;
            }
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

        public static List<PointInfo> Points
        {
            get
            {
                return points;
            }
        }
    }
}
