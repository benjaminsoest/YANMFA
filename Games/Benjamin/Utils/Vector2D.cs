using System;

namespace YANMFA.Games.Benjamin.Utils
{
    public struct Vector2D : IEquatable<Vector2D>, IComparable<Vector2D>, IComparable<double>
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Length => Math.Sqrt(X * X + Y * Y);
        private Vector2D Rotate(double a) => new Vector2D(X * Math.Cos(a) - Y * Math.Sin(a), X * Math.Sin(a) + Y * Math.Cos(a));
        private Vector2D Absolute => new Vector2D(X > 0 ? X : -X, Y > 0 ? Y : -Y);

        public override string ToString() => $"({X} ,{Y})";
        public bool Equals(Vector2D other) => X == other.X && Y == other.Y;
        public override bool Equals(object obj) => obj is Vector2D && Equals((Vector2D)obj);
        public override int GetHashCode() => (X, Y).GetHashCode();
        
        public int CompareTo(Vector2D other) => Length.CompareTo(other.Length);
        public int CompareTo(double d) => Length.CompareTo(d);

        public static explicit operator (double x, double y)(Vector2D v) => (v.X, v.Y);
        public void Deconstruct(out double x, out double y)
		{
            x = X;
            y = Y;
		}

        public static implicit operator Vector2D((double x, double y) t) => new Vector2D(t.x, t.y);
        public static implicit operator Vector2D((int x, int y) t) => new Vector2D(t.x, t.y);
        public static implicit operator Vector2D((float x, float y) t) => new Vector2D(t.x, t.y);

        public static Vector2D operator +(Vector2D v) => v;
        public static Vector2D operator -(Vector2D v) => new Vector2D(-v.X, -v.Y);
        public static Vector2D operator ~(Vector2D v) => v.Absolute;

        public static Vector2D operator +(Vector2D a, Vector2D b) => new Vector2D(a.X + b.X, a.Y + b.Y);
        public static Vector2D operator -(Vector2D a, Vector2D b) => new Vector2D(a.X - b.X, a.Y - b.Y);
        public static Vector2D operator *(Vector2D a, Vector2D b) => new Vector2D(a.X * b.X, a.Y * b.Y);
        public static Vector2D operator /(Vector2D a, Vector2D b) => new Vector2D(a.X / b.X, a.Y / b.Y);

        public static bool operator ==(Vector2D a, Vector2D b) => a.X == b.X && a.Y == b.X;
        public static bool operator !=(Vector2D a, Vector2D b) => a.X != b.X || a.Y != b.X;
        public static bool operator >(Vector2D a, Vector2D b) => a.Length > b.Length;
        public static bool operator <(Vector2D a, Vector2D b) => a.Length < b.Length;

        public static Vector2D operator *(Vector2D v, double d) => new Vector2D(v.X * d, v.Y * d);
        public static Vector2D operator *(double d, Vector2D v) => new Vector2D(v.X * d, v.Y * d);
        public static Vector2D operator /(Vector2D v, double d) => new Vector2D(v.X / d, v.Y / d);
        public static Vector2D operator ^(Vector2D a, double d) => a.Rotate(d);
    }
}