using System.Collections.Generic;

namespace LabyrinthApplication
{
    internal class Node
    {
        public int X { get; }
        public int Y { get; }

        public int G { get; set; }
        public int H { get; set; }

        public int FCost
        {
            get { return G + H; }
        }

        public NodeType Type = NodeType.Path;
        public Node Parent;

        public Node(int _x, int _y)
        {
            this.X = _x;
            this.Y = _y;
        }

        public List<Node> GetNeighbors(Node[,] labyrinth)
        {
            List<Node> neighbors = new List<Node>();

            int labyrinthRows = labyrinth.GetLength(0);
            int labyrinthColumns = labyrinth.GetLength(1);

            //positions: left, right, up, down
            int[] directionX = { -1, 1, 0, 0 };
            int[] directionY = { 0, 0, 1, -1 };

            //check all directions from the current node
            for (int i = 0; i < 4; i++)
            {
                int newX = X + directionX[i];
                int newY = Y + directionY[i];
                if (newX >= 0 && newX < labyrinthColumns && newY >= 0 && newY < labyrinthRows)
                {
                    Node neighbor = labyrinth[newY, newX];

                    //add node if it is not wall
                    if (neighbor.Type != NodeType.Wall)
                    {
                        neighbors.Add(neighbor);
                    }
                }
            }

            return neighbors;
        }
    }
}
