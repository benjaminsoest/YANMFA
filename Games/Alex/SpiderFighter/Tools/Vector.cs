using System;
using System.Drawing;

namespace YANMFA.Games.Alex.SpiderFighter.Tools
{
    public sealed class Vector
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public PointF ToPoint()
        {
            return new PointF(X, Y);
        }

        public static Vector operator | (Vector a, Vector b)
        {
            return new Vector(b.X - a.X, b.Y - a.Y);
        }

        public static Vector operator / (Vector a, int d)
        {
            return new Vector(a.X / d, a.Y / d);
        }
        public static Vector ConvertUnitVector (Vector a, int d)
        {
            float sign = (float)Math.Sqrt((a.X * a.X) + (a.Y * a.Y));
            float kehrSign = d / (sign);
            return new Vector(a.X * kehrSign, a.Y * kehrSign);
        }

        public static Vector VectorConverter(PointF p)
        {
            return new Vector(p.X, p.Y);
        }
    }
}