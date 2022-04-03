using System;
using System.Data.SQLite;

namespace HabitLogger
{ 
    class Program
    {
        public static void Main(String[] args)
        {
            SQLHandler sqlHandler = new SQLHandler();

            Console.WriteLine("Hello, and Welcome to the Habit Logger!");
            Console.WriteLine("How many Activities did you engage in today?");

            sqlHandler.printAll();

            int activities = intInput();

            for (int i = 0; i < activities; i++)
            {
                String activity = "";
                int numHours = 0;

                Console.WriteLine("Activity " + i + ":");
                activity = Console.ReadLine();
                Console.WriteLine("Number of Hours Performed:");
                numHours = intInput();

                sqlHandler.insert(activity, numHours);
            }
        }


        public static int intInput()
        {
            String input = "";
            int number = 0;

            while (input.Equals(""))
            {
                input = Console.ReadLine();
                try
                {
                    int.TryParse(input, out number);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Need an integer!");
                    input = "";
                }
            }
            return number;

        }

        
    }
}

