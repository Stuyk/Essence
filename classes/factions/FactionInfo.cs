using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.factions
{
    public class FactionInfo
    {
        private int id;
        private string name;
        
        public FactionInfo()
        {

        }

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
