using GTANetworkServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCr = BCrypt.Net;

namespace Essence.classes
{
    class Register : Script
    {
        Database db = new Database();

        public Register()
        {
            API.onPlayerConnected += API_onPlayerConnected;
        }

        private void API_onPlayerConnected(Client player)
        {
            API.sendChatMessageToPlayer(player, "~b~Essence: ~w~If you're a new user. Register with: /register [username] [password] [password]");
        }

        [Command("register", Description = "/register [username] [password] [password2]")]
        public void cmdRegister(Client player, string username, string password, string password2)
        {
            if (password != password2)
            {
                API.sendChatMessageToPlayer(player, "~r~Passwords did not match.");
                return;
            }

            var hash = BCr.BCrypt.HashPassword(password, BCr.BCrypt.GenerateSalt(12));

            string[] varNamesZero = { "Username" };
            string beforeZero = "SELECT Username FROM Players WHERE";
            object[] dataZero = { username };
            DataTable result = db.compileSelectQuery(beforeZero, varNamesZero, dataZero);

            if (result.Rows.Count >= 1)
            {
                API.sendChatMessageToPlayer(player, "That username already exists.");
                return;
            }

            DateTime date = DateTime.Now;

            string[] varNamesTwo = { "Username", "Password", "IP", "Health", "Armor", "RegistrationDate" };
            string tableName = "Players";
            string[] dataTwo = { username, hash, player.address, "100", "0", date.ToString("yyyy-MM-dd HH:mm:ss") };
            db.compileInsertQuery(tableName, varNamesTwo, dataTwo);

            API.sendChatMessageToPlayer(player, "Succesfully registered, you may now login.");
        }
    }
}
