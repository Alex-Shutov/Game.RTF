using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Game_Prototype.Map.MapObjects;

namespace Game_Prototype.PlayerClasses
{
    public class EventContainer
    {

        public MapObjDelegate[] mapDelegates { get; set; }
        public EventHandler[] tabDelegates { get; set; }
        public EventHandler DeathEvent { get; set; }

        private GameModel Game;
        private Control.ControlCollection Controls;
        private PictureBox buttonPicture;
        private PictureBox Text;
        private Timer timerDelayBoxDelete;

        private Form1 formForRestart; // TODO переделать на ивент!!!
        private int counter;

        private Graphics graphics;

        public EventContainer(GameModel game, Control.ControlCollection controls, PictureBox button, PictureBox text, Form1 form)
        {
            timerDelayBoxDelete = new Timer()
            {
                Interval = 450
            };
            timerDelayBoxDelete.Tick += new EventHandler(DeleteTextLabel);
            Game = game;
            Controls = controls;
            buttonPicture = button;

            mapDelegates = new MapObjDelegate[] { CollideObject, DeCollideObject, PressedButtonOn };
            tabDelegates = new EventHandler[] { ShowInventory, InventoryUnpressed };
            DeathEvent = new EventHandler(DeathPlayerEnd);
                Text = text;
            formForRestart = form;

        }

        [DllImport("winmm.dll", EntryPoint = "sndPlaySound")]
        public static extern long PlaySound(string fileName, long flags);

        private void ShowInventory(object? e, EventArgs args)
        {
            Game.Tab.Picture.Visible = true;
            this.Controls.Add(Game.Tab.Picture);
            Game.Tab.Picture.BringToFront();
        }
        private void InventoryUnpressed(object? e, EventArgs args)
        {
            this.Controls.Remove(Game.Tab.Picture);
            Game.Tab.Picture.Visible = false;
        }

        private void CollideObject(Image box, IMapObject source)
        {
            buttonPicture = new PictureBox()
            {
                Image = box,
                Size = new Size(400, 100),
                Location = new Point(500, 600),
            };
            this.Controls.Add(buttonPicture);
            buttonPicture.BringToFront();
        }

        private void DeCollideObject(Image box, IMapObject source)
        {

            this.Controls.Remove(buttonPicture);
            buttonPicture.Dispose();


        }
        private void DeleteTextLabel(object? sender, EventArgs e)
        {
            counter++;
            if (counter > 5)
            {
                timerDelayBoxDelete.Stop();
                Text.Visible = false;
                counter = 0;
            }

        }

        private void PressedButtonOn(Image box, IMapObject source) //TODO DIctionary
        {
            switch (source)
            {
                case Chest chest:
                    InitializzButtonBox(Chest.CreatingChests[chest.Filling]);
                    if (chest.Filling == ChestFill.Key)
                    {
                        Game.Player.hasKeyToOpenExit = true;
                        //if (TabMenu.DictTasks.ContainsKey(2))
                        //    Game.Tab.DeleteTask(2);

                        Game.Tab.AddTaskElement("Найти применение этой вещице!",3);
                    }

                    break;
                case Lift lift when !lift.isWorking:
                    InitializzButtonBox("Кажется, лифт не работает...");
                    break;
                case Lift lift:
                    CreatingPanelForEnd("Вы выбрались на поверхность \n Спуститься вновь ?", Sources.kotyonok_na_plyazhe, Color.Aqua,Sources.CatWin);
                    break;
                case NPC2 npc when Game.Player.hasKeyToOpenExit == null :
                    InitializzButtonBox("Говоришь, хочешь дойти до входа в бездну?\n ХА... Поищи-ка ключ, братец!");
                    if (!TabMenu.DictTasks.ContainsKey(1))
                        Game.Tab.AddTaskElement("Найти ключ ?!",1);
                    break;
                case NPC2 :
                    InitializzButtonBox("Кажется, ты уже нашел, что искал... \nСпускайся к входу, братец"); 
                    break;
                case KeyWall when Game.Player.hasKeyToOpenExit == null:
                    InitializzButtonBox("Кажется,чего-то не хватает...");
                    if (!TabMenu.DictTasks.ContainsKey(2))
                        Game.Tab.AddTaskElement("Найти способ открыть дверь...",2);
                    break;
                case KeyWall wallKey when Game.Player.hasKeyToOpenExit == true:
                    InitializzButtonBox("Похоже открылось !");
                    if (TabMenu.DictTasks.ContainsKey(2))
                        Game.Tab.DeleteTask(2);
                    if (TabMenu.DictTasks.ContainsKey(3))
                        Game.Tab.DeleteTask(3);
                    DeleteFromCollides();
                    wallKey.ChangeImageOnKeyEvent();
                    break;
                case ExitMap:
                    CreatingPanelForEnd("Вы вошли в бездну\nКогда-нибудь вы подниметесь назад, а пока ...",Sources.foma_fizruk_nagiev,Color.Black,Sources.ExitGame);
                    break;
            }
        }

        private void DeleteFromCollides()
        {
            var keyBoxes = Maze.HashSetWalls.Select(x => x as KeyWall).Where(x => x != null);
            foreach (var key in keyBoxes)
            {
                key.ChangeImageOnKeyEvent();   
            }
            Maze.HashSetWalls = Maze.HashSetWalls.Select(x=>x).Where(x=>!(x is KeyWall)).Where(x=>x!=null).ToHashSet();
        }

        public void CreatingPanelForEnd(string text, Image image, Color color,Stream music)
        {
            formForRestart.mainTimer.Stop();
            //formForRestart.Init();
            var pictureRestart = new PictureBox
            {
                BackColor = color,
                Location = new Point(0, 0),
                Size = new Size(formForRestart.Width, formForRestart.Height),
                Image = image,
                SizeMode = PictureBoxSizeMode.Zoom,
            };
           var buttonRestart = new Button()
            {
                BackColor = Color.AliceBlue,
                Size = new Size(200, 100),
                Location = new Point(0, pictureRestart.Height - 200),
                Text = text
            };
            
            formForRestart.Controls.Clear();
            buttonRestart.Click += ClickForRestart;
            pictureRestart.Controls.Add(buttonRestart);
            formForRestart.Controls.Add(pictureRestart);
            pictureRestart.BringToFront();
            formForRestart.MediaStart(music);
            //buttonRestart.PerformClick();
        }

        private void ClickForRestart(object? sender, EventArgs e)
        {
            formForRestart.RestartForm();
        }

        private void InitializzButtonBox(string str)
        {
            Text.Visible = true;
            timerDelayBoxDelete.Start();
            //bitmapButton = new Bitmap(Text.Width, Text.Height);
            graphics?.Clear(Color.AliceBlue);
            Text.Image = Sources.textButton;
            graphics = Graphics.FromImage(Text.Image);
            graphics.DrawString(str, new Font("Times New Roman", 22), Brushes.LightGray, 50, 10);
            //Text.Text = Chest.CreatingChests[chest.Filling];
            Text.BringToFront();
        }

        private void DeathPlayerEnd(object? obj, EventArgs args)
        {
            CreatingPanelForEnd("Вы погибли в бездне...\n Впрочем,история всегда повторяется...", Sources.CatEnd, Color.Gray,Sources.GameDead);
        }
    }
}
