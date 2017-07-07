using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.events
{
    public class EventInfo
    {
        private string eventName;
        private string targetClass;
        private string targetClassFunction;
        /// <summary>
        /// Used to create a new event to fire a method in a different class.
        /// </summary>
        /// <param name="eventName">Local Event Name Used</param>
        /// <param name="targetClass">The class you want to target.</param>
        /// <param name="targetClassFunction">What method / function inside the class.</param>
        public EventInfo(string eventName, string targetClass, string targetClassFunction)
        {
            this.eventName = eventName.ToLower();
            this.targetClass = targetClass;
            this.targetClassFunction = targetClassFunction;
        }

        public void fireEvent(params object[] arguments)
        {
            API.shared.call(targetClass, targetClassFunction, arguments);
        }

        public string EventName
        {
            get
            {
                return eventName;
            }
        }

        public string ClassName
        {
            get
            {
                return targetClass;
            }
        }

        public string ClassFunction
        {
            get
            {
                return targetClassFunction;
            }
        }
    }
}
