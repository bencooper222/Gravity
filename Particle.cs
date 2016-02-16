using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dongtility;
using System.IO;

namespace Gravitation
{
    public class Particle
    {



        public Vector Position { get; set; }
        public Vector Velocity { get; set; }
        private Vector acceleration = Vector.NullVector();
        public Vector Acceleration { get { return acceleration; } }

        private double mass;
        public double Mass
        {
            get
            {
                return mass;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("mass", "Mass cannot be zero or negative!");
                }
                else
                {
                    mass = value;
                }
            }
        }

        public string Name { get; set; }

        private GravitationEngine engine;

        public Particle(String name, double mass, Vector position, Vector velocity, GravitationEngine engine)
        {
            Position = position;
            Velocity = velocity;
            this.mass = mass;
            Name = name;
            this.engine = engine;
        }

        override public string ToString()
        {
            return Position + "\t" + Velocity + "\t" + Acceleration;
        }

        /**
         * Changes the velocity based on a given acceleration, assuming a small time step
         * @param acceleration - the acceleration
         * @param timeIncrement - the amount of time the acceleration acts for
         */
        public void AdvanceVelocity(double timeIncrement)
        {
            Velocity += Acceleration * timeIncrement;
        }

        /**
         * Changes the position based on the current velocity, assuming a small time step
         * @param timeIncrement - the amount of time over which the velocity is valid
         */
        public void AdvancePosition(double timeIncrement)
        {
            Position += Velocity * timeIncrement;
        }

        public void SetAccel()
        {
            acceleration = GetFullForce() / mass;
        }

        private Vector GetFullForce()
        {
            Vector force = Vector.NullVector();
            List<PointMass> allPoints = engine.getAllPoints();

            for (int i = 0; i < allPoints.Count; i++)
            {
                Vector directionBetween = (Position - allPoints[i].Position);

                double forceGravity = NewtonGravitational(mass, allPoints[i].Mass, directionBetween.Mag2());
                //    Vector gravityDirection = -(directionBetween).UnitVector();
                Vector gravityDirection = -directionBetween.UnitVector();
                // some crazy shit with direction is going on. Figure it out tomorrow.
                force = force + forceGravity * gravityDirection;



            }

            // TODO: Fill this part in!
            return force;
        }

        private double NewtonGravitational(double mass1, double mass2, double distanceSquared)
        {
            if (distanceSquared != 0)
            {
                double gravConstant = 6.674 * 10E-11;
                return gravConstant * mass1 * mass2 / distanceSquared;
            }
            else
            {
                return 0;
            }
        }





    }
}
