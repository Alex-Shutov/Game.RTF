using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Game_Prototype
{
    interface  IMapObject
    {
        public bool Collide();

        public Action OnCollide();
    }
}
