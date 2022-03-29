using System;
using System.Data.SQLite;

namespace HabitLogger
{ 
    class Program
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Hello, World!");
            SQLInitializer();
        }

        public static void SQLInitializer()
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=hello.db");
            conn.Open();
            Console.WriteLine("Conn opened");


            var command = conn.CreateCommand();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Users([Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [Username] NVARCHAR(64) NOT NULL, [Email] NVARCHAR(128) NOT NULL, [Password] NVARCHAR(128) NOT NULL  )";
            command.ExecuteNonQuery();
            Console.WriteLine("Table created");

            /*
            command.CommandText = "INSERT INTO Users(Username, Email, Password) VALUES('admin', 'testing@gmail.com', 'test') ";
            command.ExecuteNonQuery();
            Console.WriteLine("Inserted?");
            */

            command.CommandText = "SELECT Username, Email, Password FROM users";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var name = reader.GetString(0);
                    var name2 = reader.GetString(1);
                    Console.WriteLine($"Hello, {name}, {name2}!");
                }
            }
        }
    }
}

