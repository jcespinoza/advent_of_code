using Advent23.Days.Day05;
using AdventOfCode.Commons;

namespace Advent23.Days
{
    public class Day05Solver : Solver<Almanac, long>
    {
        public Day05Solver() : base(2023, 05) { }

        public override Almanac ParseInput(IEnumerable<string> input)
            => Almanac.Parse(input);


        public override long PartOne(Almanac input)
        {
            var location = ComputeLowestLocation(input, useRanges: false);
            return location;
        }

        private long ComputeLowestLocation(Almanac almanac, bool useRanges)
        {
            var settings = new List<FarmingSetting>();
            long[] seedList = useRanges ? GetSeedsAsRanges(almanac.Seeds) : almanac.Seeds;
            long lowest = long.MaxValue;
            foreach (var seed in seedList)
            {
                long location = GetLocation(almanac, seed);
                if(location < lowest)
                {
                    lowest = location;
                }
            }
            return lowest;
        }

        private long[] GetSeedsAsRanges(long[] seeds)
        {
            throw new NotImplementedException();
        }

        private long GetLocation(Almanac almanac, long seed)
        {
            long soil = GetMapping(almanac.Seed_Soil, seed);
            long fertilizer = GetMapping(almanac.Soil_Fert, soil);
            long water = GetMapping(almanac.Fert_Water, fertilizer);
            long light = GetMapping(almanac.Water_Light, water);
            long temperature = GetMapping(almanac.Light_Temp, light);
            long humidity = GetMapping(almanac.Temp_Humid, temperature);
            long location = GetMapping(almanac.Humid_Loc, humidity);
            return location;
        }

        private long GetMapping(FarmingMap[] mappings, long sourceValue)
        {
            foreach (var mapping in mappings)
            {
                long min = mapping.SourceStart;
                long max = mapping.SourceStart + mapping.RangeLength - 1;
                long offset = sourceValue - min;
                if (min <= sourceValue && sourceValue <= max)
                {
                    var matchingTarget = mapping.TargetStart + offset;
                    return matchingTarget;
                }
            }
            return sourceValue;
        }

        public override long PartTwo(Almanac input)
        {
            return ComputeLowestLocation(input, useRanges: true);
        }
    }
}