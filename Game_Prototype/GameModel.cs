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

namespace Game_Prototype
{
    class GameModel
    {
        public Player player;
        public Maze maze;
        public Label tmpLabel;
        //public Keys press;
        public PictureBox box;
       private Physics physics;

       private Graphics gr;
        //private 
        private bool goRight, goLeft, jumping;
        private int force, velocityForCamera, velocityForPlayer,jumpVelocity;

        public Keys keysFromForm { get; set; }
        public GameModel(int plX, int plY, int MazeHeight, int MazeWidth)
        {
            force = 8;
            velocityForCamera = 8;
            velocityForPlayer = 12;
            maze = MakeMaze(MazeHeight, MazeWidth);
            player = new Player(new Point(plX, plY), new Size(107, 132));
            tmpLabel = new Label()
            {
                Text = UpdateLabel(),
                Size = new Size(200, 200)
            };
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
                //Location = new Point(-100, -100),
                BackgroundImage = Sources._123,
                Height = 1100,
                Width = 2000,

            };
            box.Paint += new PaintEventHandler(DrawPlayer);

        }
        //public void MakePermutations
        public void KeyIsDownForPlayer(KeyEventArgs e)
        {
            
            switch (e.KeyCode)
            {
                case Keys.A:
                    goLeft = true;
                    player.image = Sources.idle_3_1;
                    player.image.RotateFlip(RotateFlipType.Rotate180FlipY);
                    player.dx = 8;
                    break;
                case Keys.D:
                    goRight = true;
                    player.image = Sources.idle_3_1;
                    player.dx = 4;
                    
                    break;
                case Keys.W when !jumping:
                    jumping = true;
                    
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
            #region MyRegion



            //player.CreatureBox.Top += jumpVelocity;
            if (goLeft && player.physics.transform.position.X > 60  )
            {
              //await Task.Run(() => player.transform.position.X -= velocityForPlayer);
                player.physics.transform.position.X -= velocityForPlayer;
                //return;
            }

            else if (goRight && player.physics.transform.position.X + player.physics.transform.size.Width <1600 )
            {
                //await Task.Run(() => player.transform.position.X += velocityForPlayer);
                player.physics.transform.position.X += velocityForPlayer;
                //return;
            }

            if (goLeft && box.Left < 0)
            {
                //await Task.Run(() => box.Left += velocityForCamera);  
                 box.Left += velocityForCamera;
                //  return;
                if (jumping)
                {
                    player.physics.AddForce();
                    player.physics.ApplyPhysics();
                    // box.Refresh();
                }
                else
                {
                    player.physics.DownForce();
                }
                box.Refresh();
                return;
            }

            if (goRight && box.Left > -500)
            {
                //await Task.Run(() => box.Left -= velocityForCamera);
                box.Left -= velocityForCamera;
                
                if (jumping)
                {
                    player.physics.AddForce();
                    player.physics.ApplyPhysics();
                   // box.Refresh();
                }
                else
                {
                    player.physics.DownForce();
                }
                box.Refresh();
                return;
            }

            //if (jumping)
            //{
            //    player.physics.AddForce();
            //    player.physics.ApplyPhysics();
            //    box.Refresh();
            //    return;
            //}

            player.physics.DownForce();
                box.Refresh();
                #endregion
           
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
                    goRight = false;
                    //player.image = Sources.idle_3_1;
                    //player.dx = 5;
                    break;
                case Keys.A:
                    goLeft = false;
                    //player.image = Sources.idle_3_1;
                    //player.image.RotateFlip(RotateFlipType.Rotate180FlipY);
                    //player.dx = 8;
                    break;
            }

            if (jumping)
            {
                jumping = false;
            }
            #endregion
        }
        public void PlayerMove()
        {
            #region MyRegion




            //switch (keysFromForm)
            //{
            //    case Keys.W:
            //        Jump(DateTime.Now.Millisecond / 100f);
            //        break;
            //    case Keys.A:
            //        player.Move(player.CreatureBox.Location.X - 1, player.CreatureBox.Location.Y);
            //        break;
            //    case Keys.S:
            //        player.Move(player.CreatureBox.Location.X, player.CreatureBox.Location.Y + 1);
            //        break;
            //    case Keys.D:
            //        player.Move(player.CreatureBox.Location.X+1, player.CreatureBox.Location.Y);
            //        break;
            //}

            #endregion

            //    //dictActions.TryGetValue(keysFromForm, out var action);

            //    //switch (keysFromForm)
            //    //{
            //    //    case Keys.D:
            //    //        MoveRight();
            //    //        break;
            //    //    case Keys.A:
            //    //        MoveLeft();
            //    //        break;
            //    //}
            //    //var tmpbool = rect.Any(elRectangle => elRectangle.IntersectsWith(player.CreatureBox.Bounds));
            //    // var tmp = rect.Any(elRectangle => new Rectangle(elRectangle.X+1,elRectangle.Y+1,elRectangle.Width,elRectangle.Height).IntersectsWith(player.CreatureBox.Bounds));

            //    //foreach (Control x in box.Controls)
            //    //{
            //    //    //box.FindForm().Controls.Add(x);
            //    //    //x.Location = x.PointToScreen(new Point());
            //    //    if ((string)x.Tag == "wall")
            //    //    {
            //    //        if (player.CreatureBox.Bounds.IntersectsWith(x.Bounds))
            //    //            return;

            //    //    }


            //    //    //   //s player.CreatureBox.BringToFront();
            //    //    //}

            //    //}
            //    dictActions[keysFromForm].Invoke();
            //       // action?.Invoke();
            //        //tmpLabel.Text =UpdateLabel();
            //}
        }


        public string UpdateLabel()
        {
            return "   ";
            //$"HP: {player.HP}\nIsAgressive: {player.isAgressive}\nLocation:{player.CreatureBox.Location}\nPress:{keysFromForm}";
        }

        public void Jump(float a)
        {
            if ((keysFromForm & Keys.W) == Keys.W) 
               player.CreatureBox.Location = new Point(player.CreatureBox.Location.X, player.CreatureBox.Location.Y-1);
        }

        private static Maze MakeMaze(int MazeWidth, int MazeHeight)
        {
            var tmp = new Maze(MazeWidth, MazeHeight);
            for (int i = 0; i < 6; i++)
                tmp.maze = tmp.Generate();
            return tmp;
        }

        public void PrintMaze()
        {
            //  box.Image = new Bitmap(box.Width, box.Height);
            //graphics = Graphics.FromImage(box.Image);
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
                        //rect.Add(new Rectangle(x * side, y * side, side, side));
                        //var e = new PictureBox()
                        //{
                        //    Image = Sources.Tile_1,
                        //    Tag = "wall",
                        //    Location = new Point(x * side, y * side),
                        //    Size = new Size(side, side)
                        //};
                        //box.Controls.Add(e);
                    }
                }
            }

            box.Image = t;
            // box.Controls.Add(player.CreatureBox);
            //player.CreatureBox.BringToFront();
            
        }

        public Maze CheckMaze(int MazeWidth, int MazeHeight)
        {
            var tmp = new Maze(MazeWidth, MazeHeight)
            {
                maze = new [,]
                {
                    {MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty},
                    {MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty},
                    {MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty},
                    {MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty},
                    {MapCell.Empty, MapCell.Wall, MapCell.Wall, MapCell.Wall, MapCell.Wall},
                }
            };
            return tmp;
        }





    }
}
