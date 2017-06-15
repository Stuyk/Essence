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

namespace Essence.classes
{
    public class Login : Script
    {
        Database db = new Database();

        public Login()
        {
            API.onResourceStart += API_onResourceStart;
            API.onPlayerConnected += API_onPlayerConnected;
        }

        private void API_onPlayerConnected(Client player)
        {
            API.sendChatMessageToPlayer(player, "~b~Essence: ~w~If you're an existing user. Login with: /login [username] [password]");
        }

        private void API_onResourceStart()
        {
            API.consoleOutput("Started: LoginHandler");
            SetAllPlayerLoginsToZero();
           
            
        }

        private void SetAllPlayerLoginsToZero()
        {
            string[] varNames = { "LoggedIn" };
            string before = "UPDATE Players SET";
            object[] data = { "0" };
            string after = string.Format("");
            db.compileQuery(before, after, varNames, data);
        }

        [Command("login", Description = "/login [username] [password]")]
        public void cmdLogin(Client player, string username, string password)
        {
            string[] varNames = { "Username" };
            string before = "SELECT ID, Password, X, Y, Z, Money, Bank, LoggedIn, Health, Armor FROM Players WHERE";
            object[] data = { username };
            DataTable result = db.compileSelectQuery(before, varNames, data);

            if (result.Rows.Count < 1)
            {
                API.sendChatMessageToPlayer(player, "~b~Essence: ~r~User does not exist.");
                return;
            }

            bool verify = BCr.BCrypt.Verify(password, Convert.ToString(result.Rows[0]["Password"]));
            if (!verify)
            {
                API.sendChatMessageToPlayer(player, "~b~Essence: ~r~Username or password does not match.");
                return;
            }

            new Player(player, result.Rows[0]);
        }

    }
}
