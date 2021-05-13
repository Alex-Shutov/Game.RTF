using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using tainicom;
using tainicom.Aether.Physics2D.Collision;
using Timer = System.Windows.Forms.Timer;

namespace Game_Prototype
{
    class GameModel
    {
        public Player player;
        public Maze maze;
        public Label tmpLabel;
        public PictureBox box;
        private bool jumping;
        private int velocityForCamera;
        private (int, int) SizeOfWindow;
        private Timer timerForCamera;
        public TextBox textOnCollides;

        public GameModel(int plX, int plY, int MazeHeight, int MazeWidth,(int,int) sizeForm)
        {
            velocityForCamera = 15;
            maze = MakeMaze();
            player = new Player(new Point(plX + 300, plY), new Size(107, 132));
            SizeOfWindow = sizeForm;    
            #region
            //rnd = new Random(173971^DateTime.Now.Millisecond);
            #endregion

            textOnCollides = new TextBox();
            timerForCamera = new Timer()
            {
                Interval = 25
            };
            timerForCamera.Tick += new EventHandler(CameraMovement);
            SetSettingsForPictureBoxes();
            timerForCamera.Start();
        }

        private void CameraMovement(object? sender, EventArgs e)
        {
            //delete = CameraBounds.Contains(new Point((int) playerFromKeyboard.physics.transform.position.X,
            //    (int) playerFromKeyboard.physics.transform.position.Y));
            //    if (delete)
            //        return;
            //var diffY = playerFromKeyboard.physics.transform.position.Y - CameraBounds.Y;
            //if (diffY < 0)
            //{
            //    if( box.Top < 200)
            //    {
            //        //box.Top += playerFromKeyboard.permutation.velocityForPlayer + 10;
            //        //tmpLabel.Top -= playerFromKeyboard.permutation.velocityForPlayer + 10;
            //        //CameraBounds.Y -= playerFromKeyboard.permutation.velocityForPlayer + 10;
            //    }
            //}
            //else
            //{
            //    if (box.Top >-640)
            //    {
            //        //box.Top -= playerFromKeyboard.permutation.velocityForPlayer + 15;
            //        //tmpLabel.Top += playerFromKeyboard.permutation.velocityForPlayer + 15;
            //        //CameraBounds.Y -= playerFromKeyboard.permutation.velocityForPlayer + 15;
            //    }
            //    //timerForCamera.Stop();
            //}

        }

        private void SetSettingsForPictureBoxes()
        {
            box = new PictureBox()
            {
                BackgroundImage = Sources._123,
                Height =6000,
                Width = 7000,
            };
            box.Image = maze.PrintMaze(box.Height, box.Width);
            
            tmpLabel = new Label()
            {
                Text = UpdateLabel(),
                Size = new Size(200, 200),
                Location = new Point(1000, 0)
            };
            AddControlsToMainControl();
            box.Paint += new PaintEventHandler(DrawPlayer);
            box.Controls.Add(textOnCollides);
            box.Controls.Add(tmpLabel);
            box.Controls.Add(player.CameraBox);
        }

