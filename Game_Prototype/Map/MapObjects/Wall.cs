using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Game_Prototype
{
    public class Wall
    {
        public Rectangle Borders { get; }
        public Wall(int x, int y, int width, int height)
        {
            Borders = new Rectangle(x, y, width, height);
        }
    }
}
