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
        private int id;
        private string name;
        private WeaponModule weaponModule;

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

        /// <summary>
        /// The weapon module assigned to this faction.
        /// </summary>
        public WeaponModule ArmsModule
        {
            get
            {
                return weaponModule;
            }
            set
            {
                weaponModule = value;
            }
        }

        /// <summary>
        /// The ID that this faction pulls from.
        /// </summary>
        public int ID
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

        /// <summary>
        /// The name of this faction.
        /// </summary>
        public string Name
        {
            set
            {
                name = value;
            }
            get
            {
                return name;
            }
        }


    }
}
