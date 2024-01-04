using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LabyrinthApplication
{
    internal class LabyrinthApp
    {
        public void Run()
        {
            Console.Clear();
            Console.Write("Use 'NEW' command to create new labyrinth or 'SELECT' to select existing one: ");
            InputHandler.NewOrExistingInput();

            Utils.AddEmptyLines(3);
            Console.WriteLine($"Press Enter key to end or Escape key to reset console");
            InputHandler.ResetOrCloseConsole(this);
        }

        public static void SelectExistingLabyrinth()
        {
            if (Labyrinth.labyrinthList.Count > 0)
            {
                Labyrinth.labyrinthList[InputHandler.SelectLabyrinth()].PrintLabyrinth();
            }
            else
            {
                Console.WriteLine("List of labyrinths is empty. Please create new labyrinth first.");
                CreateNewLabyrinth();
            }
        }

        public static void CreateNewLabyrinth()
        {
            InputHandler inputHandler = new InputHandler();
            int labyrinthWidth = inputHandler.GetLabyrinthParameter(2, "Enter the width of the labyrinth - (minimum value = 2): ");
            Utils.AddEmptyLines(1);

            int labyrinthHeight = inputHandler.GetLabyrinthParameter(2, "Enter the height of the labyrinth (minimum value = 2): ");
            Utils.AddEmptyLines(1);

            int[] values = inputHandler.GetValues(labyrinthWidth * labyrinthHeight); Utils.AddEmptyLines(1);

            Labyrinth labyrinth = new Labyrinth(values, labyrinthWidth, labyrinthHeight); Utils.AddEmptyLines(4);
            Labyrinth.labyrinthList.Add(labyrinth);

            labyrinth.PrintLabyrinth(); Utils.AddEmptyLines(2);

            Pathfinding pathfinding = new Pathfinding();

            List<Node> path = pathfinding.FindPath(labyrinth.LabyrinthArray);
            if (path != null)
            {
                Console.Write("Path:  ");
                Pathfinding.PrintPath(path);
            }
            else
            {
                Console.WriteLine("The path not found");
            }

        }
    }
}
