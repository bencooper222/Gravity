using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dongtility;

namespace Gravitation
{
    public class SimpleParticle : IGravitationParticle
    {
        private Particle particle;

        public SimpleParticle(String name, double mass, Vector position, Vector velocity, GravitationEngine engine)
        {
            particle = new Particle(name, mass, position, velocity, engine);
        }

        public string Name
        {
            get
            {
                return particle.Name;
            }

            set
            {
                particle.Name = value;
            }
        }

        public List<PointMass> allPoints()
        {
            var response = new List<PointMass>();
            response.Add(new PointMass (particle.Position, particle.Mass));
            return response;
        }

        public void Calculate()
        {
            particle.SetAccel();
        }

        public void Update(double timeIncrement)
        {
            particle.AdvanceVelocity(timeIncrement);
            particle.AdvancePosition(timeIncrement);
        }

        public override string ToString()
        {
            return particle.ToString();
        }
    }
}
