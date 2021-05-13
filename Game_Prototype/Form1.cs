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
        private GameModel game;
        #region MyRegion

        

       
        //private readonly int deathLimit = 3,
        //                     birthLimit = 4;
 #endregion

 public Form1()
        {
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
            game = new GameModel(0, 0, 1920 / side, 1080 / side,(this.Width,this.Height)); ;
            this.Controls.Add(game.box);
            this.KeyDown += new KeyEventHandler(KeyIsDown);
            this.KeyUp += new KeyEventHandler(KeyIsUp);
            this.Paint += new PaintEventHandler(DrawGame);
            var timer1 = new Timer() { Interval = 15 };
            timer1.Tick += new EventHandler(MainTimerEvent);
            timer1.Start();
            

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
