using System.Text;

namespace AdventOfCode.Commons
{
    public static class DataPrinter
    {
        public static string Print<T>(IEnumerable<T> data)
        {
            StringBuilder sb = new();
            foreach (T i in data)
            {
                sb.Append((i?.ToString() ?? string.Empty) + ' ');
            }
            return sb.ToString();
        }

        public static string Print<T>(IEnumerable<IEnumerable<T>> grid)
        {
            StringBuilder sb = new();
            foreach (IEnumerable<T> row in grid)
            {
                foreach (T cell in row)
                {
                    sb.Append((cell?.ToString() ?? string.Empty) + ' ');
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
