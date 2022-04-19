using System;
using System.Data.SQLite;

namespace HabitLogger
{
    internal class SQLHandler
    {
        private SQLiteCommand command;
        public int[] ids = new int[99];
        public int numElements = 0;

        public SQLHandler()
        {
            String source = "HabitLogger.db";

            SQLiteConnection conn = new SQLiteConnection("Data Source=" + source);
            conn.Open();

            command = conn.CreateCommand();
            command.CommandText =
                "CREATE TABLE IF NOT EXISTS Logs([Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [Activity] NVARCHAR(64) NOT NULL, [TimesPerformed] INTEGER NOT NULL)";
            command.ExecuteNonQuery();

            initializeIDArray();
        }

        public void initializeIDArray()
        {
            ids = new int[20];

            command.CommandText = "SELECT ID FROM Logs";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ids[numElements] = reader.GetInt32(0);
                    numElements++;
                }
            }
        }

        public void insert(String activity, int timesPerformed)
        {
            command.CommandText = "INSERT INTO Logs(Activity, TimesPerformed) VALUES('" + activity + "', '" + timesPerformed + "') ";
            command.ExecuteNonQuery();
            initializeIDArray();
        }

        public Boolean update(int id, String activity, int timesPerformed)
        {
            command.CommandText = $"UPDATE Logs SET Activity = '{activity}', TimesPerformed = '{timesPerformed}' WHERE id = {id}";
            command.ExecuteNonQuery();
            return true;
        }

        public Boolean deleteID(int id)
        {
            Boolean found = false;
            for (int i = 0; i < numElements; i++)
            {
                if (ids[i] == id)
                    found = true;
            }

            if (found)
            {
                command.CommandText = "DELETE FROM Logs WHERE Id = " + id;
                command.ExecuteNonQuery();
                initializeIDArray();
            }
            return found;
        }

        public void printOne(int id)
        {
            command.CommandText = "SELECT ID, Activity, TimesPerformed FROM Logs WHERE ID = " + id;
            using (var reader = command.ExecuteReader())
            {
                var activity = reader.GetString(1);
                var timesPerformed = reader.GetInt32(2);
                Console.WriteLine($": ID: {id}, {activity}, {timesPerformed} times performed"); ;
            }
            
        }

        public void printAll()
        {
            command.CommandText = "SELECT ID, Activity, TimesPerformed FROM Logs";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var activity = reader.GetString(1);
                    var timesPerformed = reader.GetInt32(2);
                    Console.WriteLine($"ID: {id}, {activity}, {timesPerformed} times performed");
                }
            }
        }
    }
}
