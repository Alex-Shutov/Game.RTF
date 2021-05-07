using System.Collections.Generic;
using System.Drawing;

namespace Game_Prototype
{
    public class Physics
    {
        public Transform transform;
        private float a;
        public float dx;
        public bool isJumping;
        public float gravity = -9.8f;
        private HashSet<Rectangle> HashSetWalls;
        //public PermutationForCamera cameraMovement;

        public Physics(PointF pos, Size size, HashSet<Rectangle> hashHashSetWalls)
        {
            transform = new Transform(pos, size);
            gravity = 0;
            a = 0.45f;
            isJumping = false;
            HashSetWalls = hashHashSetWalls;
            
        }

        public void CalculatePhysics()
        {
            if (transform.position.Y < 150 || isJumping)
            {
                transform.position.Y += gravity;
                gravity += a;
            }

            if (transform.position.Y > 70)
            {
                //isActivated = false;
                isJumping = false;
            }
        }

        public void ApplyPhysics()
        {
            CalculatePhysics();
        }

        public void AddForce()
        {

            if (!isJumping)
            {
                isJumping = true;
                gravity = -10;
            }
        }

        public void DownForce()
        {
            transform.position.Y -= gravity;
            gravity -= a;
        }

        //public bool Collide()
        //{

        //}
        //public float CalculateGravity()
        //{
        //    if (dx != 0)
        //    {
        //        transform.position.X +=dx;
        //    }

        //    if (transform.position.Y < 700)
        //    {
        //        transform.position.Y += gravity;
        //        gravity += a;
        //    }

        //    return gravity;
        //}

        //public void AddForce()
        //{
        //    gravity = -10;
        //}
        //public virtual Point Update(float dt)
        //{
        //    var force = Force;
        //    Force = PointF.Empty;
        //    if (IsGravityActive)
        //        force = new PointF(force.X, force.Y + 9.8f);
        //    Velocity = new PointF(Velocity.X + force.X * dt, Velocity.Y + force.Y * dt);
        //    return Position = new Point((int) (Position.X + Velocity.X * dt), (int) (Position.Y + Velocity.Y * dt));

        //}

        public bool Collide(ref PermutationForPlayer permutatuion)
        {
            var dirSize = new Size();
            foreach (var cell in HashSetWalls)
            {
                if (cell.IntersectsWith(new Rectangle(new Point((int)transform.position.X, (int)transform.position.Y),
                    transform.size + new Size(-10, 10))))
                {
                    return true;
                }
                #region -
                //var delta = new PointF();
                //delta.X = (transform.position.X + transform.size.Width / 2) - (cell.X + cell.Width / 2);
                //delta.Y = (transform.position.Y + transform.size.Height / 2) - (cell.Y + cell.Height / 2);
                //if (Math.Abs(delta.X) <= transform.size.Width / 2 + cell.Width / 2)
                //    return true;
                //if (Math.Abs(delta.Y) <= transform.size.Height / 2 + cell.Height / 2)
                //    return true; 
                #endregion


            }
            return false;
        }
        public bool CollideBottom()
        {
            foreach (var wall in HashSetWalls)
            {
                var tmp = new Rectangle(wall.Location.X+5 , wall.Location.Y - 10, wall.Width-10, 5);
                if (tmp.IntersectsWith(new Rectangle(new Point((int)transform.position.X, (int)transform.position.Y),
                    transform.size)))
                    return true;
            }

            return false;
        }
        public bool CollideLeft()
        {
            foreach (var wall in HashSetWalls)
            {
                var tmp = new Rectangle(wall.Location.X - 10, wall.Location.Y + 3, 1, wall.Height - 3);
                if (tmp.IntersectsWith(new Rectangle(new Point((int) transform.position.X, (int) transform.position.Y),
                    transform.size)))
                {
                    PermutationOfCamera.cameraLeft = false;
                    return true;
                }
            }

            PermutationOfCamera.cameraLeft = true;

            return false;
        }
        public bool CollideRight()
        {
            foreach (var wall in HashSetWalls)
            {
                var tmp = new Rectangle(wall.Location.X + wall.Width + 10, wall.Location.Y + 3, 1, wall.Height - 3);
                if (tmp.IntersectsWith(new Rectangle(new Point((int) transform.position.X, (int) transform.position.Y),
                    transform.size)))
                {
                    PermutationOfCamera.cameraRight = false;
                    return true;
                }
            }

            PermutationOfCamera.cameraRight = true;
            return false;
        }

        public bool CollideUp()
        {
            foreach (var wall in HashSetWalls)
            {
                var tmp = new Rectangle(wall.Location.X + 10 , wall.Location.Y - 1, wall.Width + 6, 1);
                if (tmp.IntersectsWith(new Rectangle(new Point((int)transform.position.X, (int)transform.position.Y),
                    transform.size)))
                    return true;
            }

            return false;
        }
    }
}
