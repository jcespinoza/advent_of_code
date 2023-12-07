﻿
namespace Advent23.Days.Day05
{
    public record FarmingMap
    {
        public required long SourceStart { get; init; }
        public required long TargetStart { get; init; }
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
                SourceStart = sourceStart,
                RangeLength = rangeLength
            };

            return map;
        }
    }
}