using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Game_Prototype
{
    public class Transform
    {
        public PointF position;
        public Size size;

        public Transform(PointF pos, Size size)
        {
            position = pos;
            this.size = size;
        }
    }
}
