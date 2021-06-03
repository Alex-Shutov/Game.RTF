using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.Xml;
using System.Threading;
using System.Windows.Forms;

namespace Game_Prototype
{
    public class Player : CreatureBase 
    {
        private int UpCounter;
        public int dx;
        public int widthForGraphics;
        public int destWidthForGraphics;
        public PictureBox CameraBox;
        public event EventHandler eventCamera;
        public bool? hasKeyToOpenExit;
        public Player(PointF x, Size y)
        {
            isAlive = true;
            permutation = new PermutationForCreature();
            UpCounter = 1;
            permutation.velocityForPlayer = 15;
            HP = 100;
            image = Sources.idle_3_1;
            dx = 10;
            //isAgressive = false;
            physics = new Physics(pos: x, size:y,this);
            widthForGraphics = 90;
            destWidthForGraphics = 90;
            CameraBox = new PictureBox()
            {
                Location = new Point(200, 0),
                Size = new Size(90, 150),
                BackColor = Color.Blue,
            };
        }

        public void UpdatePlayer()
        {
            physics.transform.position.X = Move(physics.transform.position.X, permutation.velocityForPlayer, 0);
        }
        public override float Move(float f, int velocity, int direction)
        {
            if (permutation.goUP && !physics.CollideUp())
            {
                CameraBox.Top -= velocity + 5;
                physics.AddForce();
                physics.ApplyPhysics();
                
            }
            else if (!physics.CollideBottom())
            {
                physics.DownForce();
                CameraBox.Top += velocity;
                eventCamera?.Invoke(null,null);

            }
            if (permutation.goLEFT && physics.transform.position.X > -10 && !physics.CollideRight())
            {
                return base.Move(physics.transform.position.X, velocity, -1);
            }
            var stateLeft = !physics.CollideLeft();
            if (permutation.goRIGHT  && stateLeft)
            { 
                return base.Move(physics.transform.position.X, velocity, 1);
            }
            GetHP();
            return physics.transform.position.X;
        }


        //TODO убрать магию, нет mvc в коде ниже
        public void GetHP()
        {
            if (permutation.goUP)
                UpCounter++;
            if (UpCounter % 15 == 0)
            {
                HP -= 20;
                UpCounter = 1;
            }

            if (!physics.couldJump && physics.gravity < -25)
                UpCounter = 1;
        }

        public void Heal()
        {
            if (HP<=100)
                HP += 35;
            if (HP >= 100)
                HP = 100;
        }

    }
}
