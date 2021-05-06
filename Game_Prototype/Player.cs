using System.Drawing;
using System.Security.Cryptography.Xml;

namespace Game_Prototype
{
    class Player : Creature
    {
        public PermutationForPlayer permutation;
        private int check;
        public int frameCount;
        public int animationCount;
        public int dx;
        public Physics physics;
        public Player(PointF x, Size y)
        {
            permutation = new PermutationForPlayer();
            permutation.Setvelocity(9);
            HP = 100;
            image = Sources.idle_3_1;
            dx = 3;
            physics = new Physics(pos: x, size: y);
        }

        public void UpdatePlayer()
        {
            physics.transform.position.X = Move(physics.transform.position.X, permutation.velocityForPlayer, 0);
        }
        public override float Move(float f, int velocity, int direction)
        {
            if (permutation.goLEFT && physics.transform.position.X > 60)
            {
                return base.Move(physics.transform.position.X, velocity, -1);
            }

            if (permutation.goRIGHT && physics.transform.position.X + physics.transform.size.Width < 1600)
            {
                return base.Move(physics.transform.position.X, velocity, 1);
            }

            return physics.transform.position.X;
        }

        public override void DrawSprites(Graphics g, Image image, Rectangle destRectangle, Rectangle sourceRectangle)
        {
            animationCount++;
            #region Draw(optional)
            //g.DrawImage(Sources.idle_3_1, new Rectangle((int)transform.position.X, (int)transform.position.Y, 90, 150), new Rectangle(frameCount * 92+5, 0, 95, 150),
            //    GraphicsUnit.Pixel);
            //frameCount++;
            //if (frameCount == 7)
            //    frameCount = 0;
            //if (animationCount%10 == 0)
            //    listImages.Draw(g,new Point(100,100),frameCount);
            //frameCount++;
            //if (frameCount == 7)
            //    frameCount = 0;
            //animationCount++;
            //{
            //    frameCount++;
            //frameCount = (animationCount % 40) switch
            //{
            //    < 5 => 0,
            //    >= 5 and < 10 => 1,
            //    >= 10 and < 15 => 2,
            //    >= 15 and < 20 => 3,
            //    >= 20 and < 25 => 4,
            //    >= 25 and < 30 => 5,
            //    >= 30 and < 35 => 6,
            //    >= 35 and < 40 => 7,
            //    _ => frameCount
            //};
            //if (physics.isJumping)
            //base.DrawSprites(g, image, new Rectangle((int)physics.transform.position.X, (int)physics.transform.position.Y, 90, 150), new Rectangle(frameCount * 92 + dx, 0, 95, 150)); 
            #endregion
            base.DrawSprites(g, image, new Rectangle((int)physics.transform.position.X, (int)physics.transform.position.Y, 90, 150), new Rectangle(frameCount * 92 + dx, 0, 95, 150));

            frameCount++;
            if (frameCount == 7)
                frameCount = 0;
        }

        public void GetHP(int tmp)
        {
            if (tmp > physics.transform.position.Y)
                check++;
            else check = 0;
            if (check % 200 == 0 && check != 0)
                HP -= 10;
        }
    }
}
