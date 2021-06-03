using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Game_Prototype.Map.MapObjects
{
    internal class KeyWall : Wall,IMapObject

    {
        public bool isWorking { get; set; }
        public bool WasPressed { get; set; }
        public bool isCollide { get; set; }
        public PictureBox picture { get; set; }
        private MapObjDelegate[] Methods { get; }

        private Image[] imageSet;
        public KeyWall(int x, int y, int side, MapObjDelegate[] links, Image[] imageArray) : base(x*side, y*side, side,side)
        {
            imageSet = imageArray;
            Methods = links;

            picture = new PictureBox()
            {
                Location = new Point(x * side, y * side),
                Size = new Size(side, side),
                Image = imageArray[0],
                BackColor = Color.Transparent,
            };
        }
        public void MakeAction()
        {
            if (isWorking)
                WasPressed = true;
            Methods?[2]?.Invoke(Sources.ButtonLift, this);
        }

        public void OnCollide()
        {
            if (!isCollide && !WasPressed)
            {
                isCollide = true;
                Methods?[0]?.Invoke(Sources.ButtonKeyWall, this);
            }
            else if (WasPressed)
            {
                DeCollide();
            }
        }

        public void DeCollide()
        {
            isCollide = false;
            Methods?[1]?.Invoke(Sources.ButtonKeyWall, this);
        }

        public void ChangeImageOnKeyEvent()
        {
            picture.Image = imageSet[1];
        }
    }
}
