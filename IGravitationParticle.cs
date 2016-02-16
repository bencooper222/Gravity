using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gravitation
{
    public interface IGravitationParticle
    {
        string Name { get; set; }
        void Calculate();
        void Update(double timeIncrement);
        List<PointMass> allPoints();
    }
}
