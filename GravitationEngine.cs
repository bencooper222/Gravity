using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dongtility;

namespace Gravitation
{
    public class GravitationEngine
    {
        private double time = 0;
        public double Time { get { return time; } }

        private IList<IGravitationParticle> projectiles = new List<IGravitationParticle>();
        private StreamWriter ps;

        public GravitationEngine(StreamWriter ps)
        {
            this.ps = ps;
        }

        public void Run(double timeIncrement, int nIncrements, bool print = true)
        {
            if (print)
            {
                ps.WriteLine(header());
            }

            Console.WriteLine("Running " + nIncrements + " iterations.");

            for (int i = 0; i < nIncrements; ++i)
            {
                if (i % 100 == 0)
                {
                    Console.WriteLine("Completed " + ((double)i / nIncrements * 100) + " percent.");
                }

                increment(timeIncrement);
                if (print)
                {
                    ps.WriteLine(ToString());
                }

            }
        }

        public void AddProjectile(IGravitationParticle projectile)
        {
            if (projectiles.Contains(projectile))
            {
                throw new InvalidOperationException("Attempted to add Projectile that already exists!");
            }
            else
            {
                projectiles.Add(projectile);
            }
        }

        override public string ToString()
        {
            string response = "";
            foreach (var projectile in projectiles)
            {
                response += "\t" + Time + "\t" + projectile;
            }
            return response;
        }

        private string header()
        {
            string response = "";
            foreach (var projectile in projectiles)
            {
                response += projectile.Name;
                response += "\ttime\tx\ty\tz\tMag\tvx\tvy\tvz\tSpeed\tax\tay\taz\t|a|";
            }
            return response;
        }

        public void increment(double timeIncrement)
        {
            time += timeIncrement;

            foreach (var projectile in projectiles)
            {
                projectile.Calculate();
            }
            foreach (var projectile in projectiles)
            {
                projectile.Update(timeIncrement);
            }
        }

        public List<PointMass> getAllPoints()
        {
            var answer = new List<PointMass>();
            foreach (var particle in projectiles)
            {
                answer.AddRange(particle.allPoints());
            }
            return answer;
        }
    }
}
