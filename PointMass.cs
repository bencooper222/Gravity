using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dongtility;

namespace Gravitation
{
    public struct PointMass
    {
        public Vector Position { get; set; }
        public double Mass { get; set; }
        
        public PointMass(Vector position, double mass)
        {
            Position = position;
            Mass = mass;
        }
    }
}
