using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.factions
{
    public class FactionInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public WeaponModule ArmsModule { get; set; }

        public enum WeaponModule
        {
            LowGradeArms,
            MediumGradeArms,
            HighGradeArms,
            ExplosiveArms,
            Melee
        }
        
        public FactionInfo() { }
        public FactionInfo(DataRow db)
        {

        }
    }
}
