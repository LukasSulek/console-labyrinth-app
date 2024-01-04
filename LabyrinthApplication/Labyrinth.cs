using System;
using System.Collections.Generic;

namespace LabyrinthApplication
{
    internal class Labyrinth
    {
        public static List<Labyrinth> labyrinthList = new List<Labyrinth>();

        private Node[,] labyrinthArray;
        public Node[,] LabyrinthArray
        {
            get { return labyrinthArray; }
            set { labyrinthArray = value; }
        }

        public Labyrinth(int[] values, int x, int y)
        {
            this.labyrinthArray = CreateLabyrinth(values, x, y);
        }

        public Node[,] CreateLabyrinth(int[] values, int x, int y)
        {
            Node[,] labyrinth = new Node[y, x];
            int count = 0;

            //create node for each value
            for (int i = 0; i < labyrinth.GetLength(0); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(1); j++)
                {
                    Node node = new Node(j, i);
                    ///assign the value to node based on the value
                    switch (values[count])
                    {
                        case 0:
                            node.Type = NodeType.Path;
                            break;
                        case 1:
                            node.Type = NodeType.Wall;
                            break;
                        case 2:
                            node.Type = NodeType.Target;
                            break;
                        default:
                            Console.WriteLine("NodeType does not exist");
                            break;
                    }

                    labyrinth[i, j] = node;
                    count++;
                }
            }
            //set the position[0,0] to Start
            labyrinth[0, 0].Type = NodeType.Start;
            return labyrinth;
        }

        public void PrintLabyrinth()
        {
            //printh the value of nodes
            for (int i = 0; i < labyrinthArray.GetLength(0); i++)
            {
                for (int j = 0; j < labyrinthArray.GetLength(1); j++)
                {
                    switch (labyrinthArray[i, j].Type)
                    {
                        case NodeType.Start:
                        case NodeType.Path:
                            Console.Write("0 ");
                            break;
                        case NodeType.Wall:
                            Console.Write("1 ");
                            break;
                        case NodeType.Target:
                            Console.Write("2 ");
                            break;
                    }
                }
                Console.WriteLine();
            }

            Utils.AddEmptyLines(2);

            //print the positions of nodes
            for (int i = 0; i < labyrinthArray.GetLength(0); i++)
            {
                for (int j = 0; j < labyrinthArray.GetLength(1); j++)
                {
                    Console.Write($"[{labyrinthArray[i, j].X},{labyrinthArray[i, j].Y}] ");
                }
                Console.WriteLine();
            }
        }
    }
}
