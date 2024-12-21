namespace AdventOfCode.Commons
{
    public static class InputExtensions
    {
        public static char[][] ToCharArray(this IEnumerable<string> input)
            => input.Select(l => l.ToCharArray()).ToArray();

        /// <summary>
        /// Compares two bidimensional arrays and checks the elements are the same
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsTheSameAs<T>(this T[][] input, T[][] target) where T : IEquatable<T>
        {
            if (input.Length != target.Length) return false;

            for (int row = 0; row < input.Length; row++)
            {
                if (input[row].Length != target[row].Length) return false;
                for (int col = 0; col < input[row].Length; col++)
                {
                    if (input[row][col] == null && target[row][col] != null) return false;
                    if (input[row][col] == null && target[row][col] != null) return false;
                    
                    if (!input[row][col].Equals(target[row][col])) return false;
                }
            }

            return true;
        }
    }
}
