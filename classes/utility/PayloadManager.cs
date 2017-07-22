using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Essence.classes.utility
{
    // This is the manager for payloads. Just runs on a timer and executes queries every 30 seconds.
    public class PayloadManager : Script
    {
        public PayloadManager()
        {
            API.onResourceStart += API_onResourceStart;
        }

        private void API_onResourceStart()
        {
            Timer timer = new Timer();
            timer.Interval = 30000;
            timer.Elapsed += Time_Elapsed;
            timer.Enabled = true;
        }

        private void Time_Elapsed(object sender, ElapsedEventArgs e)
        {
            Payload.executeQueries();
        }
    }
}
