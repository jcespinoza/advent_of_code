namespace Advent.Days
{
    public record Range(long Start, long End)
    {
        public static Range From(string s)
        {
            var parts = s.Split('-');
            var start = long.Parse(parts[0]);
            var end = long.Parse(parts[1]);
            return new Range(start, end);
        }
    }
}