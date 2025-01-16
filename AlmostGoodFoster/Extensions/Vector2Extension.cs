using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodFoster.Extensions
{
    public static class Vector2Extension
    {
        public static Vector2 WithXY(this Vector2 vector, float x, float y)
        {
            return vector.WithX(x).WithY(y);
        }

        public static Vector2 WithXY(this Vector2 vector, Vector2 xy)
        {
            return vector.WithXY(xy.X, xy.Y);
        }

        public static Vector2 WithX(this Vector2 vector, float x)
        {
            vector.X = x;
            return vector;
        }

        public static Vector2 WithY(this Vector2 vector, float y)
        {
            vector.Y = y;
            return vector;
        }

        public static Vector2 Multiply(this Vector2 vector1, Vector2 vector2)
        {
            vector1.X *= vector2.X;
            vector1.Y *= vector2.Y;
            return vector1;
        }

        public static Vector2 Divide(this Vector2 vector1, Vector2 vector2)
        {
            vector1.X /= vector2.X;
            vector1.Y /= vector2.Y;
            return vector1;
        }

        public static Vector2 Add(this Vector2 vector1, Vector2 vector2)
        {
            vector1.X += vector2.X;
            vector1.Y += vector2.Y;
            return vector1;
        }

        public static Vector2 Substract(this Vector2 vector1, Vector2 vector2)
        {
            vector1.X -= vector2.X;
            vector1.Y -= vector2.Y;
            return vector1;
        }
    }
}
