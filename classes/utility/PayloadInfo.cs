using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.utility
{
    public class PayloadInfo
    {
        private Dictionary<string, string> parameters;
        private string target;
        private string where;
        private string[] variables;
        private object[] data;
        /** Create a new PayloadInfo to save to our database. */
        public PayloadInfo()
        {
            parameters = new Dictionary<string, string>();
        }
        /** This is our target. IE: 'UPDATE PlayerClothing SET'
         * Remember to use string.Format for this. */
        public string Target
        {
            set
            {
                target = value;
            }
            get
            {
                return target;
            }
        }
        /** This is where we want to update our target. IE: "WHERE Id='1'"
         * Remember to use string.Format for this. */
        public string Where
        {
            set
            {
                where = value;
            }
            get
            {
                return where;
            }
        }
        /** These are the table column names we want to update. Make an array and pass it through here. **/
        public string[] Variables
        {
            set
            {
                variables = value;
            }
            get
            {
                return variables;
            }
        }
        /** This is the data that corresponds with the Variables array. */
        public object[] Data
        {
            set
            {
                data = value;
            }
            get
            {
                return data;
            }
        }
    }
}
