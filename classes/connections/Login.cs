using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using BCr = BCrypt.Net;
using Essence;
using Essence.classes.discord;

namespace Essence.classes
{
    public static class Login
    {
        private static Database db = new Database();

        private static void SetAllPlayerLoginsToZero()
        {
            /*
            string[] varNames = { "LoggedIn" };
            string before = "UPDATE Players SET";
            object[] data = { "0" };
            string after = string.Format("");
            db.compileQuery(before, after, varNames, data);
            */
        }

        public static void cmdLogin(Client player, params object[] arguments)
        {
            if (arguments.Length <= 0)
            {
                API.shared.triggerClientEvent(player, "FailLogin");
                return;
            }

            string username = arguments[0].ToString();
            string password = arguments[1].ToString();

            if (player.hasSyncedData("ESS_LoggedIn"))
                return;

            if (username.Length <= 0)
            {
                API.shared.triggerClientEvent(player, "FailLogin");
                return;
            }

            if (password.Length <= 0)
            {
                API.shared.triggerClientEvent(player, "FailLogin");
                return;
            }

            string[] varNames = { "Username" };
            string before = "SELECT ID, Password, X, Y, Z, Money, Bank, Name, LoggedIn, Health, IsAdmin, Armor FROM Players WHERE";
            object[] data = { username };
            DataTable result = db.compileSelectQuery(before, varNames, data);

            if (result.Rows.Count <= 0)
            {
                API.shared.triggerClientEvent(player, "FailLogin");
                return;
            }

            bool verify = BCr.BCrypt.Verify(password, Convert.ToString(result.Rows[0]["Password"]));
            if (!verify)
            {
                API.shared.triggerClientEvent(player, "FailLogin");
                return;
            }

            new Player(player, result.Rows[0]);
        }

    }
}
