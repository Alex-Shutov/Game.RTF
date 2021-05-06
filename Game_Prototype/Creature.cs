using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Game_Prototype
{
    abstract  class Creature
    {
        private int currentSlide = 0;
        public Bitmap image;
        public int HP = 0;
        public  bool isAgressive = false;

       // public Transform transform;
        //public 
        public PictureBox CreatureBox;

        private Label label;
        //public 
        public virtual void Move(int x, int y)
        {
            //CreatureBox.Location = new Point(x,y);
            //CreatureBox.Size = transform.size;
        }

        public virtual void DrawSprites(Graphics g,Image image, Rectangle destRectangle,Rectangle sourceRectangle)
        {
            g.DrawImage(image, destRectangle, sourceRectangle,GraphicsUnit.Pixel);
            //var t = CreatureBox.Image.Width;
            //var p = CreatureBox.Image.Height;
            //  CreatureBox.Location = transform.position;
            //CreatureBox.Location = new Point((int) transform.position.X, (int) transform.position.Y);
            //CreatureBox.BackColor = Color.Transparent;
            // label = new Label()
            //{
            //    Size = new Size(300,300),
            //    Location = new Point(100, 50),
            //};
            // var c = 
            //form.Controls.Add(label);
            //var t = new Timer();
            ////var per =  MakePermutationInSprite();
            //t.Interval = 1000;
            //t.Tick += new EventHandler(Update);
            //t.Start();
            //form.Invalidate();

        }

        private void Update(object? sender, EventArgs e)
        {
            //MakeAnim();
            //if (currentSlide == 7)
            //    currentSlide = 0;
            //currentSlide++;
            
        }

        //public virtual ImageList MakePermutationInSprite()
        //{

        //   // //var part = new Bitmap(300, 300);
        //   // //var graphics = Graphics.FromImage;
        //   // ////g.FillRectangle(Brushes.Aqua,new Rectangle(0,0,300,300));
        //   // //g.DrawImage(CreatureBox.Image,0,0,new Rectangle(new Point(0 ,0),new Size(300   ,300)),GraphicsUnit.Pixel);
        //   // //CreatureBox.SizeMode = PictureBoxSizeMode.Normal;
        //   //// var graphics
        //   // //CreatureBox.Location = new Point(0, 0);
        //   // //CreatureBox.Size = new Size(300, 1350);
        //   // //CreatureBox.Image = part;
        //   // list = new ImageList();
        //   // var d = 0;
        //   // for (int i = 0; i < 8; i++)
        //   // {
                
        //   //     //CreatureBox.Visible = false;
        //   //     //graphics.FillRectangle(Brushes.Aqua,0, 0, 120, 130);
        //   //    // Thread.Sleep(100);
        //   //    // var e = CreatureBox.PointToScreen(CreatureBox.Location);
        //   //     var part = new Bitmap(Sources.Idle__2);
        //   //     //var graphics = Graphics.FromImage(part);
        //   //         //  graphics.FillRectangle(Brushes.Aqua, 100, 0, 120, 130);
        //   //    // CreatureBox.Controls.Add();
        //   //    var recttoClone = new Rectangle((330+d)*i,0,120,130);
        //   //    // CreatureBox.Size = new Size(120, 130);
        //   //     var g = part.Clone(recttoClone, PixelFormat.Format64bppPArgb);
        //   //     //g.SetResolution(300, 300);
                
        //   //     //CreatureBox.Image = g;
        //   //     list.Images.Add(g);
        //   //     d = 5 * i;
        //   // }

        //   // list.ImageSize = new Size(120, 130);
        //   // //list.Images[0].
        //   // return list;

        //}
        private void MakeAnim()
        {
            label.Text = $"{currentSlide}";
            //CreatureBox.Size = new Size(120, 130);;
            //CreatureBox.Image = list.Images[currentSlide];
            //list.Draw(Graphics.FromImage(CreatureBox.Image), CreatureBox.Location, currentSlide);
            //CreatureBox.Refresh();
            //form.Controls.Add(CreatureBox);
        }


    } 
}
