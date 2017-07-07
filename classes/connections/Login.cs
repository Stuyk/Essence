using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using BCr = BCrypt.Net;
using Essence;
using GTANetworkShared;
using Essence.classes.discord;

namespace Essence.classes
{
    public class Login : Script
    {
        Database db = new Database();

        public Login()
        {
           
        }


        private void SetAllPlayerLoginsToZero()
        {
            /*
            string[] varNames = { "LoggedIn" };
            string before = "UPDATE Players SET";
            object[] data = { "0" };
            string after = string.Format("");
            db.compileQuery(before, after, varNames, data);
            */
        }

        [Command("forceLogin")]
        public void cmdForceLogin(Client player, string user, string pass)
        {
            cmdLogin(player, user, pass);
        }

        public void cmdLogin(Client player, params object[] arguments)
        {
            if (arguments.Length <= 0)
            {
                API.triggerClientEvent(player, "FailLogin");
                return;
            }

            string username = arguments[0].ToString();
            string password = arguments[1].ToString();

            if (API.hasEntitySyncedData(player, "ESS_LoggedIn"))
            {
                return;
            }

            if (username.Length <= 0)
            {
                API.triggerClientEvent(player, "FailLogin");
                return;
            }

            if (password.Length <= 0)
            {
                API.triggerClientEvent(player, "FailLogin");
                return;
            }

            string[] varNames = { "Username" };
            string before = "SELECT ID, Password, X, Y, Z, Money, Bank, LoggedIn, Health, Armor FROM Players WHERE";
            object[] data = { username };
            DataTable result = db.compileSelectQuery(before, varNames, data);

            if (result.Rows.Count < 1)
            {
                API.triggerClientEvent(player, "FailLogin");
                return;
            }

            bool verify = BCr.BCrypt.Verify(password, Convert.ToString(result.Rows[0]["Password"]));
            if (!verify)
            {
                API.triggerClientEvent(player, "FailLogin");
                return;
            }

            new Player(player, result.Rows[0]);
            //API.sendNativeToPlayer(player, (ulong)Hash.DO_SCREEN_FADE_OUT, 2000);
            API.delay(3000, true, () =>
            {
                API.triggerClientEvent(player, "FinishLogin");
                //API.sendNativeToPlayer(player, (ulong)Hash.DO_SCREEN_FADE_IN, 5000);
            });
        }

    }
}
