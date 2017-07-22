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

namespace Essence.classes.missions
{
    public class Stash : Script
    {
        Database db = new Database();

        public Stash()
        {
            API.onResourceStart += LoadStashes;
        }

        private void LoadStashes()
        {
            DataTable table = db.executeQueryWithResult("SELECT * FROM Stash");

            if (table.Rows.Count <= 0)
            {
                return;
            }

            int count = 0;

            foreach (DataRow row in table.Rows)
            {
                StashInfo stashInfo = new StashInfo();
                stashInfo.ID = Convert.ToInt32(row["ID"]);
                stashInfo.Quantity = Convert.ToInt32(row["Quantity"]);
                stashInfo.Location = new Vector3(Convert.ToSingle(row["X"]), Convert.ToSingle(row["Y"]), Convert.ToSingle(row["Z"]));
                switch (Convert.ToInt32(row["Type"]))
                {
                    case 0:
                        stashInfo.Type = StashInfo.StashType.None;
                        Console.WriteLine("[STASH] !!! FOUND A 'NONE' STASH. FIX IN SQLite MANAGER");
                        break;
                    case 1:
                        stashInfo.Type = StashInfo.StashType.CarParts;
                        break;
                }
                setupPoint(stashInfo);
                StashManager.stashes.Add(stashInfo);
                count++;
            }

            Console.WriteLine("[STASH] TOTAL COUNT: " + count.ToString());
        }

        private void setupPoint(StashInfo stashinfo)
        {
            PointInfo point = PointHelper.addNewPoint();
            point.BlipColor = 1;
            point.BlipType = 1;
            point.Text = string.Format("Contains - {0} ~n~ Quantity: {1}", stashinfo.Type.ToString(), stashinfo.Quantity.ToString());
            point.DrawLabel = true;
            point.ID = stashinfo.ID.ToString();
            point.InteractionEnabled = false;
            point.Position = stashinfo.Location;
            point.BlipEnabled = false;
            point.StashType = true;
        }
    }
}
