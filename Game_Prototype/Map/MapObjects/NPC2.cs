using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game_Prototype.Map.MapObjects
{
    public class NPC2 : CreatureBase, IMapObject
    { 
        public MapObjDelegate[] Methods;
        public bool isWorking { get; set; }

        public bool WasPressed { get; set; }

        public bool isCollide { get; set; }

        public PictureBox picture { get; set; }

        public NPC2(int x, int y, int side, MapObjDelegate[] delegates)
        {
            picture = new PictureBox()
            {
                Location = new Point(x * side, y * side - 50),
                Image = Sources.Bandit,
                Size = new Size(150, 150),
                BackColor = Color.Transparent
            };
            isAlive = true;
            HP = 100;
            isAgressive = false;
            Methods = delegates;
        }
        public void MakeAction()
        {
            if (!WasPressed)
            {
                Methods?[2]?.Invoke(Sources.Dialog, this);
            }
        }

        public void OnCollide()
        {
            if (!isCollide && !WasPressed)
            {
                isCollide = true;
                Methods?[0]?.Invoke(Sources.Dialog, this);
            }
            else if (WasPressed)
            {
                DeCollide();
            }
        }



        public void DeCollide()
        {
            isCollide = false;
            Methods?[1]?.Invoke(Sources.Dialog, this);

        }
    }
}

