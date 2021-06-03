using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Game_Prototype.Map.MapObjects
{
    public class TabTask
    {
        public PictureBox Picture { get; set; }
        //private static int dy = 60;
        private Graphics graphics;
        //public int Index;
        public TabTask(string str,int x, int y)
        {
            //Index = index;
            Picture = new PictureBox()
            {
                Image = Sources.ButtinTabTask,
                Location = new Point(x,y),
            };
            graphics = Graphics.FromImage(Picture.Image);
            graphics.DrawString(str, new Font("Times New Roman", 28), Brushes.LightGray, 50, 10);
        }
        public TabTask(string str,Point point)
        {
           // Index = index;
            Picture = new PictureBox()
            {
                Image = Sources.ButtinTabTask,
                Location = point,
                Size = new Size(600,70)
            };
            graphics = Graphics.FromImage(Picture.Image);
            graphics.DrawString(str, new Font("Times New Roman", 28), Brushes.LightGray, 50, 10);
        }
    }
}
