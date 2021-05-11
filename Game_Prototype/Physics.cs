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
        //private HashSet<Rectangle> HashSetWalls;
        //public PermutationForCamera cameraMovement;
        //private PermutationForCreature playerMovement;
        public bool couldJump;

        public Creature Creature { get; set; }

        public Physics(PointF pos, Size size, Creature creature)
        {
            transform = new Transform(pos, size);
            gravity = 0;
            a = 0.47f;
            isJumping = false;
            Creature = creature;
            //HashSetWalls = hashHashSetWalls;
            //playerMovement = playerMovements;
            //Creature = creature;
        }

        public void CalculatePhysics()
        {
            if (couldJump && gravity >= -15.8f && gravity <= -5)
            {
                transform.position.Y += gravity;
                gravity += a;
                // counterForJump++;
            }

            if ((gravity > -5 || gravity < -15.8f))
            {
                Creature.permutation.goUP = false;
                couldJump = false;
            }

        }

        public void ApplyPhysics()
        {
            CalculatePhysics();
           
        }

        public void AddForce()
        {

            if (!Creature.permutation.goUP)
            {
                gravity = -15.8f;
            }
        }

        public void DownForce()
        {

            if (Creature.permutation.goDOWN)
            {
                Creature.permutation.goUP = false;
                transform.position.Y -= gravity;
                gravity -= a;
                // counterForJump--;
            }
        }


        public bool Collide(ref PermutationForCreature permutatuion)
        {
            var dirSize = new Size();
            foreach (var cell in Maze.HashSetWalls)
            {
                if (cell.IntersectsWith(new Rectangle(new Point((int)transform.position.X, (int)transform.position.Y),
                    transform.size + new Size(-10, 10))))
                {
                    return true;
                }
            }
            return false;
        }
        public bool CollideBottom()
        {
            foreach (var wall in Maze.HashSetWalls)
            {
                var tmp = new Rectangle(wall.Location.X  + 10 , wall.Location.Y-10 , wall.Width -15, 15);
                if (tmp.IntersectsWith(new Rectangle(new Point((int)transform.position.X, (int)transform.position.Y),
                    transform.size - new Size(10,0))))
                {
                    Creature.permutation.goDOWN = false;
                    if (Creature is Player)
                    {
                        AddForce();
                        couldJump = true;
                        DirectionForCamera.CameraDown = false;
                        //counterForJump = 0;
                    }

                    return true;
                }
            }
            if (Creature is Player)
            {
                Creature.permutation.goDOWN = true;
                couldJump = false;
                DirectionForCamera.CameraDown = true;
            }

            return false;
        }
        public bool CollideLeft()
        {
            foreach (var wall in Maze.HashSetWalls)
            {
                var tmp = new Rectangle(wall.Location.X + 15, wall.Location.Y + 3, 10, wall.Height - 2);
                if (tmp.IntersectsWith(new Rectangle(new Point((int)transform.position.X + transform.size.Width, (int)transform.position.Y),
                    new Size(5,transform.size.Height-5))))
                {
                    if (Creature is Player)
                    {
                        DirectionForCamera.CameraLeft = false;
                        //counterForJump = 0;
                    }

                    return true;
                }
            }

            DirectionForCamera.CameraLeft = true;

            return false;
        }
        public bool CollideRight()
        {
            foreach (var wall in Maze.HashSetWalls)
            {
                var tmp = new Rectangle(wall.Location.X + wall.Width - 5, wall.Location.Y + 3, 5, wall.Height - 10);
                if (tmp.IntersectsWith(new Rectangle(new Point((int)transform.position.X - 3 , (int)transform.position.Y),
                    new Size(3,transform.size.Height-5))))
                {
                    if (Creature is Player)
                    {
                        DirectionForCamera.CameraRight = false;
                        //counterForJump = 0;
                    }
                    return true;
                }
            }

            DirectionForCamera.CameraRight = true;
            return false;
        }

        public bool CollideUp()
        {
            foreach (var wall in Maze.HashSetWalls)
            {
                var tmp = new Rectangle(wall.Location.X + 5, wall.Location.Y + wall.Height -15, wall.Width - 15, 10);
                if (tmp.IntersectsWith(new Rectangle(new Point((int) transform.position.X, (int) transform.position.Y),
                    new Size(transform.size.Width - 15, 5))))
                { 
                    
                    if (Creature is Player)
                    {
                        //AddForce();
                        couldJump = false;
                        DirectionForCamera.CameraUp = false;
                        //counterForJump = 0;
                    }
                    return true;
                }

                
            }
            Creature.permutation.goUP = true;
            if (Creature is Player)
            {
               
                couldJump = true;
                DirectionForCamera.CameraUp = true;
                //counterForJump = 0;
            }
            return false;
        }
    }
}
