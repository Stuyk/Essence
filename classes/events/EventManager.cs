using Essence.classes.doors;
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

namespace Essence.classes.events
{
    public static class EventManager
    {
        public static List<EventInfo> events = new List<EventInfo>();

        public static void SetupEvents()
        {
            // Login
            events.Add(new EventInfo("clientLogin", "Login", "cmdLogin"));
            events.Add(new EventInfo("clientRegister", "Register", "cmdRegister"));
            // Missions / Objectives / Minigames
            events.Add(new EventInfo("checkObjective", "MissionHelper", "CheckObjective"));
            events.Add(new EventInfo("Check_Lockpick_Score", "MinigameHelper", "CheckLockPickScore"));
            // Vehicles
            events.Add(new EventInfo("Vehicle_Engine", "VehicleCommands", "ToggleEngine"));
            events.Add(new EventInfo("Vehicle_Trunk", "VehicleCommands", "ToggleTrunk"));
            events.Add(new EventInfo("Vehicle_Hood", "VehicleCommands", "ToggleHood"));
            events.Add(new EventInfo("Vehicle_Door", "VehicleCommands", "ToggleDoor"));
            events.Add(new EventInfo("Vehicle_Windows_Up", "VehicleCommands", "WindowState"));
            events.Add(new EventInfo("Vehicle_Windows_Down", "VehicleCommands", "WindowState"));
            events.Add(new EventInfo("SHOP_ATM", "Atm", "loadATM"));
            // Shops
            events.Add(new EventInfo("SHOP_BARBER", "Barber", "startBarberShop"));
            events.Add(new EventInfo("SHOP_MASK", "Mask", "startMaskShop"));
            events.Add(new EventInfo("SHOP_TATTOO", "Tattoo", "startTattooShop"));
            //shop Buying
            events.Add(new EventInfo("BUY_MASK", "Mask", "PurchaseMask"));
            events.Add(new EventInfo("BARBER_BUY", "Barber", "PurchaseBarber"));
            // Jobs
            events.Add(new EventInfo("JOB_LONG_RANGE_TRUCKING", "LongRangeTrucking", "startLongRangeTruckingJob"));
            events.Add(new EventInfo("JOB_SHORT_RANGE_TRUCKING", "ShortRangeTrucking", "startShortRangeTruckingJob"));
            events.Add(new EventInfo("JOB_CHOP_SHOP", "ChopShop", "startChopShopJob"));
            // Inventory
            events.Add(new EventInfo("DROP_ITEM", "ItemCalls", "DropItem"));
            events.Add(new EventInfo("PICKUP_ITEM", "ItemCalls", "PickupItem"));
            events.Add(new EventInfo("GET_ITEMS", "ItemCalls", "GetItems"));
            events.Add(new EventInfo("USE_ITEM", "ItemCalls", "UseItem"));

            // Doors
            events.Add(new EventInfo("Exit_Dynamic_Door", "DoorCalls", "ExitInterior"));
            Console.WriteLine("Events setup with {0} events.", events.Count);
        }

        public static EventInfo GetEvent(string eventName)
        {
            for (int i = 0; i < events.Count; i++)
            {
                if (events[i].EventName != eventName)
                    continue;
                return events[i];
            }
            return null;
        }
    }
}
