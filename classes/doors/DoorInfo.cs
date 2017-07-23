using Essence.classes.utility;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.doors
{
    public class DoorInfo
    {
        private int coreID;
        private string id;
        private Vector3 doorLocation;
        public bool Locked { get; set; }
        private string owner;
        public string IPL { get; set; }
        public int Price { get; set; }
        public bool isForSale { get; set; }
        private Vector3 interiorLocation;

        public DoorInfo(DataRow db)
        {
            doorLocation = new Vector3(Convert.ToSingle(db["X"]), Convert.ToSingle(db["Y"]), Convert.ToSingle(db["Z"]));
            IPL = Convert.ToString(db["IPL"]);
            coreID = Convert.ToInt32(db["ID"]);
            id = "Door-" + Convert.ToString(db["ID"]);
            owner = "Some Dude";

            interiorLocation = Interiors.getInteriorByType(IPL);

            Locked = Convert.ToBoolean(db["Locked"]);

            PointInfo info = PointHelper.addNewPoint();
            info.Position = doorLocation;
            info.BlipEnabled = false;
            info.BlipColor = 1;
            info.BlipType = 1;
            info.Text = string.Format("House [{0}]", owner);
            info.ID = id;
            info.InteractionEnabled = true;
            info.DrawLabel = true;
        }

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

        public Vector3 DoorLocation
        {
            set
            {
                doorLocation = value;
            }
            get
            {
                return doorLocation;
            }
        }

        public Vector3 InteriorLocation
        {
            get
            {
                return interiorLocation;
            }
        }

        public bool isLocked
        {
            get
            {
                return Locked;
            }
        }
    }
}
