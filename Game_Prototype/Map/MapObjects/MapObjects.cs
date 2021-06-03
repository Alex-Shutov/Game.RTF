using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Game_Prototype.Map.MapObjects
{
    public class MapObjects //TODO Фабрика фабрик - Много классов с дублированием кода - обобщить
    {
        public static Dictionary<int, NPC2> ListBandits = new Dictionary<int, NPC2>();
        public List<IMapObject> ListObjects;


        public Point bandit;

        //public IMapObject CHECK;
        public Timer timer;

        public Transform player;
        private IMapObject objectMap { get; set; }
        public MapObjects(Transform model)
        {
            player = model;
            ListObjects = new List<IMapObject>();
            timer = new Timer();
            timer.Tick += new EventHandler(Collide);
            timer.Interval = 100;
            timer.Start();
        }

       

        private void Collide(object? sender, EventArgs e) => Collide(player);



        private void Collide(Transform model)
        {
            foreach (var mapObject in ListObjects)
            {

                var bounds = new Rectangle(new Point(mapObject.picture.Location.X - 20, mapObject.picture.Location.Y),
                    new Size(mapObject.picture.Size.Width + 20, mapObject.picture.Height));
                if (mapObject != objectMap && objectMap != null)
                    continue;
                if (bounds.IntersectsWith(new Rectangle(new Point((int)model.position.X, (int)model.position.Y), model.size)))
                {
                    objectMap = mapObject;
                    objectMap.OnCollide();
                }
                else if (objectMap != null)
                {

                    objectMap.DeCollide();
                    //CHECK?.DeCollide();
                    objectMap = null;
                }
                //timer.Start();
            }

            //return false;
        }

        public void CreateMapObjects(MapCell[,] map,MazeDelegate mazeLink)
        {
            CreatingRandomLocationChests(map);
            SpawnBandit(mazeLink);

        }
        public IMapObject PressedButton()
        {
            objectMap?.MakeAction();
            return objectMap;
        }

        private void SpawnBandit(MazeDelegate link)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            var index = rnd.Next(ListBandits.Count);
            //var bandit = ListObjects.Select(x => x as NPC2).Where(x => x != null).ToList();
            link.Invoke(index);
            bandit = ListBandits[index].picture.Location;
        }
        private void CreatingRandomLocationChests(MapCell[,] maze)
        {
            var enumrable = ListObjects.Select(x => x as Chest).Where(x => x != null).ToList();
            foreach (var chest in enumrable)
            {
                chest.CreateRandomLocation(maze);
            }
        }

    }

}
