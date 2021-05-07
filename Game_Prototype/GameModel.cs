using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using tainicom;
using tainicom.Aether.Physics2D.Collision;

namespace Game_Prototype
{
    class GameModel
    {
        public Player player;
        public Maze maze;
        public Label tmpLabel;
        public PictureBox box;
        public HashSet<Rectangle> HashSetRect;
        private bool jumping;
        private int velocityForCamera;

        public GameModel(int plX, int plY, int MazeHeight, int MazeWidth)
        {
            //force = 8;
            HashSetRect = new HashSet<Rectangle>();
            velocityForCamera = 5;
            maze = MakeMaze(MazeHeight, MazeWidth);
            player = new Player(new Point(plX, plY), new Size(107, 132),HashSetRect);
            
            #region

            //dictActions = new Dictionary<Keys, Action>()
            //{
            //    {Keys.W, () => Jump(DateTime.Now.Millisecond/100f)},
            //    {Keys.A, () => player.Move(player.CreatureBox.Location.X - 4, player.CreatureBox.Location.Y)},
            //    {Keys.S, () => player.Move(player.CreatureBox.Location.X, player.CreatureBox.Location.Y + 4)},
            //    {Keys.D, () => player.Move(player.CreatureBox.Location.X + 4, player.CreatureBox.Location.Y)}
            //};
            //rect = new HashSet<Rectangle>();
            //grav = new Physics(player.transform.position, player.transform.size);
            #endregion

            box = new PictureBox()
            {
                BackgroundImage = Sources._123,
                Height = 1100,
                Width = 2000,

            };
            tmpLabel = new Label()
            {
                Text = UpdateLabel(),
                Size = new Size(200, 200),
                Location = new Point(1000,0)
            };
            box.Paint += new PaintEventHandler(DrawPlayer);
            box.Controls.Add(tmpLabel);

        }
        public void KeyIsDownForPlayer(KeyEventArgs e)
        {
            
            switch (e.KeyCode)
            {
                case Keys.A:
                    player.permutation.goLEFT = true;
                    player.image = Sources.idle_3_1;
                    player.image.RotateFlip(RotateFlipType.Rotate180FlipY);
                    player.dx = 8;
                    break;
                case Keys.D:
                    player.permutation.goRIGHT = true;
                    player.image = Sources.idle_3_1;
                    player.dx = 4;
                    
                    break;
                case Keys.W when !jumping:
                    jumping = true;
                    player.permutation.goUP = true;
                    break;
                
            }
            #region MyRegion

            //switch (e.KeyCode)
            //{
            //    case Keys.A:
            //        goLeft = true;
            //        break;
            //    case Keys.D:
            //        goRight = true;
            //        break;
            //    case Keys.W when !jumping:
            //        jumping = true;
            //        break;
            //}
            #endregion
        }

        public async void MainTimerEvent(int rightSide, EventArgs e)
        {

            //TODO Переделать управление -> лагает

            //player.CreatureBox.Top += jumpVelocity;
            #region MyRegion
            //if (goLeft && player.physics.transform.position.X > 60)
            //{
            //    player.physics.transform.position.X -= velocityForPlayer;
            //}

            //else if (goRight && player.physics.transform.position.X + player.physics.transform.size.Width < 1600)
            //{
            //    player.physics.transform.position.X += velocityForPlayer;
            //}

            ////if (goLeft && box.Left < 0)
            ////{
            ////    box.Left += velocityForCamera;
            ////    tmpLabel.Left -= velocityForCamera;
            ////} 
            #endregion

            //player.physics.transform.position.X = player.Move(player.physics.transform.position.X, player.permutation.velocityForPlayer, 0);
            player.UpdatePlayer();
            //TODO вынести код ниже в карту
            if (player.permutation.goRIGHT && box.Left > -500 )
            {
                box.Left -= velocityForCamera;
                tmpLabel.Left += velocityForCamera;
            }
            if (player.permutation.goLEFT && box.Left < 0 )
            {
                box.Left += velocityForCamera;
                tmpLabel.Left -= velocityForCamera;
            }

            box.Refresh();
            tmpLabel.Text = UpdateLabel();

        }

        

        private void DrawPlayer(object sender, PaintEventArgs e)
        {
            player.DrawSprites(e.Graphics, player.image, Rectangle.Empty, Rectangle.Empty);
            
        }

        private void CheckIntersections()
        {
            //foreach (Control xControl in box.Controls)
            //{
            //    if (xControl is PictureBox && (string)xControl.Tag == "wall")
            //        if (player.CreatureBox.Bounds.IntersectsWith(xControl.Bounds) && jumping == false)
            //        {
            //            force = 8;
            //            //jumping = false;
            //            //player.CreatureBox.Top = xControl.Top - player.CreatureBox.Height;
            //            jumpVelocity = 0;
            //        }
            //    xControl.BringToFront();
            //}
        }
        public void KeyIsUpForPlayer(KeyEventArgs e)
        {
            #region MyRegion

            switch (e.KeyCode)
            {
                case Keys.D:
                    player.permutation.goRIGHT = false;
                    break;
                case Keys.A:
                    player.permutation.goLEFT = false;
                    break;
            }

            if (jumping)
            {
                jumping = false;
                player.permutation.goUP = false;
                player.permutation.goDOWN = true;
            }
            #endregion
        }
        public string UpdateLabel()
        {
            return $"HP: {player.HP}\nIsAgressive: {player.isAgressive}\nLocation:{player.physics.transform.position}\nLeft:{player.permutation.goLEFT}\nRight:{player.permutation.goRIGHT}\nDown:{player.permutation.goDOWN}\nUp:{player.permutation.goUP}";
        }

        //public void Jump(float a)
        //{
        //    if ((keysFromForm & Keys.W) == Keys.W) 
        //       player.CreatureBox.Location = new Point(player.CreatureBox.Location.X, player.CreatureBox.Location.Y-1);
        //}

        private static Maze MakeMaze(int MazeWidth, int MazeHeight)
        {
            var tmp = new Maze(MazeWidth, MazeHeight);
            for (int i = 0; i < 6; i++)
                tmp.maze = tmp.Generate();
            return tmp;
        }

        public void PrintMaze()
        {
            var t = new Bitmap(box.Width, box.Height);
            var side = 95;
            var g = Graphics.FromImage(t);
           
            
            for (int x = 0; x < maze.maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.maze.GetLength(1); y++)
                {
                    if (maze.maze[x, y] == MapCell.Wall)
                    {
                        g.DrawImage(Sources.Tile_1, x * side, y * side, side, side);
                        #region MyRegion
                        //rect.Add(new Rectangle(x * side, y * side, side, side));
                        //var e = new PictureBox()
                        //{
                        //    Image = Sources.Tile_1,
                        //    Tag = "wall",
                        //    Location = new Point(x * side, y * side),
                        //    Size = new Size(side, side)
                        //};
                        //box.Controls.Add(e); 
                        #endregion

                        HashSetRect.Add(new Rectangle(x * side, y * side, side, side));

                    }
                }
            }
            box.Image = t;

        }

        public Maze CheckMaze(int MazeWidth, int MazeHeight)
        {
            var tmp = new Maze(MazeWidth, MazeHeight)
            {
                maze = new [,]
                {
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty},
                    {MapCell.Empty, MapCell.Empty,MapCell.Wall, MapCell.Wall, MapCell.Empty, MapCell.Empty, MapCell.Empty},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Wall, MapCell.Wall},
                }
            };
            return tmp;
        }





    }
}
