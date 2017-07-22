﻿using GTANetworkServer;
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
            // Nothing
        }

        [Command("forceRegistration")]
        public void cmdForceReg(Client player, string user, string pass)
        {
            cmdRegister(player, user, pass);
        }

        public void cmdRegister(Client player, params object[] arguments)
        {
            if (arguments.Length <= 0)
            {
                API.triggerClientEvent(player, "FailRegistration");
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
                API.triggerClientEvent(player, "FailRegistration");
                return;
            }

            if (password.Length <= 0)
            {
                API.triggerClientEvent(player, "FailRegistration");
                return;
            }

            var hash = BCr.BCrypt.HashPassword(password, BCr.BCrypt.GenerateSalt(12));

            string[] varNamesZero = { "Username" };
            string beforeZero = "SELECT Username FROM Players WHERE";
            object[] dataZero = { username };
            DataTable result = db.compileSelectQuery(beforeZero, varNamesZero, dataZero);

            if (result.Rows.Count >= 1)
            {
                API.triggerClientEvent(player, "FailRegistration");
                return;
            }

            DateTime date = DateTime.Now;

            // Setup registration.
            string[] varNamesTwo = { "Username", "Password", "IP", "Health", "Armor", "RegistrationDate" };
            string tableName = "Players";
            string[] dataTwo = { username, hash, player.address, "100", "0", date.ToString("yyyy-MM-dd HH:mm:ss") };
            db.compileInsertQuery(tableName, varNamesTwo, dataTwo);

            // Get the ID that belongs to the player. (THIS IS NOT SECURE, COULD BE WRONG IF TWO PEOPLE REGISTER AT THE SAME TIME)
            result = db.executeQueryWithResult("SELECT ID FROM Players ORDER BY ID DESC LIMIT 1");
            string playerID = Convert.ToString(result.Rows[0]["ID"]);

            // Setup clothing table for new player.
            setupTableForPlayer(playerID, "Clothing");

            // Setup skin table for new player.
            setupTableForPlayer(playerID, "Skin");

            // Setup inventory table for new player.
            setupTableForPlayer(playerID, "Inventory");
            API.triggerClientEvent(player, "FinishRegistration");
        }

        private void setupTableForPlayer(string id, string tableName)
        {
            string[] varNames = { "Owner" };
            string[] data = { id };
            db.compileInsertQuery(tableName, varNames, data);
        }

    }
}
