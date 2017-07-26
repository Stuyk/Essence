using Essence.classes.utility;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCr = BCrypt.Net;

namespace Essence.classes
{
    public static class Register
    {
        public static Database db = new Database();

        public static void cmdRegister(Client player, params object[] arguments)
        {
            if (arguments.Length <= 0)
            {
                API.shared.triggerClientEvent(player, "FailRegistration");
                return;
            }

            string username = arguments[0].ToString();
            string password = arguments[1].ToString();
            string playername = arguments[2].ToString();

            if (API.shared.hasEntitySyncedData(player, "ESS_LoggedIn"))
                return;

            if (username.Length <= 0)
            {
                API.shared.triggerClientEvent(player, "FailRegistration");
                return;
            }

            if (password.Length <= 0)
            {
                API.shared.triggerClientEvent(player, "FailRegistration");
                return;
            }

            if (username.Length <= 0)
            {
                API.shared.triggerClientEvent(player, "FailRegistration");
                return;
            }

            string[] varNamesOne = { "Name" };
            string beforeOne = "SELECT Name FROM Players WHERE";
            object[] dataOne = { playername };
            DataTable result = db.compileSelectQuery(beforeOne, varNamesOne, dataOne);

            if (result.Rows.Count >= 1)
            {
                API.shared.triggerClientEvent(player, "FailRegistration");
                return;
            }

            string[] varNamesZero = { "Username" };
            string beforeZero = "SELECT Username FROM Players WHERE";
            object[] dataZero = { username };
            result = db.compileSelectQuery(beforeZero, varNamesZero, dataZero);

            if (result.Rows.Count >= 1)
            {
                API.shared.triggerClientEvent(player, "FailRegistration");
                return;
            }

            var hash = BCr.BCrypt.HashPassword(password, BCr.BCrypt.GenerateSalt(12));
            DateTime date = DateTime.Now;

            // Setup registration.
            string[] varNamesTwo = { "Username", "Password", "IP", "Health", "Armor", "RegistrationDate", "Name" };
            string tableName = "Players";
            string[] dataTwo = { username, hash, player.address, "100", "0", date.ToString("yyyy-MM-dd HH:mm:ss"), playername };
            db.compileInsertQuery(tableName, varNamesTwo, dataTwo);

            // Get the ID that belongs to the player. (THIS IS NOT SECURE, COULD BE WRONG IF TWO PEOPLE REGISTER AT THE SAME TIME)
            result = db.executeQueryWithResult("SELECT ID FROM Players ORDER BY ID DESC LIMIT 1");
            string playerID = Convert.ToString(result.Rows[0]["ID"]);

            // Setup clothing table for new player.
            Utility.setupTableForPlayer(playerID, "Clothing");

            // Setup skin table for new player.
            Utility.setupTableForPlayer(playerID, "Skin");

            // Setup inventory table for new player.
            API.shared.triggerClientEvent(player, "FinishRegistration");
        }
    }
}
