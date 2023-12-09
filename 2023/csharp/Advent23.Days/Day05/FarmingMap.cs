
namespace Advent23.Days.Day05
{
    public record FarmingMap
    {
        public required long SourceStart { get; init; }
        public required long SourceEnd { get; init; }
        public required long TargetStart { get; init; }
        public required long TargetEnd { get; init; }
        public required long RangeLength { get; init; }

        public static FarmingMap Parse(string line)
        {
            var parts = line
                .Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var targetStart = long.Parse(parts[0].Trim());
            var sourceStart = long.Parse(parts[1].Trim());
            var rangeLength = long.Parse(parts[2].Trim());
            var map = new FarmingMap
            {
                TargetStart = targetStart,
                TargetEnd = targetStart + rangeLength - 1,
                SourceStart = sourceStart,
                SourceEnd = sourceStart + rangeLength - 1,
                RangeLength = rangeLength
            };

            return map;
        }
    }
}