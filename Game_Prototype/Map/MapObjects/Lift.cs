using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace Game_Prototype
{
    internal class Lift :  IMapObject
    {
        public static int counterWorkingLifts = 2; //
        public bool WasPressed { get; set; }
        public bool isCollide { get; set; }
        public bool isWorking { get; set; }
        public static int allLifts = 6;
        public PictureBox picture { get; set; }

        private MapObjDelegate[] Methods { get; }


        //private PictureBox button = new PictureBox();
        public Lift(int x, int y,int side,MapObjDelegate[] links,Image image )
        {
            CreateRandomFilling();
            Methods = links;
            picture = new PictureBox()
            {
                Location = new Point(x*side,y*side),
                Size = new Size(side, side),
                Image = image,
                BackColor = Color.Transparent,
            };
            //CreatingButton();
        }
        public void MakeAction()
        {
            if (isWorking)
                WasPressed = true;
            Methods?[2]?.Invoke(Sources.ButtonLift,this);
        }

        public void OnCollide()
        {
            if (!isCollide && !WasPressed)
            {
                isCollide = true;
                //return new Action(()=>cw)
                Methods?[0]?.Invoke(Sources.ButtonLift,this);
            }
            else if (WasPressed)
            {
                DeCollide();
            }

        }

        public void DeCollide()
        {
            isCollide = false;
            Methods?[1]?.Invoke(Sources.ButtonLift,this);
        }
        private void CreateRandomFilling()
        {
            if (counterWorkingLifts <= 0)
            {
                isWorking = false;
                return;
                
            }
            var rnd = new Random(DateTime.Now.Millisecond^17349);
            isWorking = (rnd.NextDouble() > 0.6);
            allLifts--;
            if (allLifts == counterWorkingLifts && counterWorkingLifts!=0)
            {
                counterWorkingLifts--;
                isWorking = true;
                return;
            }
            if (isWorking && counterWorkingLifts!=0)
                counterWorkingLifts--;
        }

    }
}
