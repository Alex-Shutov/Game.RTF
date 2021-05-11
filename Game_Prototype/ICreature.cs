using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Game_Prototype
{
    public interface ICreature
    {
        public int HP { get; set; }

        public PermutationForCreature permutation { get; set; }
        public bool isAgressive { get; set; }

        public Physics physics { get; set; }

        public Bitmap image { get; set; }
        public float Move(float x, int velocity, int direction);

        public void DrawSprites(Graphics g, Image image, Rectangle destRectangle, Rectangle sourceRectangle);

    }
}
