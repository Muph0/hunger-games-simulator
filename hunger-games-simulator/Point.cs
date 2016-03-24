using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hunger_games_simulator
{
    [Serializable]
    struct Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int distanceSquared(Point p)
        {
            int x = X - p.X;
            int y = Y - p.Y;
            return x * x + y * y;
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }
    }
}
