using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Game_Prototype
{
    public static class ElementsOfMapGenerator
    {
        internal class Node
        {
            internal Node previous { get; }
            internal Point location;

            internal Node(Point loc, Node prev = null)
            {
                location = loc;
                previous = prev;
            }

        }
        private static readonly Random randomCountSteps = new Random(DateTime.Now.Millisecond ^ 1273214);
        public static IEnumerable<List<Point>> CreateNewLocation(Point start, MapCell[,] mazeCells)
        {
            var node = new Node(start);
            var stack = new Stack<Node>();
            var visited = new HashSet<Point>();
            var rnd = randomCountSteps.Next(1, 2);
            stack.Push(node);
            visited.Add(start);
            while (stack.Count != 0)
            {
                var element = stack.Pop();
                if (rnd <= 0)
                    yield return ReversePath(element);
                rnd--;
                foreach (var point in GetNeighbours(element, mazeCells).Where(x => !visited.Contains(x.location)))
                {
                    visited.Add(point.location);
                    stack.Push(point);
                }

            }
        }

        private static List<Point> ReversePath(Node node)
        {
            var tmp = new List<Point>();
            while (node.previous != null)
            {
                tmp.Add(node.location);
                node = node.previous;
            }

            tmp.Reverse();
            return tmp;
        }

        private static List<Node> GetNeighbours(Node cell, MapCell[,] maze)
        {
            var listNeighbour = new List<Node>();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    var incidentPoint = new Point((cell.location.X / Maze.SIDE), (cell.location.Y / Maze.SIDE));
                    if (maze[incidentPoint.X + j, incidentPoint.Y + i] ==MapCell.Empty)
                    {
                        listNeighbour.Add(new Node(new Point((incidentPoint.X + j) * Maze.SIDE, (incidentPoint.Y + i) * Maze.SIDE), cell));
                    }
                }
            }

            return listNeighbour;
        }
    }


}
