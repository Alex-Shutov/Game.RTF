using System;
using Game_Prototype.Map.MapObjects;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Game_Prototype
{
    public delegate void MazeDelegate(int index);
    public class Maze
    {
        public  const int SIDE = 90;

        public string[] MapChars = new[]
        {   "00##########################000000000000000000000000000000000000000000000",
            "00#G          #        #####000000000000000000000000000000000000000000000",
            "00#                  ##################################################00",
            "00#                                                                   #00",
            "00#########                                                       E  N#00",
            "00#           G  G  G  G E                                    #########00",
            "00#          #########################################KK######00000000#00",
            "00#          ##0000000000000000000000000000000000000#   #0000000000000#00",
            "00####      ##00000000000000000000000000000000000000#  ##0000000000000#00",
            "00#         ##00000############################000###    ##00000000000#00",
            "00#        ##000000#                          ########KKKK###000000000#00",
            "00#       G##000000#          E                             #000000000#00",
            "00#      B#####0000#  #####################         ###   E #000000000#00",
            "00#     ###    #000#  #0000000000000000000###             ##########00#00",
            "00#             ####  ###################0#    ###           $     #00#00",
            "00##                                    #0#            #    B$     #00#00",
            "00###                             G     #########  ##### ######    #00#00",
            "00#####         ####             ####             ##000# #000###   #00#00",
            "00######        #                                  #000# #0000#    #00#00",
            "00#######       #      #######         #############000# #0000#   ##00#00",
            "00#             #L                     ##      ######### #0000#    #00#00",
            "00#             ###              ######                  #0000##   #00#00",
            "00#   ####      #    E                                    0000#    #00#00",
            "00#      #      #    #########          ###  ###         L0000#   ##00#00",
            "00##     ##                             #            #####0000#    #00#00",
            "00###   ####                   ###     #     G      ######0000##   #00#00",
            "00### ###################################  ###############0000#    #00#00",
            "00### #00000000000000000000000000000000#   #000000000000000000#   ##00#00",
            "00### #0000000000000000000000000000000#   #000000##############    #00#00",
            "00### #000000000000000000000000000000#   #0000000#             #   #00#00",
            "00### #00000000000000000000000000000#   #00000000#             $$$$####00",
            "00### #00000#####000000000000000000#   #000000000                #    #00",
            "00### #000##     ##00000000000000##   #0000000000L G           ###    #00",
            "00### ####         ##############    #####0000000###########          #00",
            "00#                 P     P              #0000000#            E       #00",
            "00#N      E         P     P       #   E  #0000000#            ####    #00",
            "00###########     ##########    ##    ############                #   #00",
            "00                  #00000#                                          ##00",
            "00L   G             #00000#N ##       B           E                 ###00",
            "00#############  ####00000########  #################OO################00",
            "00#00000000000#   #0000000000000#   #000000000000000#   #0000000000000#00",
            "00#00000000000##  #0000000000000#  ##000000000000000##  #0000000000000#00",
            "00#00000000000#   #0000000000000#   #000000000000000#   #0000000000000#00",
            "00#00000000000#  ##0000000000000##   000000000000000#  ##0000000000000#00",
            "00#00000000000#   ##############    L0000000000000###EOO###############00",
            "00#00000000000##                   ###################                #00",
            "00#00000000000##    G       E    #                O                   #00",
            "00#00000000000####################            G   O            E     X#00",
            "00#00000000000#########################################################00",
            "00#0000000000000000000000000000000000000000000000000000000000000000000000",

        };
        public MapCell[,] maze;
        //public static List<IMapObject> arrayLift;
        public static HashSet<Wall> HashSetWalls = new HashSet<Wall>();
        public int counter;
        public MapObjects MapElements;

        public PictureBox MazeBox;

        private MapObjDelegate[] events;
        //public Label tmpLabel;


        public Maze(Player player,MapObjDelegate[] linkMethod)
        {
            
            events = linkMethod;
            MapElements = new MapObjects(player.physics.transform);
            maze = GetEnumMaze();
            MazeBox = new PictureBox()
            {
                BackgroundImage = Sources._123,
                Height = 7500,
                Width = 7500,
            };
            MazeBox.Image = PrintMaze(MazeBox.Width, MazeBox.Height);
            player.eventCamera += CountInvalidate;
        }
        private MapCell[,] GetEnumMaze()
        {
            var rows = MapChars.Length;
            var colums = MapChars[0].Length;
            var mazeEnums = new MapCell[colums, rows];
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < colums; x++)
                {
                    mazeEnums[x, y] = MapChars[y][x] switch
                    {
                        'G' => MapCell.ChestFind,
                        '#' => MapCell.Wall,
                        'L' => MapCell.Lift,
                        'B' => MapCell.BanditFind,
                        //'T' => MapCell.Lift,
                        '0' => MapCell.BlackWall,
                        'O' => MapCell.KeyWall,
                        'X' => MapCell.Exit,
                        _ => MapCell.Empty
                    };
                }
            }
            return mazeEnums;
        }

        public void CameraMoveLeftRigth(int velocity)
        {
            MazeBox.Left -= velocity;
        }
        private void CountInvalidate(object? obj, EventArgs args) => counter = 0;
        public void CameraMoveDown(int velocity)
        {
            if (counter <= 30 && MazeBox.Top < 3500 )
            {
                MazeBox.Top -= velocity;
                counter++;
            }
        }
        public void CameraMoveUp(int velocity) 
        {
            if (counter >= -30 && MazeBox.Top < 0)
            {
                MazeBox.Top += velocity;
                counter--;
            }
        }

        public Bitmap PrintMaze(int height, int width)
        {
            var t = new Bitmap(width, height);
            
            var g = Graphics.FromImage(t);

            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    switch (maze[x,y])
                    {
                        case MapCell.BlackWall:
                            g.DrawImage(Sources._1, x * SIDE - 1, y * SIDE, SIDE, SIDE);
                            HashSetWalls.Add(new Wall(x * SIDE, y * SIDE, SIDE, SIDE));
                            break;
                        case MapCell.KeyWall:
                            var keyWal = new KeyWall(x, y, SIDE, events, new Image[] {Sources.KeyWall, null});
                            MazeBox.Controls.Add(keyWal.picture);
                            MapElements.ListObjects.Add(keyWal);
                            HashSetWalls.Add(keyWal);
                            break;
                        case MapCell.BanditFind:
                            var npc = new NPC2(x, y, SIDE, events);
                            MapObjects.ListBandits.Add(MapObjects.ListBandits.Count, npc);
                            break;
                        case MapCell.Lift:
                            var lift = new Lift(x, y, SIDE, events, Sources.Lift);
                            MazeBox.Controls.Add(lift.picture);
                            MapElements.ListObjects.Add(lift);
                            break;
                        case MapCell.Wall:
                            g.DrawImage(Sources.Land_5, x * SIDE-1, y * SIDE, SIDE, SIDE);
                            HashSetWalls.Add(new Wall(x * SIDE, y * SIDE, SIDE, SIDE));
                            break;
                        case MapCell.ChestFind:
                            var chest = new Chest(x, y, SIDE, events, new Image[] { Sources.Syndyk, Sources.Syndyk_Open });
                            chest.CreateRandomFilling();
                            MazeBox.Controls.Add(chest.picture);
                            MapElements.ListObjects.Add(chest);
                            break;
                        case MapCell.Exit:
                            var exit = new ExitMap(x, y, SIDE, events, Sources.basket);
                            MazeBox.Controls.Add(exit.picture);
                            MapElements.ListObjects.Add(exit);
                            break;;


                    }
                }
            }
            MapElements.CreateMapObjects(maze,AddControlsToMazePictureBox);
            return t;

        }

        public void AddControlsToMazePictureBox(int index)
        {
            MapObjects.ListBandits.TryGetValue(index, out var npc);
            var npcNotNull = npc ?? new NPC2(-10000, 10000, 0, null);
            MazeBox.Controls.Add(npcNotNull.picture);
            MapElements.ListObjects.Add(npcNotNull);
            

        }
    }
}
