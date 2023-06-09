using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpringRenderer
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Vector2 : IEquatable<Vector2>
    {
        public float X;
        public float Y;

        private static Vector2 zeroVector = new Vector2(0f, 0f);
        private static Vector2 unitVector = new Vector2(1f, 1f);
        private static Vector2 unitXVector = new Vector2(1f, 0f);
        private static Vector2 unitYVector = new Vector2(0f, 1f);

        #region Properties

        public static Vector2 Zero
        {
            get { return zeroVector; }
        }

        public static Vector2 One
        {
            get { return unitVector; }
        }

        public static Vector2 UnitX
        {
            get { return unitXVector; }
        }

        public static Vector2 UnitY
        {
            get { return unitYVector; }
        }

        #endregion Properties
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        public Vector2(float value)
        {
            X = value;
            Y = value;
        }
        public static float Distance(Vector2 value1, Vector2 value2)
        {
            float result;
            DistanceSquared(value1, value2, out result);
            return (float)Math.Sqrt(result);
        }
        public static void Distance(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            DistanceSquared(value1, value2, out result);
            result = (float)Math.Sqrt(result);
        }

        public static float DistanceSquared(Vector2 value1, Vector2 value2)
        {
            float result;
            DistanceSquared(value1, value2, out result);
            return result;
        }
        public static float Dot(Vector2 value1, Vector2 value2)
        {
            return value1.X * value2.X + value1.Y * value2.Y;
        }
        public static void Dot(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            result = value1.X * value2.X + value1.Y * value2.Y;
        }
        public float Length()
        {
            float result;
            DistanceSquared(this, zeroVector, out result);
            return (float)Math.Sqrt(result);
        }

        public float LengthSquared()
        {
            float result;
            DistanceSquared(this, zeroVector, out result);
            return result;
        }
        public void Normalize()
        {
            Normalize(this, out this);
        }

        public static Vector2 Normalize(Vector2 value)
        {
            Normalize(value, out value);
            return value;
        }

        public static void Normalize(Vector2 value, out Vector2 result)
        {
            float factor;
            DistanceSquared(value, zeroVector, out factor);
            factor = 1f / (float)Math.Sqrt(factor);
            result.X = value.X * factor;
            result.Y = value.Y * factor;
        }
        public static void DistanceSquared(Vector2 value1, Vector2 value2, out float result)
        {
            result = (value1.X - value2.X) * (value1.X - value2.X) + (value1.Y - value2.Y) * (value1.Y - value2.Y);
        }
        public Vector2 LeftPerp()
        {
            return new Vector2(-Y, X);
        }
        public Vector2 RightPerp()
        {
            return new Vector2(Y, -X);
        }
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }
        public static Vector2 operator *(Vector2 left, float scaler)
        {
            return new Vector2(left.X * scaler, left.Y * scaler);
        }
        public static Vector2 operator *(float scaler, Vector2 value)
        {
            value.X *= scaler;
            value.Y *= scaler;
            return value;
        }
        public static float operator *(Vector2 left, Vector2 right)
        {
            return left.X * right.X + left.Y * right.Y;
        }
        public static Vector2 operator /(Vector2 left, float scaler)
        {
            return new Vector2(left.X / scaler, left.Y / scaler);
        }
        public static Vector2 operator /(float scaler, Vector2 value)
        {
            value.X /= scaler;
            value.Y /= scaler;
            return value;
        }
        public static Vector2 operator -(Vector2 self)
        {
            self.X = -self.X;
            self.Y = -self.Y;
            return self;
        }
        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return !(left == right);
        }

        public static implicit operator PointF(Vector2 self)
        {
            return new PointF(self.X, self.Y);
        }
        public static implicit operator Vector2(PointF point)
        {
            return new Vector2(point.X, point.Y);
        }
        public static implicit operator Point(Vector2 self)
        {
            return new Point((int)self.X, (int)self.Y);
        }
        public static implicit operator Vector2(Point point)
        {
            return new Vector2(point.X, point.Y);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Vector2))
                return false;
            return Equals((Vector2)obj);
        }
        public bool Equals(Vector2 other)
        {
            return X.Equals(other.X) && Y.Equals(other);
        }
        public override int GetHashCode()
        {
            return new float[2] { X, Y }.GetHashCode();
        }
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
