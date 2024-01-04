using System;
using System.Collections.Generic;

namespace LabyrinthApplication
{
    internal class Pathfinding
    {
        public Pathfinding()
        {

        }

        public List<Node> FindPath(Node[,] labyrinth)
        {
            //find the start and end nodes
            Node startNode = FindNode(NodeType.Start, labyrinth);
            Node endNode = FindNode(NodeType.Target, labyrinth);

            List<Node> openSet = new List<Node>();  //set of nodes to be evaluated
            HashSet<Node> closedSet = new HashSet<Node>();  //set of nodes already evaluated
            List<Node> path = new List<Node>();  //path from end node to start node

            openSet.Add(startNode);

            //main loop, goes until all nodes were evaluated
            while (openSet.Count > 0)
            {
                Node currentNode = FindLowestFCostNode(openSet);  //find the lowest f cost node
                if (currentNode == endNode)  //if current node is end node, path has been found
                {
                    path = ReconstructPath(currentNode);
                    return path;
                }

                //node has been evaluated and added to the closed set                
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                List<Node> nodeNeighbors = currentNode.GetNeighbors(labyrinth);

                foreach (Node neighbor in nodeNeighbors)
                {
                    //continue if the neighbor node is in closed set or it is a wall
                    if (closedSet.Contains(neighbor) || neighbor.Type == NodeType.Wall)
                        continue;

                    //calculating the g cost of the neighbor
                    int G = currentNode.G + 1;

                    if (openSet.Contains(neighbor))
                    {
                        //skip the neighbor if the cost from the start node is bigger
                        if (G >= neighbor.G) continue;
                    }
                    else
                    {
                        //otherwise add it to the open set
                        openSet.Add(neighbor);
                    }

                    //update neighbor parent, G cost and H cost
                    neighbor.Parent = currentNode;
                    neighbor.G = G;
                    neighbor.H = CalculateHeuristic(neighbor, endNode);
                }
            }

            return null;
        }

        public Node FindNode(NodeType typeToFind, Node[,] labyrinth)
        {
            //finds the node based on specified type
            for (int i = 0; i < labyrinth.GetLength(0); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(1); j++)
                {
                    if (labyrinth[i, j].Type == typeToFind) return labyrinth[i, j];
                }
            }
            return null;
        }

        public Node FindLowestFCostNode(List<Node> openSet)
        {
            Node lowestFCostNode = openSet[0];

            for (int i = 0; i < openSet.Count; i++)
            {
                // change the lowestFCostNode if:
                // the f cost of the current node in iteration is smaller than the f cost of the current lowestFCostNode
                // OR
                // the f cost of the current node in iteration is equal to the f cost of the current lowestFCostNode,
                // AND
                // the current node's heuristic (H) is smaller than the lowestFCostNode's heuristic (H)
                if (lowestFCostNode.FCost > openSet[i].FCost || (lowestFCostNode.FCost == openSet[i].FCost && lowestFCostNode.H > openSet[i].H))
                {
                    lowestFCostNode = openSet[i];
                }
            }
            return lowestFCostNode;
        }

        public int CalculateHeuristic(Node nodeA, Node nodeB)
        {
            int H = Math.Abs(nodeA.X - nodeB.X) + Math.Abs(nodeA.Y - nodeB.Y);
            return H;
        }


        public List<Node> ReconstructPath(Node endNode)
        {
            //reconstructs the path from endNode to startNode based on parents
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != null)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Reverse();
            return path;
        }

        public static void PrintPath(List<Node> path)
        {

            for (int i = 0; i < path?.Count; i++)
            {
                Console.Write($"[{path[i].X},{path[i].Y}] ");
            }
            Utils.AddEmptyLines(1);
        }
    }
}
