using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Game_Prototype.Map.MapObjects;

namespace Game_Prototype.PlayerClasses
{
    public class TabInventoryElement
    {
        public PictureBox Picture { get; }
        public ChestFill Filling { get; }
        public Point LocPoint { get; }

        public int index { get; }
        private static Dictionary<ChestFill, Image> dictionaryTab = new()
        {
            {ChestFill.Empty, Sources.TabButton},
            {ChestFill.Health, Sources.TabButtonHeal},
            {ChestFill.Key, Sources.TabButtonStone}
        };

        public TabInventoryElement(ChestFill fill,Point locPoint,Size size,int index)
        {
            Filling = fill;
            Picture = new PictureBox()
            {
                Image = dictionaryTab[fill],
                Location = locPoint,
                Size = size
            };
            LocPoint = locPoint;
            this.index = index;
        }
    }
}
