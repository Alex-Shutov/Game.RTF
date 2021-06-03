using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Game_Prototype.Map.MapObjects;
using Game_Prototype.PlayerClasses;

namespace Game_Prototype
{
    public class Chest : CreatureBase, IMapObject
    {

        private Timer timer;
        private static bool keyWasSpawned;
        public bool isWorking { get; set; }
        public bool WasPressed { get; set; }
        public bool isCollide { get; set; }
        public PictureBox picture { get; set; }
        
        private MapObjDelegate[] Methods { get;  }

        private readonly Random rnd;
        private static int counterOfChestFull;
        
        private static int counterofChestEmpty;
        

        public Point location => picture.Location;

        public ChestFill Filling; //Инкапсулировать
        public static Dictionary<ChestFill, string> CreatingChests = new Dictionary<ChestFill, string>();
        private static float deltaRandom = 1;
        private readonly Image[] ClosedAndOpenChest;
        public Chest(int x, int y, int side, MapObjDelegate[] links,Image[] imageArray)
        {
            ClosedAndOpenChest = imageArray;   
            timer = new Timer()
            {
                Interval = 100,
            };
            timer.Tick += CheckCollideBottom;
            rnd = new Random(DateTime.Now.Millisecond ^ 173491);
            if (counterOfChestFull == 0)
                counterOfChestFull = rnd.Next(4, 9);
            Methods = links;
            
            picture = new PictureBox()
            {
                Location = new Point(x * side, y * side),
                Size = new Size(side, side),
                Image = imageArray[0],
                BackColor = Color.Transparent,
            };
            permutation = new PermutationForCreature();
        }
        public void MakeAction()
        {
            if (!WasPressed)
            {
                Methods?[2]?.Invoke(Sources.buttonChest,this);
                picture.Image = ClosedAndOpenChest[1];
            }
        }

        public void OnCollide()
        {
            if (!isCollide && !WasPressed)
            {
                isCollide = true;
                Methods?[0]?.Invoke(Sources.buttonChest,this);
            }
            else if (WasPressed)
            {
                DeCollide();
            }

        }

       

        public void DeCollide()
        {
            isCollide = false;
            Methods?[1]?.Invoke(Sources.buttonChest,this);
        }
         /// <summary>
         /// Magic Random Method !
         /// </summary>
        public void CreateRandomFilling()
        {
            var tmp = rnd.NextDouble();
            if (counterOfChestFull > 0 && counterofChestEmpty > 7)
            {
                tmp = 0.65f;
                deltaRandom = 1;
            }
            switch (tmp * deltaRandom)
            { 
                case >= 0.3f when !keyWasSpawned && counterofChestEmpty+3 >= counterOfChestFull:
                    Filling = ChestFill.Key;
                    keyWasSpawned = true;
                    if (!CreatingChests.ContainsKey(Filling))
                        CreatingChests.Add(Filling,"Кажется что-то нашел... Интересно, что это ?");
                    break;
                case >= 0.5f when counterOfChestFull>0:
                    deltaRandom += 0.1f;
                    Filling = ChestFill.Health;
                    if (!CreatingChests.ContainsKey(Filling))
                        CreatingChests.Add(Filling, "Нашел зелье здоровья !");
                    counterOfChestFull--;
                    break;
                
                
                case < 0.6f:
                    Filling = ChestFill.Empty;
                    deltaRandom += 0.1f;
                    if (!CreatingChests.ContainsKey(Filling))
                        CreatingChests.Add(Filling, "Хм...Кажется, сундук пуст !");
                    counterofChestEmpty++;
                    break;
                
               

            }
        }

         private void CheckCollideBottom(object? sender, EventArgs args)
         {
             physics = new Physics(picture.Location, picture.Size, this);
             if (!physics.CollideBottom())
             {
                 picture.Top += 90;
             }
             else
             {
                 timer.Stop();
             }
         }

         public void CreateRandomLocation(MapCell[,] maze)
         {
             var tmp = ElementsOfMapGenerator.CreateNewLocation(this.location, maze).Select(x => x[^1]).ToList();
              picture.Location = tmp[rnd.Next(tmp.Count - 1)];
             timer.Start();
         }
    }
}
