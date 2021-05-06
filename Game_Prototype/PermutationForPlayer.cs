using System;
using System.Collections.Generic;
using System.Text;

namespace Game_Prototype
{
    public class PermutationForPlayer
    {
        public bool goRIGHT { get; set; }
        public bool goLEFT { get; set; }
        public bool goDOWN { get; set; }
        public bool goUP { get; set; }

        public int velocityForPlayer { get; private set; }

        public void Setvelocity(int a)
        {
            velocityForPlayer = a;
        }

    }
}
