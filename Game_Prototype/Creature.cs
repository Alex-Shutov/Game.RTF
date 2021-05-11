using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game_Prototype
{
    public abstract class Creature : ICreature
    {
        public PermutationForCreature permutation { get; set; }
        public Bitmap image { get; set; }
        public int HP { get; set; }
        public bool isAgressive { get; set; }
        
        public Physics physics { get; set; }

        public virtual float Move(float x, int velocity, int direction)
        {
            return x + velocity * direction;
        }

        public virtual void DrawSprites(Graphics g, Image image, Rectangle destRectangle, Rectangle sourceRectangle)
        {
            g.DrawImage(image, destRectangle, sourceRectangle, GraphicsUnit.Pixel);

        }
    }
}
