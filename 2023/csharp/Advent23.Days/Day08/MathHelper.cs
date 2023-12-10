namespace Advent23.Days.Day08
{
    /// <summary>
    /// Adapted from https://stackoverflow.com/a/74765134/1544395
    /// </summary>
    public static class MathExtensios
    {
        public static long GreatestCommonDivisor(long a, long b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        public static long LeastCommonMultiple(long a, long b)
            => a / GreatestCommonDivisor(a, b) * b;

        public static long LeastCommonMultiple(this IEnumerable<long> values)
            => values.Aggregate(LeastCommonMultiple);
    }
}
