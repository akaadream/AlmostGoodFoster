using System.Numerics;

namespace AlmostGoodFoster.Utils
{
    public static class BetterMath
    {
        public static float Exp2(float x) => MathF.Pow(2, x);

        public static float CubicSmoothstep(float x) => x * x * (3f - 2f * x);

        public static float InverseCubicSmoothstep(float x) => 0.5f - (float) Math.Sin(Math.Asin(1f - 2f * x) / 3f);

        public static float QarticSmoothstep(float x) => x * x * (2f - x * x);

        public static float InverseQuarticSmoothstep(float x) => (float)Math.Sqrt(1f - Math.Sqrt(1f - x));

        public static float QuinticSmoothstep(float x) => x * x * x * (x * (x * 6f - 15f) + 10f);

        public static float LerpSmooth(float a, float b, float dt, float h) => b + (a - b) * Exp2(-dt / h);

        public static float Expm1(float n) => MathF.Exp(n) - 1;

        public static float Dim(float x, float y) => y >= x ? 0f : x - y;

        public static int Roundi(float n) => (int)Math.Round(n);

        public static float To1(float min, float max, float value) => ToIntervale(min, max, 0f, 1f, value);

        public static float ToIntervale(float fromMin, float fromMax, float toMin, float toMax, float value) =>
            (value - fromMin) * (toMax - toMin) / (fromMax - fromMin) + toMin;

        public static float ManhattanDistance(Vector2 point1, Vector2 point2) => Math.Abs(point2.X - point1.X) + Math.Abs(point2.Y - point1.Y);

        public static int DiscreteDistance(Vector2 point1, Vector2 point2) => point1 == point2 ? 0 : 1;

        public static T? Min<T>(params T[] values) where T : IComparable<T>
        {
            if (values.Length == 0)
            {
                return default;
            }

            T min = values[0];

            for (int i = 1; i < values.Length; i++)
            {
                if (values[i].CompareTo(min) < 0)
                {
                    min = values[i];
                }
            }

            return min;
        }

        public static T? Max<T>(params T[] values) where T : IComparable<T>
        {
            if (values.Length == 0)
            {
                return default;
            }

            T max = values[0];

            for (int i = 1; i < values.Length; i++)
            {
                if (values[i].CompareTo(max) > 0)
                {
                    max = values[i];
                }
            }

            return max;
        }

        public static int DamerauLevenshteinDistance(string str1, string str2)
        {
            if (string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(str2))
            {
                return 0;
            }

            if (string.IsNullOrEmpty(str1))
            {
                return str2.Length;
            }

            if (string.IsNullOrEmpty(str2))
            {
                return str1.Length;
            }

            var distances = new int[str1.Length + 1, str2.Length + 1];

            for (int i = 0; i <= str1.Length; distances[i, 0] = i++)
            {
                ;
            }

            for (int j = 0; j <= str2.Length; distances[0, j] = j++)
            {
                ;
            }

            for (int i = 1; i <= str1.Length; i++)
            {
                for (int j = 1; j <= str2.Length; j++)
                {
                    int cost = str2[j - 1] == str1[i - 1] ? 0 : 1;
                    distances[i, j] = Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1, distances[i - 1, j - 1] + cost);
                }
            }

            return distances[str1.Length, str2.Length];
        }
    }
}
