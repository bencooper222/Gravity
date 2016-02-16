using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Dongtility;

namespace Gravitation
{
    public class ExtendedParticle : IGravitationParticle
    {
        public string Name { get; set; }

        private List<PointMass> points = new List<PointMass>();

        public ExtendedParticle(string name)
        {
            Name = name;
        }

        public void AddPoint(Vector position, double mass)
        {
            points.Add(new PointMass(position, mass));
        }

        public List<PointMass> allPoints()
        {
            return points;
        }

        public void Calculate()
        {
            // Do nothing, unless you want to implement this
        }

        public void Update(double timeIncrement)
        {
            // Do nothing, unless you want to implement this
        }

        public override string ToString()
        {
            return points.First().Position.ToString() + '\t' + Vector.NullVector() + '\t' + Vector.NullVector();
        }
    }
}
