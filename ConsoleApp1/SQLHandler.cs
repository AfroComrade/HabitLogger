using System;
using System.Data.SQLite;

namespace HabitLogger
{
    internal class SQLHandler
    {
        private SQLiteCommand command;

        public SQLHandler()
        {
            String source = "HabitLogger.db";

            SQLiteConnection conn = new SQLiteConnection("Data Source=" + source);
            conn.Open();

            command = conn.CreateCommand();
            command.CommandText =
                "CREATE TABLE IF NOT EXISTS Logs([Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [Activity] NVARCHAR(64) NOT NULL, [Duration] INTEGER NOT NULL)";
            command.ExecuteNonQuery();
        }

        public void insert(String activity, int Duration)
        {
            command.CommandText = "INSERT INTO Logs(Activity, Duration) VALUES('" + activity + "', '" + Duration + "') ";
            command.ExecuteNonQuery();
        }

        public void printAll()
        {
            command.CommandText = "SELECT Activity, Duration FROM Logs";
            using (var reader = command.ExecuteReader())
            {
                int logNum = 0;
                while (reader.Read())
                {
                    logNum++;
                    var activity = reader.GetString(0);
                    var duration = reader.GetInt32(1);
                    Console.WriteLine("Log " + logNum + $": {activity}, {duration} hours");
                }
            }
        }
    }
}
