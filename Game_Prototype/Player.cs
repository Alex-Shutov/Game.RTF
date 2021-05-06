using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Game_Prototype
{
    class Player : Creature
    {

        //public ImageList listImages;
        private int check;
        public int frameCount;
        public int animationCount;
        public int dx;
        public Physics physics;
        public Player(PointF x, Size y)
        {
        //    listImages = new ImageList()
        //    {
        //        ImageSize = new Size(140,140),
        //        Images =
        //        {
        //            Sources._1, Sources._2, Sources._3, Sources._4, Sources._5, Sources._6, Sources._7, Sources._8
        //        }
        //    };
           // transform = new Transform(x, y);
            //CreatureBox = new PictureBox()
            //{
            //   // BackColor = Color.Black,
            //    SizeMode = PictureBoxSizeMode.CenterImage,
            //};
            frameCount = 0;
            animationCount = 0;
            HP = 100;
            image = Sources.idle_3_1;
            dx = 3;
            physics = new Physics(pos:x, size:y);
        }
        public override void Move(int x, int y)
        {
           // var tmp = CreatureBox.Location.Y;
            base.Move(x,y);
            //GetHP(tmp);
        }

        public override void DrawSprites(Graphics g, Image image, Rectangle destRectangle, Rectangle sourceRectangle)
        {
            animationCount++;
            //g.DrawImage(Sources.idle_3_1, new Rectangle((int)transform.position.X, (int)transform.position.Y, 90, 150), new Rectangle(frameCount * 92+5, 0, 95, 150),
            //    GraphicsUnit.Pixel);
            //frameCount++;
            //if (frameCount == 7)
            //    frameCount = 0;
            //if (animationCount%10 == 0)
            //    listImages.Draw(g,new Point(100,100),frameCount);
            //frameCount++;
            //if (frameCount == 7)
            //    frameCount = 0;
            //animationCount++;
            //{
            //    frameCount++;
            //frameCount = (animationCount % 40) switch
            //{
            //    < 5 => 0,
            //    >= 5 and < 10 => 1,
            //    >= 10 and < 15 => 2,
            //    >= 15 and < 20 => 3,
            //    >= 20 and < 25 => 4,
            //    >= 25 and < 30 => 5,
            //    >= 30 and < 35 => 6,
            //    >= 35 and < 40 => 7,
            //    _ => frameCount
            //};
            //if (physics.isJumping)
                //base.DrawSprites(g, image, new Rectangle((int)physics.transform.position.X, (int)physics.transform.position.Y, 90, 150), new Rectangle(frameCount * 92 + dx, 0, 95, 150));
            base.DrawSprites(g,image, new Rectangle((int)physics.transform.position.X, (int)physics.transform.position.Y, 90, 150), new Rectangle(frameCount * 92 + dx, 0, 95, 150));
            //if (animationCount > 40)
            //    animationCount = 0;
            // }
                frameCount++;
            if (frameCount == 7)
                frameCount = 0;
            //  CreatureBox.Image = Sources.Idle_big_1;
            //  CreatureBox.Size = new Size(85,140);
            ////  CreatureBox.BackColor = Color.Transparent;
            //  base.DrawSprites();
        }

        public void GetHP(int tmp)
        {
            //if (tmp.Y==200)
            //if (CreatureBox.Location.Y % 200 == 0 && CreatureBox.Location.Y != 0 && tmp.Y-CreatureBox.Location.Y<0)
            //    HP -= 10;
            if (tmp > CreatureBox.Location.Y)
                check++;
            else check = 0;
            if (check % 200 == 0 && check != 0)
                HP -= 10;
        }
    }
}
