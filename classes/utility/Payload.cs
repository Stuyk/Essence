using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Essence.classes.utility
{
    public static class Payload
    {
        // Path To Our Database / Connection String
        private static string path = "resources/Essence/database/database.db";
        private static string connString = string.Format("Data Source={0};Version=3", path);
        private static List<PayloadInfo> payloads = new List<PayloadInfo>();
        private static List<PayloadInfo> queuedPayloads = new List<PayloadInfo>();
        private static bool runningExecution = false;

        // Used to save data.
        public static void executeQueries()
        {
            int executionCount = 0;
            // No reason to execute queries if the length is zero.
            if (payloads.Count <= 0)
                return;
            // Set our execution boolean to true to ensure no payloads make their way in.
            runningExecution = true;
            // Let's run a foreach Statement to our database.
            foreach (PayloadInfo payload in payloads)
            {
                compileQuery(payload.Target, payload.Where, payload.Variables, payload.Data);
                executionCount++;
            }
            // Let's clear our list for the next time.
            payloads = new List<PayloadInfo>();
            // After the foreach Statement is complete, let's check our count and log it.
            Console.WriteLine(string.Format("[Payload] Executed Saves, Saved: {0} Queries", executionCount));
            // Stop our execution boolean.
            runningExecution = false;
        }
        // Add a new payload to our list.
        public static void addNewPayload(string target, string where, string[] variables, object[] data)
        {
            PayloadInfo info = new PayloadInfo();
            info.Target = target;
            info.Where = where;
            info.Variables = variables;
            info.Data = data;
            // If executeQueries is not running, add it and add our queue in.
            if (!runningExecution)
            {
                payloads.Add(info);
                addFromQueue();
                Console.WriteLine("[Payload] Added new payload.");
                Console.WriteLine(string.Format("[Payload] Current Count: {0}", payloads.Count));
            } else {
                queuedPayloads.Add(info);
                Console.WriteLine("[Payload] Queued new payload.");
            }
        }
        // Check out queue, add it to our main List.
        private static void addFromQueue()
        {
            if (queuedPayloads.Count <= 0)
                return;

            int count = 0;

            foreach (PayloadInfo payload in queuedPayloads)
            {
                if (!payloads.Contains(payload))
                {
                    payloads.Add(payload);
                    count++;
                    continue;
                }
            }

            queuedPayloads = new List<PayloadInfo>();

            Console.WriteLine(string.Format("[Payload] Moved Queued Payloads: {0} Queries", count));
        }

        private static void compileQuery(string before, string after, string[] vars, object[] data)
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

        private static void executePreparedQuery(string query, Dictionary<string, string> parameters)
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
                    Console.Write(string.Format("[SQLITE] [ERROR] {0}", ex));
                }
            }
        }
    }
}
