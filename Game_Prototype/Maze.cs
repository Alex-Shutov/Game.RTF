using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Game_Prototype
{
    class Maze
    {
        //public int Rows { get; set; }
        //public int Cols { get; set; }
       // public float RandCoef { get; private set;  }
        public MapCell[,] maze;
        private readonly int deathLimit = 3;
        private readonly int birthLimit = 4;
        private readonly Random rnd = new Random(DateTime.Now.Millisecond ^ 17349);

        public Maze( int rows, int cols)
        {
            maze = GetRandomMaze(0.45f,cols,rows);
        }

        private MapCell[,] GetRandomMaze(float coef, int Cols, int Rows)
        {
            //var random = new Random(DateTime.Now.Millisecond ^ 17349);
            maze = new MapCell[Cols, Rows];
            for (int x = 0; x < Cols; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    //maze[x, y] =  rnd.Next(2) == 0;
                    //if ((float) rnd.NextDouble() < coef)
                    //    maze[x, y] = MapCell.Wall;
                    //else maze[x, y] = MapCell.Empty;
                    maze[x, y] = (float) rnd.NextDouble() < coef ? MapCell.Wall : MapCell.Empty;
                }
            }

            return maze;
        }

        private int CountNeighbours(int x, int y)
        {
            var count = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    //   var curX = (x + i + cols) % cols;
                    // var curY = (y + j + rows) % rows;
                    var curX = x + i;
                    var curY = y + j;
                    //if (currentCol == x && currentRow = y)
                    if ((i == 0 && j == 0) || (curX < 0 || curY < 0))
                        continue;
                    if (curX >= maze.GetLength(0) || curY >= maze.GetLength(1) || maze[curX, curY] == MapCell.Wall)
                        count++;
                    //else if (maze[curX, curY])
                    // count++;

                }
            }

            return count;
        }

        public MapCell[,] Generate()
        {
            var tmp = new MapCell[maze.GetLength(0), maze.GetLength(1)];

            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    var countNeighbour = CountNeighbours(x, y);
                    if (maze[x, y]  == MapCell.Wall)
                        if (countNeighbour < deathLimit)
                            tmp[x, y] = MapCell.Empty;
                        else
                            tmp[x, y] = MapCell.Wall;
                    else if (countNeighbour > birthLimit)
                        tmp[x, y] = MapCell.Wall;
                    else
                        tmp[x, y] = MapCell.Empty;
                    //if (maze[x, y] == MapCell.Wall && countNeighbour == 4)
                    //if (maze[x, y] == MapCell.Empty && countNeighbour == 5)
                    //tmp[x,y] = MapCell.Wall;
                    //else if (maze[x, y] == MapCell.Wall && countNeighbour == 4)
                    //  tmp[x, y] = MapCell.Wall;
                    //else tmp[x, y] = maze[x, y];

                    //else
                    //    maze[x, y] = MapCell.Empty;
                    // tmp[x, y] = maze[x, y];
                    //if (tmp[x,y])
                    //    graphics.FillRectangle(Brushes.Blue, x * SIDE, y * SIDE, SIDE, SIDE);
                }
            }

            //MazeBox.Refresh();
            maze = tmp;
            return maze;
            //return maze;
        }

        public MapCell[,] GetMaze()
        {
            var res = new MapCell[maze.GetLength(0), maze.GetLength(1)]; 
            Array.Copy(maze,res,res.Length);
            //return res;
            //for (int x = 0; x < maze.GetLength(0); x++)
            //{
            //    for (int y = 0; y < maze.GetLength(1); y++)
            //    {
            //        res[x, y] = maze[x, y];
            //    }
            //}
            return res;
        }
    }
}
