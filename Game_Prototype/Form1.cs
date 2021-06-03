#nullable enable
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Game_Prototype.PlayerClasses;
using Timer = System.Windows.Forms.Timer;


namespace Game_Prototype
{
    public delegate void MapObjDelegate(Image image, IMapObject source);
    public partial class Form1 : Form
    {
        private GameModel game;
        private PictureBox text;
        private Label tmpLabel;
        private int frameCount;
        public Timer mainTimer;

        public SoundPlayer? media;

        private PictureBox? buttonPicture;
        public Form1()
        { 
            InitializeComponent();
            Init();
            KeyPreview = true;
        }

        public void Init()
        {
            this.StartButton.Size = new Size(100, 40);
            //tmpLabel = new Label() // удалиться
            //{
            //    Size = new Size(300, 200),
            //    Location = new Point(1000, 0),
            //    BackColor = Color.Transparent
            //};
            text = new PictureBox()
            {
                Location = new Point(70, this.Height - 120),
                Size = new Size(this.Width / 2 - 100, 68),
                BackColor = Color.Transparent,
                Image = Sources.textButton
            };
            game = new GameModel(0, 60, (this.Width, this.Height));
            game.AddingDelegates(CreatingEventLinks());

            game.Maze.MazeBox.Paint += new PaintEventHandler(PaintMazeBoxControl);
            AddingControlsToForm();
            InitializeController();
            mainTimer = new Timer() { Interval = 15 };
            mainTimer.Tick += new EventHandler(MainTimerEvent);
            this.Closed += KillingProcess; //костыль
            mainTimer.Start();
            using var music = Sources.GameMusic;
            MediaStart(music);

        }
        public void RestartForm()
        {   //Костыль, может не сработать (убейте в диспетчере задач процесс)
            var m = new Form1();
            m.Show();
            this.Hide();
            foreach (var process in Process.GetProcessesByName("Game_Prototype").Skip(1))
            {
                process.Kill();
                break;
            }

        }

        private void MainTimerEvent(object? sender, EventArgs eventArgs)
        {
            frameCount++;
            if (frameCount == 7)
                frameCount = 0;
            game?.Maze.MazeBox.Invalidate();
            //game.MainTimerEvent(this.Right, eventArgs);
            //tmpLabel.Text = UpdateLabel();
            UpdateButtonFormTimer();
            //UpdateLabel();
        }


        private void KeyIsUp(object sender, KeyEventArgs e) => UserController.KeyIsUpForPlayer(e, game.Player);

        private void KeyIsDown(object sender, KeyEventArgs e) => UserController.KeyIsDownForPlayer(e, game.Player);

        private EventContainer CreatingEventLinks() => new EventContainer(game, this.Controls, buttonPicture,text,this);


        private void AddingControlsToForm()
        {
            this.Controls.Add(tmpLabel);
            this.Controls.Add(game.Maze.MazeBox);
            this.Controls.Add(text);
        }
        private void InitializeController()
        {
            UserController.Game = game;
            this.KeyDown += new KeyEventHandler(KeyIsDown);
            this.KeyUp += new KeyEventHandler(KeyIsUp);
        }
        public void MediaStart(Stream stream)
        {
            media = new SoundPlayer(stream);
            media.LoadAsync();
            media.PlayLooping();
        }

        private void KillingProcess(object? sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("Game_Prototype"))
            {
                process.Kill();
                
            }
        }

        private void PaintMazeBoxControl(object sender, PaintEventArgs e)
        {
            game.Player.DrawSprites(e.Graphics, game.Player.image, new Rectangle(Point.Ceiling(game.Player.physics.transform.position), new Size(game.Player.destWidthForGraphics, 150)), new Rectangle(frameCount * game.Player.widthForGraphics + game.Dx, 0, game.Player.widthForGraphics, 150));

        }

        private void UpdateButtonFormTimer()
        {
            this.StartButton.Text = $@"HP: {game.Player.HP}";
        }
        /// <summary>
        /// Check metod
        /// </summary>
        /// <returns></returns>
        public string UpdateLabel()
        {
            return $"HP: {game.Player.HP}" +
                   $"\nLocationPl:{game.Player.physics.transform.position}" +
                   //$"\nIsUp:{game.Player.UpCounter}" +
                   $"\nLoca:{game.Maze.MapElements?.player.position}" +
                   //$"\nPressed: {game.Maze.MapElements?.objectMap?.WasPressed}" +
                   //$"\nIsWork: {game.Maze.MapElements?.objectMap?.isWorking}" +
                   //$"\nList:{game.Tab.listElements.Count}" +
                   $"\n abc : {game.Maze.MapElements?.bandit}"+ 
                   $"CounterControls: {game.Tab.CountCountrols}";
        }
    }
}
