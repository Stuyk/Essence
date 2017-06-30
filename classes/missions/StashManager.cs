using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.missions
{
    public static class StashManager
    {
        public static List<StashInfo> stashes = new List<StashInfo>();

        public static StashInfo getStashInfoByID(int id)
        {
            foreach(StashInfo stash in stashes)
            {
                if (stash.ID == id)
                {
                    return stash;
                }
            }
            Console.WriteLine("[STASH] !!! COULDN'T FIND PROPER STASH FOR ID:" + id.ToString());
            return null;
        }
    }
}
