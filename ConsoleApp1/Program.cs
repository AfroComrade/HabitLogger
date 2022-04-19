using System;
using System.Data.SQLite;

namespace HabitLogger
{
    class Program
    {
        public static SQLHandler sqlHandler;

        public static void Main(String[] args)
        {
            sqlHandler = new SQLHandler();

            Console.WriteLine("Hello, and Welcome to the Habit Logger!");
            Console.WriteLine("I hope you've been productive today!");
            Console.WriteLine("Type 0 at any time to quit");

            inputPerform(mainMenu());

            Console.WriteLine("Goodbye!");
        }

        public static int mainMenu()
        {
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Add Activity"); // Insert
            Console.WriteLine("2. Delete a past activity"); // Delete
            Console.WriteLine("3. Update a past activity"); // Update
            Console.WriteLine("4. View all activities"); // View

            return intMenuOption();
        }


        public static int intMenuOption()
        {
            String input = "";
            int number = 0;

            while (input.Equals(""))
            {
                input = Console.ReadLine();

                bool parsed = int.TryParse(input, out number);

                if (!parsed)
                {
                    Console.WriteLine("Need an integer!");
                    input = "";
                }
                else if (number < 0 || number > 4)
                {
                    Console.WriteLine("Need a number between 1 & 4!");
                    input = "";
                }

            }
            return number;
        }

        public static void inputPerform(int i)
        {
            int input = 0;
            switch (i)
            {
                case 0:
                    return;
                case 1:
                    input = addActivity();
                    break;
                case 2:
                    input = deleteActivity();
                    break;
                case 3:
                    input = updateActivity();
                    break;
                case 4:
                    sqlHandler.printAll();
                    input = 1;
                    break;
            }

            if (input == 0)
                return;
            else
                inputPerform(mainMenu());
        }


        public static int updateActivity()
        {
            Console.WriteLine("Which Log do you wish to delete?");
            Console.WriteLine("(You can find your log ID by viewing all logs)");

            String input = "";
            int id = 0;

            while (input.Equals(""))
            {
                input = Console.ReadLine();

                bool parsed2 = int.TryParse(input, out id);
                if (!parsed2 || id < 0)
                {
                    Console.WriteLine("Please enter a number below 0!");
                    input = "";
                }
            }

            if (id == 0) // Always quit if the user enters 0 in the prompt
                return 0;

            // Logic for handling if the id exists
            Boolean found = false;
            for (int i = 0; i < sqlHandler.numElements; i++) 
            {
                if (id == sqlHandler.ids[i])
                    found = true;
            }
            if (!found)
            {
                Console.WriteLine("ID not found. Please view all logs and try again.");
                return id;
            }


            String newActivity = "";

            int check = -1;
            Console.WriteLine("New activity?");
            newActivity = Console.ReadLine();
            bool parsed = int.TryParse(newActivity, out check);
            
            if (parsed && check == 0)
                    return 0;

            int newTimesPerformed = 0;
            input = "";
            Console.WriteLine("Times performed?");
            while (input.Equals(""))
            {
                input = Console.ReadLine();
                parsed = int.TryParse(input, out newTimesPerformed);

                if (parsed && newTimesPerformed == 0)
                    return newTimesPerformed;
                else if (!parsed)
                {
                    Console.WriteLine("Please enter a number!");
                }
            }

            sqlHandler.update(id, newActivity, newTimesPerformed);

            return 1;
        }

        public static int addActivity()
        {
            int timesPerformed = -1;
            Console.WriteLine("Activity :");
            String activity = Console.ReadLine();

            bool parsed = int.TryParse(activity, out timesPerformed);

            if (parsed && timesPerformed == 0)
                return 0;


            Console.WriteLine("Number of times Performed:");
            String input = "";
            while (input.Equals(""))
            {
                input = Console.ReadLine();
                parsed = int.TryParse(input, out timesPerformed);

                if (!parsed || timesPerformed < 0)
                {
                    Console.WriteLine("Need an integer above 0!");
                    input = "";
                }
            }
                
            if (timesPerformed != 0)
                sqlHandler.insert(activity, timesPerformed);
            
            return timesPerformed;
        }

        public static int deleteActivity()
        {
            Console.WriteLine("Which Log do you wish to delete?");
            Console.WriteLine("(You can find your log ID by viewing all logs)");

            String input = "";
            int id = 0;
            while (input.Equals(""))
            {
                input = Console.ReadLine();
                
                bool parsed = int.TryParse(input, out id);

                if (!parsed || id < 0)
                {
                    Console.WriteLine("Need a number above 0!");
                    input = "";
                }
            }

            Boolean deleted = false;
            if (id != 0)
                deleted = sqlHandler.deleteID(id);

            if (deleted)
                Console.WriteLine(id + " successfully deleted;");
            else
                Console.WriteLine(id + " not found. Please view all and try again, or report deletion fail to admin");

            return id;
        }
    }
}

