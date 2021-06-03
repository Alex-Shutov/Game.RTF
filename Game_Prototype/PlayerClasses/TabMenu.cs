using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Game_Prototype.Map.MapObjects;

namespace Game_Prototype.PlayerClasses
{
    public class TabMenu
    {
        public readonly PictureBox Picture;
        private List<TabInventoryElement> listElements;
        private int dYInventory;
        private int dXInventory;
        private int dYTask;
        //private int tabKey = 0;
        public static Dictionary<int,TabTask> DictTasks = new Dictionary<int, TabTask>();

        public EventHandler[] Methods;
        public TabMenu(int x, int y,EventHandler[] handler)
        {
           
            Methods = handler;
            Picture = new PictureBox()
            {
                Image = Sources.TabMenu,
                Location = new Point(100, 100),
                Size = new Size(1200, 600),
                BackColor = Color.Transparent,
                ForeColor = Color.Transparent
            };
            listElements = GenerateDict();
        }

        public int CountCountrols => Picture.Controls.Count;

        public void PressTab()
        {
            Methods?[0]?.Invoke(null,null);
        }

        public List<TabInventoryElement> GenerateDict()
        {
            var tmp = new List<TabInventoryElement>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var element = new TabInventoryElement(ChestFill.Empty, new Point(i * 90 + 80, j * 90 + 120), new Size(80, 80),tmp.Count);
                    tmp.Add(element);
                    Picture.Controls.Add(element.Picture);
                    
                }
            }

            return tmp;

        }

        public void Unpressed()
        {
            Methods?[1]?.Invoke(null, null);
        }

        public void AddInventoryElement(Chest chest)
        {
            #region MyRegion
            //if (dictionaryTab.ContainsKey(chest.Filling))
            //{
            //    var element = new TabInventoryElement(dictionaryTab[chest.Filling], new Point(dXInventory * 90 + 80, dYInventory * 90 + 120),
            //        new Size(80, 80));
            //    listElements.Add(element);
            //    Picture.Controls.Add(element.Picture);
            //    dXInventory++;
            //    if (dXInventory == 3)
            //    {
            //        dXInventory = 0;
            //        dYInventory++;
            //    }

            //int? tmp = listElements.Select((x, index) => new {x, index}).Where(x => x.x.Filling == ChestFill.Empty)
            //    .Select(x => x.index).FirstOrDefault(); 
            #endregion
            var elementIndex = listElements.Select(x => x).FirstOrDefault(x => x.Filling == ChestFill.Empty)?.index;
            if (elementIndex != null)
            {
                var element = new TabInventoryElement(chest.Filling, new Point(dXInventory * 90 + 80, dYInventory * 90 + 120), new Size(80, 80),(int)elementIndex);
                listElements[(int) elementIndex] = element;
                Picture.Controls.Add(element.Picture);
                element.Picture.BringToFront();
                dXInventory++;
                if (dXInventory == 3)
                {
                    dXInventory = 0;
                    dYInventory++;
                }
            }
        }

        public bool DeleteElementHeal()
        {
            var element = listElements.Select(x => x).FirstOrDefault(x => x.Filling == ChestFill.Health);
            if (element != null)
            {
                listElements[element.index] = new TabInventoryElement(ChestFill.Empty, new Point(dXInventory * 90 + 80, dYInventory * 90 + 120), new Size(80, 80), element.index);
                Picture.Controls.Remove(element.Picture);
                return true;
            }

            return false;
        }

        public void DeleteTask(int index)
        {
            DictTasks.TryGetValue(index, out var element);
            if (element!=null)
                Picture.Controls.Remove(element?.Picture);
            DictTasks.Remove(index);
        }


        public void AddTaskElement(string task,int index)
        {

            var element = new TabTask(task, new Point(630, 100+dYTask));
            Picture.Controls.Add(element.Picture);
            element.Picture.BringToFront();
            DictTasks.Add(index,element);
            dYTask += 120;
        }
    }
}