        private void AddControlsToMainControl()
        {
            foreach (var element in maze.GetControlsForParentYield())
            {
                box.Controls.Add(element);
            }
        }
        public void KeyIsDownForPlayer(KeyEventArgs e)
        {
            
            switch (e.KeyCode)
            {
                case Keys.A:
                    SetPropertiesForPlayerAnimation(ref player.permutation.goLEFT,true,150,120,75,Sources.RUN_PNG,true);
                    break;
                case Keys.D:
                    SetPropertiesForPlayerAnimation(ref player.permutation.goRIGHT,true, 150, 120, 4, Sources.RUN_PNG, false);
                    break;
                case Keys.W when player.physics.couldJump:
                    player.permutation.goUP = true;
                    break;
                case Keys.X:
                    box.Top += player.permutation.velocityForPlayer;
                    tmpLabel.Top -= player.permutation.velocityForPlayer;
                    break;
                case Keys.Z:
                    box.Top -= player.permutation.velocityForPlayer;
                    tmpLabel.Top += player.permutation.velocityForPlayer;
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
        public void KeyIsUpForPlayer(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    SetPropertiesForPlayerAnimation(ref player.permutation.goRIGHT, false,92,90,4,Sources.idle_3_1,false);
                    break;
                case Keys.A:
                    SetPropertiesForPlayerAnimation(ref player.permutation.goLEFT, false, 92, 90, 9, Sources.idle_3_1, true);
                    break;
                case Keys.W:
                    player.permutation.goUP = false;
                    player.permutation.goDOWN = true;
                    player.CameraBox.Top += player.permutation.velocityForPlayer;
                    break;
            }
        }

        public void SetPropertiesForPlayerAnimation(ref bool refPlayerTurn, bool answer, int sourceWidth, int destWidth,int dx, Bitmap imagePlayer,bool isRotating)
        {
            refPlayerTurn = answer;
            player.image = imagePlayer;
            player.widthForGraphics = sourceWidth;
            player.destWidthForGraphics = destWidth;
            if (isRotating)
                player.image.RotateFlip(RotateFlipType.Rotate180FlipY);
            player.dx = dx;
        }

        public async void MainTimerEvent(int rightSide, EventArgs e)
        {

            player.UpdatePlayer();
            //TODO вынести код ниже в карту
            if (player.permutation.goRIGHT && DirectionForCamera.CameraLeft)
            {
                //playerFromKeyboard.permutation.velocityForPlayer = 9;
                box.Left -= velocityForCamera;
                tmpLabel.Left += velocityForCamera;
            }
            if (player.permutation.goLEFT && box.Left < 0 && DirectionForCamera.CameraRight)
            {
                box.Left += velocityForCamera;
                tmpLabel.Left -= velocityForCamera;
            }
            if (player.physics.transform.position.Y > player.CameraBox.PointToScreen(player.CameraBox.Location).Y )
            {
                box.Top += player.permutation.velocityForPlayer;
                tmpLabel.Top -= player.permutation.velocityForPlayer;
                player.CameraBox.Top += player.permutation.velocityForPlayer;
            }

            if (box.Controls.Collide(player.physics.transform))
            {
                #region MyRegion
                //textOnCollides.Visible = true;
                //textOnCollides.Text = "fffff";
                //textOnCollides.BackColor = Color.Aquamarine;
                //textOnCollides.Location = new Point(SizeOfWindow.Item1 / 2 - 100, SizeOfWindow.Item2 / 2 + 300);
                //textOnCollides.Size = new Size(300, 300); 
                #endregion
            }
            else
            {
                textOnCollides.Visible = false;
            }


            box.Refresh();
            tmpLabel.Text = UpdateLabel();

        }

        

        private void DrawPlayer(object sender, PaintEventArgs e)
        {
            player.DrawSprites(e.Graphics, player.image, Rectangle.Empty, Rectangle.Empty);
            //e.Graphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.World,
            //    new []{Point.Ceiling(player.physics.transform.position),});
        }


        public string UpdateLabel()
        {
            return $"HP: {player.HP}" +
                   $"\nIsCollide: {box.Controls.Collide(player.physics.transform)}" +
                   $"\nLeft:{player.permutation.goLEFT}" +
                   $"\nRight:{player.permutation.goRIGHT}" +
                   $"\nDown:{player.permutation.goDOWN}" +
                   $"\nUp:{player.permutation.goUP}" +
                   $"\nCounterUp:{player.UpCounter}" +
                   $"\nGracity:{player.physics.gravity}" +
                   $"\nLocationCam:{player.CameraBox.PointToScreen(player.CameraBox.Location)}";
        }
        private static Maze MakeMaze()
        {
            var tmp = new Maze();
            return tmp;
        }


        public Maze CheckMaze()
        {
            var tmp = new Maze()
            {
                maze = new [,]
                {
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty},
                    {MapCell.Empty, MapCell.Empty,MapCell.Wall, MapCell.Wall, MapCell.Empty, MapCell.Empty, MapCell.Empty},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty},
                    {MapCell.Empty, MapCell.Empty,MapCell.Wall, MapCell.Wall, MapCell.Wall, MapCell.Wall, MapCell.Wall},
                }
            };
            var tmp2 = new Maze()
            {
                maze = new[,]
                {
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,},
                    {MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty,MapCell.Empty, MapCell.Empty, MapCell.Wall, MapCell.Empty, MapCell.Empty,},

                }
            };
            return tmp;
        }





    }
}
