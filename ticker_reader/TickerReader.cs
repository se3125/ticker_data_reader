using System;
using Newtonsoft.Json;
using System.Data.SQLite;

namespace ticker
{
    public class TickerReader 
    {
        private static int DEFAULT_RECORDS_TO_READ = 10;
        private static readonly JsonSerializerSettings JSON_SETTINGS = new JsonSerializerSettings() 
        {
            MissingMemberHandling = MissingMemberHandling.Error
        };

    private static readonly string DB_STRING = @"Data Source=C:\Users\saebert\Documents\db\tickerdata.db";

    static void Main(string[] args)
        {
            int myRecordsToRead = DEFAULT_RECORDS_TO_READ;

            try {
                if (args.Length == 2) {
                    myRecordsToRead = Int32.Parse(args[1]);
                } else {
                    Console.WriteLine("No value for number of records provided; using the default: " + DEFAULT_RECORDS_TO_READ);
                }
            } catch (FormatException e) {
                Console.WriteLine("Cannot parse number of records; using the default: " + DEFAULT_RECORDS_TO_READ);
            }

            using var dbConnection = new SQLiteConnection(DB_STRING);

            dbConnection.Open();

            string queryString = String.Format("SELECT * from ticker LIMIT {0};", myRecordsToRead);

            var command = new SQLiteCommand(queryString, dbConnection);

            using SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                string json = (string)reader["data"];

                try {
                    // As in TickerPersister, this deserizaliation isn't strictly necessary since the current form of the 
                    // program simply outputs the json - this is to ensure that the records are valid 
                    // (which should already be the case with the validation in the persister), and to support further
                    // extensions that might use this TickerData in ways other than simply logging it
                    TickerData tickerData = JsonConvert.DeserializeObject<TickerData>(json, JSON_SETTINGS);
                    Console.WriteLine(json);
                } catch (JsonException e) {
                    Console.WriteLine("Failed to process record: " + json + " Exception: " + e.Message);
                }
            }
        }
    }
}    