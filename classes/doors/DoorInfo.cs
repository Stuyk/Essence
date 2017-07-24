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
        public int CoreId { get; set; }
        public string Id { get; set; }
        public bool Locked { get; set; }
        public string Owner { get; set; }
        public string IPL { get; set; }
        public int Price { get; set; }
        public bool isForSale { get; set; }
        public Vector3 DoorLocation { get; set; }
        public Vector3 InteriorLocation { get; set; }

        public DoorInfo(DataRow db)
        {
            DoorLocation = new Vector3(Convert.ToSingle(db["X"]), Convert.ToSingle(db["Y"]), Convert.ToSingle(db["Z"]));
            IPL = Convert.ToString(db["IPL"]);
            CoreId = Convert.ToInt32(db["Id"]);
            Id = $"Door-{db["Id"].ToString()}";
            Owner = $"{db["Owner"]}";
            InteriorLocation = Interiors.getInteriorByType(IPL);
            Locked = Convert.ToBoolean(db["Locked"]);
            setupPointInfo();
        }

        private void setupPointInfo()
        {
            PointInfo info = PointHelper.addNewPoint();
            info.Position = DoorLocation;
            info.BlipEnabled = false;
            info.BlipColor = 1;
            info.BlipType = 1;
            info.Text = $"House - {Id} - {Owner}";
            info.ID = Id;
            info.InteractionEnabled = true;
            info.DrawLabel = true;
            setupExitPointInfo();
        }

        private void setupExitPointInfo()
        {
            PointInfo info = PointHelper.addNewPoint();
            info.Position = InteriorLocation;
            info.BlipEnabled = false;
            info.BlipColor = 1;
            info.BlipType = 1;
            info.Text = $"~r~Exit";
            info.ID = "Exit_Dynamic_Door";
            info.InteractionEnabled = true;
            info.DrawLabel = true;
            info.Dimension = CoreId;
        }
    }
}
