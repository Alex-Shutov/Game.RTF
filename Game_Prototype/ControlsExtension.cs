using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game_Prototype
{
    public static class ControlsExtension
    {
        private static List<Control> liftList = new List<Control>();
        private static List<Control> NPCList { get; set; }
        private static List<Control> ChestList { get; set; }
        private static List<Control> BanditList { get; set; }
        

        public static bool Collide(this Control.ControlCollection controls, Transform model)
        {
            foreach (Control mapObject in controls)
            {
                var bounds = new Rectangle(new Point(mapObject.Location.X - 20, mapObject.Location.Y),
                    new Size(mapObject.Size.Width + 20, mapObject.Height));
                //if ( bounds.IntersectsWith(new Rectangle(new Point((int)model.position.X, (int)model.position.Y),model.size)) && Equals(mapObject.Tag,"LIFT"))
                if (bounds.IntersectsWith(new Rectangle(new Point((int)model.position.X, (int)model.position.Y), model.size)))
                { 
                    liftList.Add(mapObject);
                    OnCollide(mapObject);
                    return true;
                }
            }

            return false;
        }

        private static Action OnCollide(Control cell)
        {
            return null;
        }
    }
}
