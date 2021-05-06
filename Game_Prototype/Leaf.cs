using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace Game_Prototype
{
    class Leaf
    {
        const int  MIN_LEAF_SIZE = 6;

        public int x, 
                   y,
                   width,
                   height;
        
        public Leaf leftChildLeaf,
                    rightChildLeaf;
        public Vector2 halls;
        public Rectangle room;

        public Leaf(int pointX, int pointY, int Width, int Height)
        {
            x = pointX;
            y = pointY;
            width = Width;
            height = Height;
        }

        public bool Split()
        {
            if (leftChildLeaf != null || rightChildLeaf != null) // уже разрезали
                return false;

            var rndSplit = new Random(DateTime.Now.Millisecond ^ 17341);
            bool splitRegulatorHeight = false;

            if (width > height && width / height >= 1.25)
                splitRegulatorHeight = false;
            else if (height > width && height / width >= 1.25)
                splitRegulatorHeight = true;
            else
                splitRegulatorHeight = !((float)rndSplit.NextDouble() > 0.5);

            var maxHeightOrWidth = (splitRegulatorHeight ? height : width) - MIN_LEAF_SIZE;

            if (maxHeightOrWidth <= MIN_LEAF_SIZE)
                return false;

            var split = rndSplit.Next(MIN_LEAF_SIZE, maxHeightOrWidth);

            if (splitRegulatorHeight)
            {
                leftChildLeaf = new Leaf(x, y, width, split);
                rightChildLeaf = new Leaf(x, y + split, width, height - split);
            }
            else
            {
                leftChildLeaf = new Leaf(x, y, split, height);
                rightChildLeaf = new Leaf(x + split, y, width = split, height);

            }

            return true;

        }

    }
}
