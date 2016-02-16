using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Dongtility;

namespace Gravitation
{
    class Program
    {
        static void Main(string[] args)
        {
            //LevelOne();
            LevelTwo();
            //LevelThree();
            Console.ReadKey();
        }

        private static void LevelOne()
        {
            StreamWriter one = File.CreateText("one.txt");
            GravitationEngine engine = new GravitationEngine(one);

            SimpleParticle earth = new SimpleParticle("earth", 5.972E24, Vector.NullVector(), Vector.NullVector(), engine);
            SimpleParticle moon = new SimpleParticle("moon", 7.34767309E22, new Vector(384472282, 0, 0), new Vector(0, 1.022 * 1000, 0), engine);

            engine.AddProjectile(earth);
            engine.AddProjectile(moon);

            engine.Run(20, 236100 / 2, true);
        }

        private static void LevelTwo()
        {
            StreamWriter two = File.CreateText("two.txt");
            GravitationEngine engine = new GravitationEngine(two);

            SimpleParticle moon = new SimpleParticle("moon", 7.34767309E22, new Vector(384472282, 0, 0), new Vector(0, 0, 1.022 * 1000), engine);
            ExtendedParticle earth = new ExtendedParticle("earth");

            double earthRadius = 384472282 / 2;
            double earthMass = 5.972E24;

            double pointIncrement = 18000000;
            double dimensionPoints = Math.Ceiling(2 * earthRadius / pointIncrement); // this is the total number of points it will make on each side
            double massPoint = earthMass / Math.Pow(dimensionPoints, 3);

            for (double x = -earthRadius; x < earthRadius; x += pointIncrement)
            {
                for (double y = -earthRadius; y < earthRadius; y += pointIncrement)
                {
                    for (double z = -earthRadius; z < earthRadius; z += pointIncrement)
                    {
                        earth.AddPoint(new Vector(x, y, z), massPoint);
                    }

                }
            }

            engine.AddProjectile(moon);
            engine.AddProjectile(earth);

            engine.Run(23, 102553, true);
        }

        private static void LevelThree()
        {
            StreamWriter three = File.CreateText("three.txt");
            GravitationEngine engine = new GravitationEngine(three);

            SimpleParticle moon = new SimpleParticle("moon", 7.34767309E22, new Vector(384472282, 0, 0), new Vector(0, 0, 1.022 * 1000), engine);
            ExtendedParticle earth = new ExtendedParticle("earth");

            double earthRadius = 6371392.9;
            double earthMass = 5.972E24;

            double pointIncrement = 1000000 / 2.3;


            double totalPoints = 0;
            for (double x = -earthRadius; x < earthRadius; x += pointIncrement) //taken from http://stackoverflow.com/questions/8671385/sphere-drawing-in-java and slightly altered to make more efficient
            {
                for (double y = -earthRadius; y < earthRadius; y += pointIncrement)
                {
                    for (double z = -earthRadius; z < earthRadius; z += pointIncrement)
                    {
                        if ((x * x) + (y * y) + (z * z) <= earthRadius * earthRadius)
                        {
                            earth.AddPoint(new Vector(x, y, z), 0); // need to figure out how to calculate masspoint
                            totalPoints++;
                        }
                    }
                }
            }

            //unless you figure out a clever way to count the total points in the sphere, you need to iterate through the Earth's points and set their mass to the proper weight here

            List<PointMass> listPoints = earth.allPoints();

            for (int i = 0; i < totalPoints; i++)
            {
                listPoints[i] = new PointMass(listPoints[i].Position, earthMass / totalPoints);
            }



            engine.AddProjectile(moon);
            engine.AddProjectile(earth);

            engine.Run(23, 102553, true);
        }


    }
}


