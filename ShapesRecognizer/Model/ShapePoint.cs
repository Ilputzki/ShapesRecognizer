using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesRecognizer.Model
{
    public struct ShapePoint
    {
        public int X { get; set; }

        public int Y { get; set; }

        public ShapePoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool Equals(ShapePoint other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ShapePoint))
                return false;

            return Equals((ShapePoint) obj);
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() * this.Y.GetHashCode();
        }

        public static bool operator ==(ShapePoint p1, ShapePoint p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(ShapePoint p1, ShapePoint p2)
        {
            return !p1.Equals(p2);
        }
        public static ShapePoint GetPointDifference(ShapePoint a, ShapePoint b)
        {
            return new ShapePoint(a.X - b.X, a.Y - b.Y);
        }
    }
}
