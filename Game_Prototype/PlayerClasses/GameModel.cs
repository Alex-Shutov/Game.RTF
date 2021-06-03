using System;
using System.Drawing;
using System.Windows.Forms;
using Game_Prototype.Map.MapObjects;
using Game_Prototype.PlayerClasses;
using Timer = System.Windows.Forms.Timer;

namespace Game_Prototype
{
    public class GameModel
    {
        public Player Player;
        public Maze Maze;
        private readonly (int, int) sizeofWindow;
        private Timer timerForCamera;
        public int Dx;
        public EventContainer containerLinks;
        public TabMenu Tab;
        private delegate void CameraHelper(int i, bool c);

        private event CameraHelper CameraMove;
        public GameModel(int plX, int plY, (int, int) sizeForm)
        {
            
            Player = new Player(new Point(plX + 300, plY), new Size(107, 132));
            sizeofWindow = sizeForm;
            Dx = 300;
            timerForCamera = new Timer()
            {
                Interval = 25
            };
            CameraMove += new CameraHelper(ChangeCameraLeftCorner);
        }

        public void AddingDelegates(EventContainer container)
        {
            containerLinks = container;
            Maze = MakeMaze(Player, container.mapDelegates);
            Tab = new TabMenu(sizeofWindow.Item1, sizeofWindow.Item2,container.tabDelegates);
            Tab.AddTaskElement("Найти вход на 2 уровень бездны",0);
            timerForCamera.Start();
        }

        private void ChangeCameraLeftCorner(int i, bool dirUpDown)
        {
            if (!dirUpDown)
                Maze.CameraMoveLeftRigth(i);
        }

        public void PressedButtonOnObject()
        {
            var objectOnMap = Maze.MapElements.PressedButton();
            if (objectOnMap is Chest {WasPressed: false} chest && chest.Filling!=ChestFill.Empty)
            {
                chest.WasPressed = true;
                chest.DeCollide();
                Tab.AddInventoryElement(chest);
            }
        }

        public void ActionHealFromInventory()
        {
            Tab.DeleteElementHeal();
            Player.Heal();
        }

        public void SetPropertiesForPlayerAnimation(ref bool refPlayerTurn, bool answer, int sourceWidth, int destWidth, int dx, Bitmap imagePlayer, bool isRotating)
        {
            refPlayerTurn = answer;
            Player.image = imagePlayer;
            Player.destWidthForGraphics = destWidth;
            Player.widthForGraphics = sourceWidth;
            if (isRotating)
                Player.image.RotateFlip(RotateFlipType.Rotate180FlipY);
            this.Dx = dx;
        }

        public void MainTimerEvent(object? sender, EventArgs eventArgs)
        {
            if (Player.physics.transform.position.X > sizeofWindow.Item1 * 0.5 && Player.permutation.goRIGHT && Maze.MazeBox.Left > -5500)
                CameraMove?.Invoke(15, false);
            if (Player.permutation.goLEFT && Maze.MazeBox.Left < 0)
                CameraMove?.Invoke(-15, false);
            Player.UpdatePlayer();
            if (Player.HP <= 0 && Player.isAlive)
            {
                timerForCamera.Stop();
                containerLinks.DeathEvent?.Invoke(null, null);
                Maze.MapElements.timer.Stop();
                Player.isAlive = false;
            }
        }

        private static Maze MakeMaze(Player player, MapObjDelegate[] mapsEvents)
        {
            var tmp = new Maze(player, mapsEvents);
            return tmp;
        }
    }





}
