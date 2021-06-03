using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Game_Prototype
{
    public static class UserController
    {
        private static GameModel gameThis;

        private static bool flagButton;
        //public static int checkdelete = 0;
        public static GameModel Game 
        {
            set => gameThis = value;
        }
        //public event 

        private static Timer TimerForMovePlayer;
        //public UserController(GameModel Game)
        //{
        //    game = Game;
        //}
        static UserController()
        {
            TimerForMovePlayer = new Timer() { Interval = 15 };
            TimerForMovePlayer.Tick += new EventHandler(MainTimerEvent);
            TimerForMovePlayer.Start();
           
        }
        public static void KeyIsDownForPlayer(KeyEventArgs e, Player player)
        {
            //checkdelete++; 
            
            //gameThis.MainTimerEvent(null, null);
            switch (e.KeyCode)
            {
                case Keys.A:
                    gameThis.SetPropertiesForPlayerAnimation(ref player.permutation.goLEFT, true, 150, 120, 75, Sources.RUN_PNG, true);
                    break;
                case Keys.D:
                    gameThis.SetPropertiesForPlayerAnimation(ref player.permutation.goRIGHT, true, 150, 120, 4, Sources.RUN_PNG, false);
                    break;
                case Keys.W when player.physics.couldJump:
                    player.permutation.goUP = true;
                    gameThis.Maze.counter = 0;
                    break;
                case Keys.X:
                   gameThis.Maze.CameraMoveUp(8);
                    //tmpLabel.Top -= Player.permutation.velocityForPlayer;
                    break;
                case Keys.Z:
                    gameThis.Maze.CameraMoveDown(8);
                    // tmpLabel.Top += Player.permutation.velocityForPlayer;
                    break;
                case Keys.E:
                    gameThis.PressedButtonOnObject();
                    break;
                case Keys.I:
                    gameThis.Tab.PressTab();
                    break;
                case Keys.D1 when flagButton:
                    flagButton = false;
                    gameThis.ActionHealFromInventory();
                    break;
            }


            
        }


        public static void MainTimerEvent(object? sender, EventArgs eventArgs) => gameThis.MainTimerEvent(sender,eventArgs);

        public static void KeyIsUpForPlayer(KeyEventArgs e, Player player)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    gameThis.SetPropertiesForPlayerAnimation(ref player.permutation.goRIGHT, false, 92, 90, 4, Sources.idle_3_1, false);
                    break;
                case Keys.A:
                    gameThis.SetPropertiesForPlayerAnimation(ref player.permutation.goLEFT, false, 92, 90, 9, Sources.idle_3_1, true);
                    break;
                case Keys.W: 
                    player.permutation.goUP = false; 
                    player.permutation.goDOWN = true;
                    break;
                case Keys.I:
                    gameThis.Tab.Unpressed();
                    break;
                case Keys.D1:
                    flagButton = true;
                    break;
                
            }

            //TimerForMovePlayer.Stop();
        }
    }
}
