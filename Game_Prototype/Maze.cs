using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Game_Prototype
{
    public class Maze
    {

        public string[] MapChars = new string[]
        {
            "00#                   #################################################00",
            "00#                                                                   #00",
            "00#########                                                       E  N#00",
            "00#                       E                                   #########00",
            "00#          #########################################KK######00000000#00",
            "00#B         ##0000000000000000000000000000000000000#   #0000000000000#00",
            "00####      ##00000000000000000000000000000000000000#  ##0000000000000#00",
            "00#         ##00000############################000###    ##00000000000#00",
            "00#        ##000000#                          ########KKKK###000000000#00",
            "00#        ##000000#          E                             #000000000#00",
            "00#       #####0000#  #####################         ###   E #000000000#00",
            "00#     ###    #000#  #                   ###             ##########00#00",
            "00#             ####  ################### #    ###           $     #00#00",
            "00##                                    # #            #    B$     #00#00",
            "00###                             G     #########  ##### ######    #00#00",
            "00#####         ####             ####             ##000# #000###   #00#00",
            "00######        #                                  #000# #0000#    #00#00",
            "00#######       #T     #######         #############000# #0000#   ##00#00",
            "00#             #L                     ##      ######### #0000#    #00#00",
            "00#             ###              ######                  #0000##   #00#00",
            "00#   ####      #    E                                   T0000#    #00#00",
            "00#      #      #    #########          ###  ###         L0000#   ##00#00",
            "00##     ##                             #            #####0000#    #00#00",
            "00###  B####                   ###     #     G      ######0000##   #00#00",
            "00### ###################################  ###############0000#    #00#00",
            "00### #00000000000000000000000000000000#   #000000000000000000#   ##00#00",
            "00### #0000000000000000000000000000000#   #000000##############    #00#00",
            "00### #000000000000000000000000000000#   #0000000#           ###   #00#00",
            "00### #00000000000000000000000000000#   #00000000#             $$$$####00",
            "00### #00000#####000000000000000000#   #000000000T               #    #00",
            "00### #000##     ##00000000000000##   #0000000000L G           ###    #00",
            "00### ####         ##############    #####0000000###########          #00",
            "00#                 P     P              #0000000#            E       #00",
            "00#N      E         P     P       ##  E  #0000000#            ####    #00",
            "00###########     ##########    ##    ############                #   #00",
            "00T                 #00000#                                          ##00",
            "00L   G             #00000#N ##                   E                 ###00",
            "00#############  ####00000########  #################OO################00",
            "00#00000000000#   #0000000000000#   #000000000000000#   #0000000000000#00",
            "00#00000000000##  #0000000000000#  ##000000000000000##  #0000000000000#00",
            "00#00000000000#   #0000000000000#   #000000000000000#   #0000000000000#00",
            "00#00000000000#  ##0000000000000##  T000000000000000#  ##0000000000000#00",
            "00#00000000000#   ##############    L0000000000000###E  ###############00",
            "00#00000000000##                   ###################                #00",
            "00#00000000000##    G       E    #                O                   #00",
            "00#00000000000####################                O    G       E     X#00",
            "00#00000000000#########################################################00",
            "00#0000000000000000000000000000000000000000000000000000000000000000000000",

        };
        public MapCell[,] maze;
        private readonly int deathLimit = 3;
        private readonly int birthLimit = 4;
        private readonly Random rnd = new (DateTime.Now.Millisecond ^ 17349);

        public static HashSet<Rectangle> HashSetWalls = new HashSet<Rectangle>();

        public Maze()
        {
            maze = GetEnumMaze();
        }

        private MapCell[,] GetEnumMaze()
        {
            //var random = new Random(DateTime.Now.Millisecond ^ 17349);
            var rows = MapChars.Length;
            var colums = MapChars[0].Length;
            var mazeEnums = new MapCell[colums,rows];
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < colums; x++)
                {
                   // maze[x, y] = (float) rnd.NextDouble() < coef ? MapCell.Wall : MapCell.Empty;
                   //mazeEnums[x, y] = MapChars[y][x] == '#' ? MapCell.Wall : MapCell.Empty;
                   switch (MapChars[y][x])
                   {
                       case '#':
                            mazeEnums[x, y] = MapCell.Wall;
                            break;
                        case 'L':
                            mazeEnums[x, y] = MapCell.Wall;
                            break;
                       case 'T':
                           mazeEnums[x, y] = MapCell.Wall;
                           break;
                       default:
                           mazeEnums[x, y] = MapCell.Empty;
                           break;
                    }
                }
            }

            return mazeEnums;
        }

        //private int CountNeighbours(int x, int y)
        //{
        //    var count = 0;

        //    for (int i = -1; i <= 1; i++)
        //    {
        //        for (int j = -1; j <= 1; j++)
        //        {
        //            var curX = x + i;
        //            var curY = y + j;
        //            if ((i == 0 && j == 0) || (curX < 0 || curY < 0))
        //                continue;
        //            if (curX >= maze.GetLength(0) || curY >= maze.GetLength(1) || maze[curX, curY] == MapCell.Wall)
        //                count++;

        //        }
        //    }

        //    return count;
        //}

        //public MapCell[,] Generate()
        //{
        //    var tmp = new MapCell[maze.GetLength(0), maze.GetLength(1)];

        //    for (int x = 0; x < maze.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < maze.GetLength(1); y++)
        //        {
        //            var countNeighbour = CountNeighbours(x, y);
        //            if (maze[x, y]  == MapCell.Wall)
        //                if (countNeighbour < deathLimit)
        //                    tmp[x, y] = MapCell.Empty;
        //                else
        //                    tmp[x, y] = MapCell.Wall;
        //            else if (countNeighbour > birthLimit)
        //                tmp[x, y] = MapCell.Wall;
        //            else
        //                tmp[x, y] = MapCell.Empty;
        //            #region MyRegion
        //            //if (maze[x, y] == MapCell.Wall && countNeighbour == 4)
        //            //if (maze[x, y] == MapCell.Empty && countNeighbour == 5)
        //            //tmp[x,y] = MapCell.Wall;
        //            //else if (maze[x, y] == MapCell.Wall && countNeighbour == 4)
        //            //  tmp[x, y] = MapCell.Wall;
        //            //else tmp[x, y] = maze[x, y];

        //            //else
        //            //    maze[x, y] = MapCell.Empty;
        //            // tmp[x, y] = maze[x, y];
        //            //if (tmp[x,y])
        //            //    graphics.FillRectangle(Brushes.Blue, x * SIDE, y * SIDE, SIDE, SIDE); 
        //            #endregion
        //        }
        //    }
        //    maze = tmp;
        //    return maze;
        //}

        public Bitmap PrintMaze(int width, int height)
        {
            var t = new Bitmap(width, height);
            var side =90;
            var g = Graphics.FromImage(t);


            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    if (maze[x, y] == MapCell.Wall)
                    {
                        g.DrawImage(Sources.Tile_1, x * side, y * side, side, side);
                        #region MyRegion

                        #endregion

                        HashSetWalls.Add(new Rectangle(x * side, y * side, side, side));

                    }
                }
            }

            return t;

        }
    }
}
