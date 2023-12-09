using Advent23.Days.Day05;
using AdventOfCode.Commons;
using System.Diagnostics;

namespace Advent23.Days
{
    public class Day05Solver : Solver<Almanac, long>
    {
        public Day05Solver() : base(2023, 05) { }

        public override Almanac ParseInput(IEnumerable<string> input)
            => Almanac.Parse(input);


        public override long PartOne(Almanac input)
        {
            var location = ComputeLowestLocation(input);
            return location;
        }

        private long ComputeLowestLocation(Almanac almanac)
        {
            long lowestLocation = long.MaxValue;
            foreach (var seed in almanac.Seeds)
            {
                long location = GetLocation(almanac, seed);
                if (location < lowestLocation)
                {
                    lowestLocation = location;
                }
            }
            return lowestLocation;
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
                long max = mapping.SourceEnd;
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
            return ComputeLowestLocationWithRanges(input);
        }

        private long ComputeLowestLocationWithRanges(Almanac almanac)
        {
            var seedRanges = almanac.Seeds.Chunk(2).Select(chunk => {
                var maxSeed = chunk[0] + chunk[1] - 1;
                return (chunk[0], maxSeed);                
            }).ToList();

            var lowestLocation = GetLowestLocationForRange(almanac, seedRanges);

            return lowestLocation;
        }

        private long GetLowestLocationForRange(Almanac almanac, List<(long start, long end)> seedRanges)
        {
            var soilRanges = GetMappingRange(almanac.Seed_Soil, seedRanges);
            var fertRanges = GetMappingRange(almanac.Soil_Fert, soilRanges);
            var waterRanges = GetMappingRange(almanac.Fert_Water, fertRanges);
            var lightRanges = GetMappingRange(almanac.Water_Light, waterRanges);
            var tempRanges = GetMappingRange(almanac.Light_Temp, lightRanges);
            var humidRanges = GetMappingRange(almanac.Temp_Humid, tempRanges);
            var locRanges = GetMappingRange(almanac.Humid_Loc, humidRanges);

            return locRanges.Select(r => r.start).Min();
        }

        private List<(long start, long end)> GetMappingRange(FarmingMap[] farmingMappings, List<(long start, long end)> sourceRanges)
        {
            var targetRanges = new List<(long start, long end)>();
            
            for (int index = 0; index < sourceRanges.Count; index++)
            {
                var srcRange = sourceRanges[index];

                foreach (var mapping in farmingMappings.OrderBy(m => m.SourceStart))
                {
                    var offset = mapping.TargetStart - mapping.SourceStart;
                    if (mapping.SourceStart > srcRange.end) continue;
                    if (mapping.SourceEnd < srcRange.start) continue;
                    
                    if(mapping.SourceStart > srcRange.start && mapping.SourceEnd < srcRange.end)
                    {
                        var trgRange = (mapping.TargetStart, mapping.TargetEnd);
                        targetRanges.Add(trgRange);
                        var leftSrcRange = (srcRange.start, mapping.SourceStart);
                        targetRanges.Add(leftSrcRange);
                        srcRange = (mapping.SourceEnd, srcRange.end);
                    }else if(mapping.SourceStart <= srcRange.start && mapping.SourceEnd >= srcRange.end)
                    {
                        var trgRange = (srcRange.start + offset, srcRange.end + offset);
                        targetRanges.Add(trgRange);
                        srcRange.start = srcRange.end = -1;
                        break;
                    }else if(mapping.SourceStart <= srcRange.start)
                    {
                        var trgRange = (srcRange.start + offset, mapping.TargetEnd);
                        targetRanges.Add(trgRange);
                        srcRange.start = mapping.SourceEnd + 1;
                    }else if(mapping.SourceEnd >= srcRange.end)
                    {
                        var trgRange = (mapping.TargetStart, srcRange.end + offset);
                        targetRanges.Add(trgRange);
                        srcRange.end = mapping.SourceStart - 1;
                    }
                    else
                    {
                        throw new NotImplementedException("This case should no happen");
                    }
                }

                // Handle range outside on the right side of all target ranges
                if(srcRange.start > -1 && srcRange.end > -1)
                {
                    targetRanges.Add(srcRange);
                }
            }

            return targetRanges;
        }
    }
}