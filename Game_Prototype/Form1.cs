using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game_Prototype.Properties;
using Timer = System.Windows.Forms.Timer;


namespace Game_Prototype
{
    public partial class Form1 : Form
    {

        private Graphics graphics;

        private GameModel game;
        #region MyRegion

        

       
        //private readonly int deathLimit = 3,
        //                     birthLimit = 4;
 #endregion
        private const int MAX_LEAF_SIZE = 20;
        private Stack<Leaf> leafs;
        private Leaf dopLeaf;

        public Form1()
        {
            this.Location = new Point(0, 0);
            InitializeComponent();
           
            Init();
            KeyPreview = true;
        }

        private async void MainTimerEvent(object? sender, EventArgs eventArgs)
        {
            game.MainTimerEvent(this.Right, eventArgs);
            Invalidate();
        }

        private void KeyIsUp(object sender, KeyEventArgs e) => game.KeyIsUpForPlayer(e);

        private void KeyIsDown(object sender, KeyEventArgs e) => game.KeyIsDownForPlayer(e);
        private void Init()
        {
            var side = 70;  
            game = new GameModel(0, 0, 1920 / side, 1080 / side);
            game.PrintMaze();
            Controls.Add(game.box);
            graphics = game.box.CreateGraphics();
            this.KeyDown += new KeyEventHandler(KeyIsDown);
            this.KeyUp += new KeyEventHandler(KeyIsUp);
            this.Paint += new PaintEventHandler(DrawGame);
            var timer1 = new Timer() { Interval = 15 };
            timer1.Tick += new EventHandler(MainTimerEvent);
            timer1.Start();

        }
        //private void DrawMaze(Maze maze)
        //{
        //    MazeBox.Enabled = true;
        //    MazeBox.Image = new Bitmap(MazeBox.Width, MazeBox.Height);
        //    graphics = Graphics.FromImage(MazeBox.Image);
        //    var tmp = maze.GetMaze();
        //    for (int i = 0; i < 6; i++)
        //    {
        //       tmp = maze.Generate();
        //    }
        //    PrintMaze(tmp);
        //}


        //private void PrintMaze(MapCell[,] maze)
        //{
        //    graphics = Graphics.FromImage(game.box.Image);
        //    var side = 70;
        //    for (int x = 0; x < maze.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < maze.GetLength(1); y++)
        //        {
        //            if (maze[x, y] == MapCell.Wall)
        //                graphics.DrawImage(Sources.Tile_1, x * side, y * side, side, side);
        //        }
        //    }
        //}
        
        private void CreateLeafs()
        {
            var root = new Leaf(0, 0, this.Width, this.Height);
            leafs.Push(root);
            var rnd = new Random(DateTime.Now.Millisecond ^ 17341);
            var isAlreadySplit = true;
            var tmp = new Stack<Leaf>();
            while (isAlreadySplit)
            {

                isAlreadySplit = false;

                
                tmp.Push(root);
                // leafs.CopyTo(tmp.ToArray(),0);
                foreach (var elementLeaf in leafs)
                    if (elementLeaf.leftChildLeaf == null && elementLeaf.rightChildLeaf == null)
                        if (elementLeaf.width > MAX_LEAF_SIZE || elementLeaf.height > MAX_LEAF_SIZE ||
                            (float) rnd.NextDouble() > 0.75)
                            if (elementLeaf.Split())
                            {
                                //    from elementLeaf 
                                //        in tmp 
                                //    where elementLeaf.leftChildLeaf == null 
                                //          && elementLeaf.rightChildLeaf == null 
                                //    where elementLeaf.width > MAX_LEAF_SIZE 
                                //          || elementLeaf.height > MAX_LEAF_SIZE ||
                                //    (float) rnd.NextDouble() > 0.25 where elementLeaf.Split() select elementLeaf)
                                //{
                                tmp.Push(elementLeaf.leftChildLeaf);
                                tmp.Push(elementLeaf.rightChildLeaf);
                                isAlreadySplit = true;
                            }
            }

            leafs = tmp;
        }

        private Bitmap PrintLeafs()
        {
            var bush = new List<Brush>()
                {
                    Brushes.Aqua,
                    Brushes.Black,
                    Brushes.Blue,
                    Brushes.Bisque,
                    Brushes.Coral
            };//graphics = MazeBox.CreateGraphics();
            var t = new Bitmap(leafs.Peek().width, leafs.Peek().height);
            graphics = Graphics.FromImage(t);
            var rnd = new Random(DateTime.Now.Millisecond ^ 17391);
            var counter = 0;
            foreach (var e in leafs)
            {
               // var counter = 0;
                graphics.FillRectangle(bush[rnd.Next(bush.Count)], e.x,e.y,e.width,e.height);
                //MazeBox.Image = t;
                //yield return t;
            }

            return t;

            //Controls.Add(graphics);
        }


        private void StartButton_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private  void DrawGame(object? sender, PaintEventArgs e)
        {
         
        }

    }
}
