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
        private PermutationForPlayer perm = new PermutationForPlayer();
        private int currentSlide = 0;
        public Bitmap image;
        public int HP = 0;
        public bool isAgressive;
        public PictureBox CreatureBox;

        public virtual float Move(float x,int velocity,int direction)
        {
            return x + velocity*direction;
        }

        public virtual void DrawSprites(Graphics g,Image image, Rectangle destRectangle,Rectangle sourceRectangle)
        {
            g.DrawImage(image, destRectangle, sourceRectangle,GraphicsUnit.Pixel);

        }

        private void Update(object? sender, EventArgs e)
        {
          
            
        }
    } 
}
