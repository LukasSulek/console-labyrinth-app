using System;

namespace LabyrinthApplication
{
    internal class Utils
    {
        public static void AddEmptyLines(int numberOfLines)
        {
            for (int i = 0; i < numberOfLines; i++)
            {
                Console.WriteLine();
            }
        }
    }
}
