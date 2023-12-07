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
            List<FarmingSetting> farmingSettings = ComputeSettings(input);
            return farmingSettings.Select(f => f.Location).Min();
        }

        private List<FarmingSetting> ComputeSettings(Almanac almanac)
        {
            var settings = new List<FarmingSetting>();
            foreach (var seed in almanac.Seeds)
            {
                long soil = GetMapping(almanac.Seed_Soil, seed);
                long fertilizer = GetMapping(almanac.Soil_Fert, soil);
                long water = GetMapping(almanac.Fert_Water, fertilizer);
                long light = GetMapping(almanac.Water_Light, water);
                long temperature = GetMapping(almanac.Light_Temp, light);
                long humidity = GetMapping(almanac.Temp_Humid, temperature);
                long location = GetMapping(almanac.Humid_Loc, humidity);
                var setting = new FarmingSetting
                {
                    Seed = seed,
                    Soil = soil,
                    Fertilizer = fertilizer,
                    Water = water,
                    Light = light,
                    Temperature = temperature,
                    Humidity = humidity,
                    Location = location
                };
                settings.Add(setting);
            }
            return settings;
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
            throw new NotImplementedException();
        }
    }
}