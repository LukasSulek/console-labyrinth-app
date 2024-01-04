using System;
using System.Linq;

namespace LabyrinthApplication
{
    internal class InputHandler
    {
        public InputHandler()
        {

        }

        public int GetLabyrinthParameter(int minValue, string message)
        {
            Console.Write(message);
            int parameter = 0;
            do
            {
                try
                {
                    parameter = Convert.ToInt32(Console.ReadLine());
                    if (parameter < minValue)
                    {
                        Console.Write($"Incorrect value. Please enter a value bigger than 1: ");
                    }
                }
                catch (FormatException)
                {
                    Console.Write($"Incorrect input format. Please enter an integer value: ");
                }
                catch (OverflowException)
                {
                    Console.Write($"Value was too long. Please enter shorter value:");
                }
            }
            while (parameter < minValue || parameter.GetType() != typeof(int));
            return parameter;
        }

        public int[] GetValues(int size)
        {
            Console.WriteLine("Enter a sequence of values 0, 1 or 2 - any other value and start position[0,0] will be always 0:");

            int[] values = new int[size];
            bool isCorrect = false;

            do
            {
                values[0] = 0;
                Console.Write($"{values[0]},");

                for (int i = 1; i < values.Length; i++)
                {
                    int value = GetValueInput();
                    if (i < (values.Length - 1)) Console.Write(",");
                    values[i] = value;
                }

                if (1 == values.Count(value => value == 2)) isCorrect = true;
                else
                {
                    Utils.AddEmptyLines(2);
                    Console.WriteLine("Sequence must contain exactly one instance of number 2. Please try again: ");
                }
            }
            while (!isCorrect);

            return values;
        }

        public int GetValueInput()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.NumPad0:
                    return 0;
                case ConsoleKey.NumPad1:
                    return 1;
                case ConsoleKey.NumPad2:
                    return 2;
                default:
                    return 0;
            }
        }

        public static void ResetOrCloseConsole(LabyrinthApp app)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.Escape:
                    app.Run();
                    break;
                case ConsoleKey.Enter:
                    Environment.Exit(0);
                    break;
                default:
                    Utils.AddEmptyLines(1);
                    Console.WriteLine("Unrecognized key. Please press Enter or Escape.");
                    ResetOrCloseConsole(app);
                    break;
            }
        }

        public static void NewOrExistingInput()
        {
            string command = Console.ReadLine();
            switch (command)
            {
                case "SELECT":
                    LabyrinthApp.SelectExistingLabyrinth();
                    break;
                case "NEW":
                    LabyrinthApp.CreateNewLabyrinth();
                    break;
                default:
                    Utils.AddEmptyLines(1);
                    Console.WriteLine("Unrecognized command.");
                    NewOrExistingInput();
                    break;
            }
        }

        public static int SelectLabyrinth()
        {
            Console.Write($"Enter the index of labyrinth (0 - {Labyrinth.labyrinthList.Count - 1}): ");
            int index = 0;
            do
            {
                try
                {
                    index = Convert.ToInt32(Console.ReadLine());
                    if (index > Labyrinth.labyrinthList.Count - 1 || index < 0)
                    {
                        Console.Write($"Incorrect value. Please enter a value from 0 to {Labyrinth.labyrinthList.Count - 1}: ");
                    }
                }
                catch (FormatException)
                {
                    Console.Write($"Incorrect input format. Please enter an integer value: ");
                }
                catch (OverflowException)
                {
                    Console.Write($"Value was too long. Please enter shorter value:");
                }
            }
            while (index < 0 || index > Labyrinth.labyrinthList.Count - 1 || index.GetType() != typeof(int));

            return index;
        }
    }
}
