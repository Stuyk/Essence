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
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace Essence.classes
{
    public class Database : Script
    {
        // Path To Our Database / Connection String
        static string path = "resources/Essence/database/database.db";
        static string connString = string.Format("Data Source={0};Version=3", path);

        // Contains Player Information
        static string playerTable = @"CREATE TABLE IF NOT EXISTS
                            [Players] (
                            [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            [Username] NVARCHAR(2048) NULL,
                            [Password] NVARCHAR(2048) NULL,
                            [Name] NVARCHAR(2048) NULL,
                            [X] FLOAT DEFAULT -1696.866,
                            [Y] FLOAT DEFAULT 142.747,
                            [Z] FLOAT DEFAULT 64.372,
                            [Money] INTEGER DEFAULT 25,
                            [Bank] INTEGER DEFAULT 0,
                            [LoggedIn] BOOL DEFAULT 0,
                            [Health] INTEGER DEFAULT 100,
                            [Armor] INTEGER DEFAULT 0,
                            [Ip] INTEGER,
                            [RegistrationDate],
                            [Faction] INTEGER DEFAULT -1)";

        static string stashTable = @"CREATE TABLE IF NOT EXISTS
                            [Stash] (
                            [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            [Type] INTEGER DEFAULT 0,
                            [Quantity] INTEGER DEFAULT 0,
                            [X] FLOAT DEFAULT 0,
                            [Y] FLOAT DEFAULT 0,
                            [Z] FLOAT DEFAULT 0)";

        static string itemsTable = @"CREATE TABLE IF NOT EXISTS 
                            [Items] (
	                        [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	                        [Owner] INTEGER DEFAULT -1,
	                        [ItemType] INTEGER DEFAULT 101,
                            [Quantity] INTEGER DEFAULT 1,
	                        [Data] TEXT)";

        static string doorsTable = @"CREATE TABLE IF NOT EXISTS
                            [Doors] (
                            [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            [Owner] INTEGER DEFAULT -1,
                            [X] INTEGER DEFAULT 0,
                            [Y] INTEGER DEFAULT 0,
                            [Z] INTEGER DEFAULT 0,
                            [Locked] INTEGER DEFAULT 0,
                            [IPL] NVARCHAR(2048) DEFAULT apa_v_mp_h_01_a)";

        // Contains Vehicle Information - Owner is the owner's Player Table ID.
        static string vehicleTable = @"CREATE TABLE IF NOT EXISTS
                            [Vehicles] (
                            [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            [Owner] INTEGER DEFAULT 0,
                            [Type] NVARCHAR(2048) NULL,
                            [X] FLOAT DEFAULT 0,
                            [Y] FLOAT DEFAULT 0,
                            [Z] FLOAT DEFAULT 0,
                            [RX] FLOAT DEFAULT 0,
                            [RY] FLOAT DEFAULT 0,
                            [RZ] FLOAT DEFAULT 0,
                            [ColorA] INTEGER DEFAULT 131,
                            [ColorB] INTEGER DEFAULT 131,
                            [PrimaryR] INTEGER DEFAULT 0,
                            [PrimaryG] INTEGER DEFAULT 0,
                            [PrimaryB] INTEGER DEFAULT 0,
                            [SecondaryR] INTEGER DEFAULT 0,
                            [SecondaryG] INTEGER DEFAULT 0,
                            [SecondaryB] INTEGER DEFAULT 0,
                            [Spoiler] INTEGER DEFAULT -1,
                            [FrontBumper] INTEGER DEFAULT -1,
                            [RearBumper] INTEGER DEFAULT -1,
                            [SideSkirt] INTEGER DEFAULT -1,
                            [Exhaust] INTEGER DEFAULT -1,
                            [Frame] INTEGER DEFAULT -1,
                            [Grille] INTEGER DEFAULT -1,
                            [Hood] INTEGER DEFAULT -1,
                            [Fender] INTEGER DEFAULT -1,
                            [RightFender] INTEGER DEFAULT -1,
                            [Roof] INTEGER DEFAULT -1,
                            [Engine] INTEGER DEFAULT -1,
                            [Brakes] INTEGER DEFAULT -1,
                            [Transmission] INTEGER DEFAULT -1,
                            [Horns] INTEGER DEFAULT -1,
                            [Suspension] INTEGER DEFAULT -1,
                            [Armor] INTEGER DEFAULT -1,
                            [Turbo] INTEGER DEFAULT -1,
                            [Xenon] INTEGER DEFAULT -1,
                            [FrontWheels] INTEGER DEFAULT -1,
                            [BackWheels] INTEGER DEFAULT -1,
                            [PlateHolders] INTEGER DEFAULT -1,
                            [TrimDesign] INTEGER DEFAULT -1,
                            [Ornaments] INTEGER DEFAULT -1,
                            [DialDesign] INTEGER DEFAULT -1,
                            [SteeringWheel] INTEGER DEFAULT -1,
                            [ShiftLever] INTEGER DEFAULT -1,
                            [Plaques] INTEGER DEFAULT -1,
                            [Hydraulics] INTEGER DEFAULT -1,
                            [Livery] INTEGER DEFAULT -1,
                            [Plate] INTEGER DEFAULT -1,
                            [WindowTint] INTEGER DEFAULT -1
                            )";

        static string clothingTable = @"CREATE TABLE IF NOT EXISTS
                            [Clothing] (
                            [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            [Owner] INTEGER DEFAULT 0,
                            [Mask] INTEGER DEFAULT 0,
                            [MaskVariant] INTEGER DEFAULT 0,
                            [Torso] INTEGER DEFAULT 0,
                            [TorsoVariant] INTEGER DEFAULT 0,
                            [Legs] INTEGER DEFAULT 0,
                            [LegsVariant] INTEGER DEFAULT 0,
                            [Bag] INTEGER DEFAULT 0,
                            [Feet] INTEGER DEFAULT 0,
                            [FeetVariant] INTEGER DEFAULT 0,
                            [Accessories] INTEGER DEFAULT 0,
                            [Undershirt] INTEGER DEFAULT 0,
                            [UndershirtVariant] INTEGER DEFAULT 0,
                            [BodyArmor] INTEGER DEFAULT 0,
                            [BodyArmorVariant] INTEGER DEFAULT 0,
                            [Top] INTEGER DEFAULT 0,
                            [TopVariant] INTEGER DEFAULT 0
                            )";

        static string skinTable = @"CREATE TABLE IF NOT EXISTS
                            [Skin] (
                            [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            [Owner] INTEGER DEFAULT 0,
                            [Father] INTEGER DEFAULT 0,
                            [Mother] INTEGER DEFAULT 0,
                            [FatherSkin] INTEGER DEFAULT 0,
                            [MotherSkin] INTEGER DEFAULT 0,
                            [FaceBlend] NUMERIC DEFAULT 0,
                            [SkinBlend] NUMERIC DEFAULT 0,
                            [Blemishes] INTEGER DEFAULT -1,
                            [Ageing] INTEGER DEFAULT -1,
                            [Complexion] INTEGER DEFAULT -1,
                            [SunDamage] INTEGER DEFAULT -1,
                            [Moles] INTEGER DEFAULT -1,
                            [BodyBlemishes] INTEGER DEFAULT -1,
                            [Hair] INTEGER DEFAULT 0,
                            [HairColor] INTEGER DEFAULT 0,
                            [HairHighLight] INTEGER DEFAULT 0,
                            [FacialHair] INTEGER DEFAULT -1,
                            [FacialHairColor] INTEGER DEFAULT 1,
                            [FacialHairOpacity] FLOAT DEFAULT 1,
                            [Eyebrows] INTEGER DEFAULT 0,
                            [EyebrowsColor] INTEGER DEFAULT 0,
                            [EyebrowsOpacity] FLOAT DEFAULT 1,
                            [Makeup] INTEGER DEFAULT -1,
                            [MakeupColor] INTEGER DEFAULT -1,
                            [MakeupOpacity] FLOAT DEFAULT 1,
                            [Lipstick] INTEGER DEFAULT -1,
                            [LipstickColor] INTEGER DEFAULT 1,
                            [LipstickOpacity] FLOAT DEFAULT 1,
                            [ChestHair] INTEGER DEFAULT -1,
                            [ChestHairColor] INTEGER DEFAULT -1,
                            [ChestHairOpacity] FLOAT DEFAULT 1,
                            [EyeColor] INTEGER DEFAULT 0,
                            [Facepaint] INTEGER DEFAULT -1,
                            [FacepaintOpacity] FLOAT DEFAULT 1,
                            [Face0] NUMERIC DEFAULT 0,
                            [Face1] NUMERIC DEFAULT 0,
                            [Face2] NUMERIC DEFAULT 0,
                            [Face3] NUMERIC DEFAULT 0,
                            [Face4] NUMERIC DEFAULT 0,
                            [Face5] NUMERIC DEFAULT 0,
                            [Face6] NUMERIC DEFAULT 0,
                            [Face7] NUMERIC DEFAULT 0,
                            [Face8] NUMERIC DEFAULT 0,
                            [Face9] NUMERIC DEFAULT 0,
                            [Face10] NUMERIC DEFAULT 0,
                            [Face11] NUMERIC DEFAULT 0,
                            [Face12] NUMERIC DEFAULT 0,
                            [Face13] NUMERIC DEFAULT 0,
                            [Face14] NUMERIC DEFAULT 0,
                            [Face15] NUMERIC DEFAULT 0,
                            [Face16] NUMERIC DEFAULT 0,
                            [Face17] NUMERIC DEFAULT 0,
                            [Face18] NUMERIC DEFAULT 0,
                            [Face19] NUMERIC DEFAULT 0,
                            [Face20] NUMERIC DEFAULT 0,
                            [Model] INTEGER DEFAULT 0
                            )";

        // What happens when we start databasehandler resource.
        public Database()
        {
            API.onResourceStart += API_onResourceStart;
            API.onResourceStop += API_onResourceStop;
        }

        private void API_onResourceStart()
        {
            API.consoleOutput("[SQLITE] Starting...");
            Directory.CreateDirectory("resources/Essence/database");

            bool exists = File.Exists(path);
            if (!exists)
            {
                SQLiteConnection.CreateFile(path);
                API.consoleOutput("[SQLITE] Created Database File.");
            }
            else
            {
                API.consoleOutput("[SQLITE] Found existing database.");
            }

            // Create a connection.
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                // Try to open, and check if we can connect.
                try
                {
                    API.consoleOutput("[SQLITE] Attempting connection...");
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                        API.consoleOutput("[SQLITE] Connected Successfully");
                }
                catch (Exception ex)
                {
                    API.consoleOutput(string.Format("[SQLITE] [ERROR] {0}", ex));
                }
            }

            
            executeQuery(playerTable);
            executeQuery(vehicleTable);
            executeQuery(clothingTable);
            executeQuery(skinTable);
            executeQuery(stashTable);
            executeQuery(itemsTable);
            executeQuery(doorsTable);

            //DataTable table = executeQueryWithResult("SELECT * FROM Players");
        }

        private void API_onResourceStop()
        {
            API.consoleOutput("[SQLITE] Connection closed.");
        }

        // Execute a single query for the database.
        public void executeQuery(string query)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    API.consoleOutput(string.Format("[SQLITE] [ERROR] {0}", ex));
                }
            }
        }

        // Execute a single query for the database, but return a DataTable.
        public DataTable executeQueryWithResult(string query)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    conn.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    DataTable results = new DataTable();
                    results.Load(reader);
                    reader.Close();
                    conn.Close();
                    return results;
                }
                catch (Exception ex)
                {
                    API.consoleOutput(string.Format("[SQLITE] [ERROR] {0}", ex));
                    return null;
                }
            }
        }

        // Execute a single prepared query for the database.
        public void executePreparedQuery(string query, Dictionary<string, string> parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    conn.Open();
                    // Execute a foreach statement. Loops through each entry of the dictionary and add to our command.
                    foreach (KeyValuePair<string, string> entry in parameters)
                    {
                        cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                    }
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    API.consoleOutput(string.Format("[SQLITE] [ERROR] {0}", ex));
                }
            }
        }

        // Execute a single prepared query fro the database and return the unique auto incremented ID
        public int executePreparedQueryReturnLastId(string query, Dictionary<string, string> parameters)
        {
            int primaryKey = -1;

            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand(query + "SELECT SCOPE_IDENTITY()", conn);
                    conn.Open();
                    // Execute a foreach statement. Loops through each entry of the dictionary and add to our command.
                    foreach (KeyValuePair<string, string> entry in parameters)
                    {
                        cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                    }
                    primaryKey = (int)cmd.ExecuteScalar();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    API.consoleOutput(string.Format("[SQLITE] [ERROR] {0}", ex));
                }
            }

            return primaryKey;
        }

        public DataTable executePreparedQueryWithResult(string query, Dictionary<string, string> parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    conn.Open();

                    foreach (KeyValuePair<string, string> entry in parameters)
                    {
                        cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                    }

                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    DataTable results = new DataTable();
                    results.Load(rdr);
                    rdr.Close();
                    return results;
                }
                catch (Exception ex)
                {
                    API.consoleOutput(string.Format("[SQLITE] [ERROR] {0}", ex));
                    return null;
                }
            }
        }

        /* Mr Booleans Magical Query Makers
        // EXAMPLE:
        // int playerID = 0;
        // string before = "UPDATE PlayerClothing SET";
        // string[] varNames = { "Username", "Password" };
        // string after = string.Format("WHERE Id='{0}'", playerID);
        // object[] args = { "Stuyk", "password" };
        // compileQuery(before, after, varNames, args);
        */
        public void compileQuery(string before, string after, string[] vars, object[] data)
        {
            int i = 0;
            string query;
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            query = string.Format("{0}", before);

            foreach (string label in vars)
            {
                if (i == vars.Length - 1)
                {
                    query = string.Format("{0} {1}=@{1}", query, label);
                }
                else
                {
                    query = string.Format("{0} {1}=@{1},", query, label);
                }

                parameters.Add(string.Format("@{0}", label), data[i].ToString());
                ++i;
            }

            //Add anything after the data formatting
            query = string.Format("{0} {1}", query, after);

            //Execute it
            executePreparedQuery(query, parameters);
        }

        /* EXAMPLE:
         * string[] varNamesTwo = { "Email", "Username", "Password", "SocialClub", "IP", "Health", "Armor", "RegisterDate" };
         * string tableName = "Players";
         * string[] dataTwo = { email, username, hash, player.socialClubName, player.address, "100", "0", date.ToString("yyyy-MM-dd HH:mm:ss") };
         * compileInsertQuery(tableName, varNamesTwo, dataTwo);
        */
        public void compileInsertQuery(string tableName, string[] vars, object[] data)
        {
            int i = 0;
            string query;
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            //Add the beginning of our query
            query = string.Format("INSERT INTO {0} (", tableName);

            //format and add our params
            foreach (string label in vars)
            {
                if (i == vars.Length - 1)
                {
                    query = string.Format("{0} {1}) VALUES (", query, label);
                }
                else
                {
                    query = string.Format("{0} {1},", query, label);
                }

                parameters.Add(string.Format("@{0}", label), data[i].ToString());
                ++i;
            }

            i = 0;
            foreach (string label in vars)
            {
                if (i == vars.Length - 1)
                {
                    query = string.Format("{0} @{1})", query, label);
                }
                else
                {
                    query = string.Format("{0} @{1},", query, label);
                }
                ++i;
            }

            executePreparedQuery(query, parameters);
        }

        //Insert Query which returns ID of Auto Incremented ID from DB
        public int compileInsertQueryReturnLastId(string tableName, string[] vars, object[] data)
        {
            int i = 0;
            string query;
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            //Add the beginning of our query
            query = string.Format("INSERT INTO {0} (", tableName);

            //format and add our params
            foreach (string label in vars)
            {
                if (i == vars.Length - 1)
                {
                    query = string.Format("{0} {1}) VALUES (", query, label);
                }
                else
                {
                    query = string.Format("{0} {1},", query, label);
                }

                parameters.Add(string.Format("@{0}", label), data[i].ToString());
                ++i;
            }

            i = 0;
            foreach (string label in vars)
            {
                if (i == vars.Length - 1)
                {
                    query = string.Format("{0} @{1})", query, label);
                }
                else
                {
                    query = string.Format("{0} @{1},", query, label);
                }
                ++i;
            }

            int lastId = executePreparedQueryReturnLastId(query, parameters);
            return lastId;
        }

        // Compile Select Query
        public DataTable compileSelectQuery(string before, string[] vars, object[] data, string after = "")
        {
            int i = 0;
            string query;
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            //Add the beginning of our query
            query = string.Format("{0}", before);

            //format and add our params
            foreach (string label in vars)
            {
                if (i == vars.Length - 1)
                {
                    query = string.Format("{0} {1}=@{1}", query, label);
                }
                else
                {
                    query = string.Format("{0} {1}=@{1} AND", query, label);
                }

                parameters.Add(string.Format("@{0}", label), data[i].ToString());
                ++i;
            }

            //Add anything after the data formatting
            query = string.Format("{0} {1}", query, after);

            //Execute it
            return executePreparedQueryWithResult(query, parameters);
        }

    }
}