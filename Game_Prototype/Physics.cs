using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.Xml;
using System.Text;

namespace Game_Prototype
{
    public class Physics
    {
        //public float ApplyGravity => CalculateGravity();
        public Transform transform;
        //private float gravity;
        private float a;
        public float dx;
        public bool isJumping;
        public float gravity = -9.8f;

        public Physics(PointF pos, Size size)
        {
            transform = new Transform(pos, size);
            gravity = 0;
            a = 0.45f;
           // dx = 0;
            //IsGravityActive = true;
           //vertSpeed = minfall;
            //last = DateTime.Now;
            isJumping = false;
        }

        public void CalculatePhysics()
        {
            if (transform.position.Y < 150 || isJumping)
            {
                transform.position.Y += gravity;
                gravity += a; 
            }

            if (transform.position.Y > 150)
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
             //   isActivated = true;
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

        public bool Collide()
        {
            return false;
        }
    }
}
