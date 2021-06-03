using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Game_Prototype.PlayerClasses;

namespace Game_Prototype.Map.MapObjects
{
    class ExitMap :IMapObject
    {
        public bool isWorking { get; set; }
        public bool WasPressed { get; set; }
        public bool isCollide { get; set; }
        public PictureBox picture { get; set; }
        private MapObjDelegate[] Methods { get; }

        public ExitMap(int x, int y, int side, MapObjDelegate[] links, Image image)
        {
            
            Methods = links;

            picture = new PictureBox()
            {
                Location = new Point(x * side, y * side),
                Size = new Size(side, side),
                Image = image,
                BackColor = Color.Transparent,
            };
        }
        public void MakeAction()
        {
            if (isWorking)
                WasPressed = true;
            
            Methods?[2]?.Invoke(Sources.ButtonBasket, this);
        }

        public void OnCollide()
        {
            if (!isCollide && !WasPressed)
            {
                isCollide = true;
                Methods?[0]?.Invoke(Sources.ButtonBasket, this);
            }
            else if (WasPressed)
            {
                DeCollide();
            }
        }

        public void DeCollide()
        {
            isCollide = false;
            Methods?[1]?.Invoke(Sources.ButtonBasket, this);
        }
    }
}
