using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SpaceLinesAndPoints
{
    class Point
    {
        public double x;
        public double y;
        public double xv;
        public double yv;
        public static int width = 600;
        public static int height = 600;
        public static int speed = 1;
        public static Random rnd = new Random();
        public List<Vector2> moons = new List<Vector2> { };

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
            xv = (-1 + rnd.NextDouble() * 2) * speed;
            yv = (-1 + rnd.NextDouble() * 2) * speed;

        }

        public void AddMoons(int moonCount)
        {
            if (moonCount > 0)
            {
                moons.Clear();
                double angle = 0;
                double rot = 360 / moonCount;
                for (int i = 0; i < moonCount; i++)
                {
                    double sin = Math.Sin((i * rot) * (Math.PI / 180)+Math.PI);
                    double cos = Math.Cos((i * rot) * (Math.PI / 180)+Math.PI);
                    Vector2 moon = new Vector2((float)(this.x + sin * 10), (float)(this.y + cos * 10));
                    moons.Add(moon);
                    angle = angle + rot;
                }
            }
        }

        public void Update()
        {
            x = x + xv;
            y = y + yv;

            if (x < 0) x = width;
            if (x > width) x = 0;
            if (y < 0) y = height;
            if (y > height) y = 0;

            xv *= 0.9985;
            yv *= 0.9985;
        }
    }
}
