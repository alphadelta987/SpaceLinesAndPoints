using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static Random rnd = new Random();
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
            xv = - 1 + rnd.NextDouble()*2;
            yv = - 1 + rnd.NextDouble()*2;
        }

        public void Update()
        {
            x = x + xv;
            y = y + yv;

            if (x < 0) x = width;
            if (x > width) x = 0;
            if (y < 0) y = height;
            if (y > height) y = 0;
        }
    }
}
