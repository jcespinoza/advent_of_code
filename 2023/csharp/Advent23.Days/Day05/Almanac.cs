
namespace Advent23.Days.Day05
{
    public record Almanac
    {
        public  required long[] Seeds { get; init; }
        public required FarmingMap[] Seed_Soil { get; set; }
        public required FarmingMap[] Soil_Fert { get; init; }
        public required FarmingMap[] Fert_Water { get; init; }
        public required FarmingMap[] Water_Light { get; init; }
        public required FarmingMap[] Light_Temp { get; init; }
        public required FarmingMap[] Temp_Humid { get; init; }
        public required FarmingMap[] Humid_Loc { get; init; }

        private enum ParsingPhase
        {
            Seeds,
            SeedToSoil,
            SoilToFertilizer,
            FertilizerToWater,
            WaterToLight,
            LightToTemperature,
            TemperatureToHumidity,
            HumidityToLocation
        }


        public static Almanac Parse(IEnumerable<string> input)
        {
            var seedList = Array.Empty<long>();
            var seedToSoil = new List<FarmingMap>();
            var soilToFert = new List<FarmingMap>();
            var fertToWater = new List<FarmingMap>();
            var waterToLight = new List<FarmingMap>();
            var lightToTemp = new List<FarmingMap>();
            var tempToHumid = new List<FarmingMap>();
            var humidToLoc = new List<FarmingMap>();

            var phase = ParsingPhase.Seeds;
            foreach (var line in input)
            {
                if (line.Contains("map:")) continue;

                if(line == string.Empty)
                {
                    phase++;
                    continue;
                }

                switch (phase)
                {
                    case ParsingPhase.Seeds:
                        seedList = ProcessSeedList(line);
                        break;
                    case ParsingPhase.SeedToSoil:
                        seedToSoil.Add(FarmingMap.Parse(line));
                        break;
                    case ParsingPhase.SoilToFertilizer:
                        soilToFert.Add(FarmingMap.Parse(line));
                        break;
                    case ParsingPhase.FertilizerToWater:
                        fertToWater.Add(FarmingMap.Parse(line));
                        break;
                    case ParsingPhase.WaterToLight:
                        waterToLight.Add(FarmingMap.Parse(line));
                        break;
                    case ParsingPhase.LightToTemperature:
                        lightToTemp.Add(FarmingMap.Parse(line));
                        break;
                    case ParsingPhase.TemperatureToHumidity:
                        tempToHumid.Add(FarmingMap.Parse(line));
                        break;
                    case ParsingPhase.HumidityToLocation:
                        humidToLoc.Add(FarmingMap.Parse(line));
                        break;
                    default:
                        break;
                }
            }

            var almanac = new Almanac
            {
                Seeds = [.. seedList],
                Seed_Soil = [.. seedToSoil],
                Soil_Fert = [.. soilToFert],
                Fert_Water = [.. fertToWater],
                Water_Light = [.. waterToLight],
                Light_Temp = [.. lightToTemp],
                Temp_Humid = [.. tempToHumid],
                Humid_Loc = [.. humidToLoc],
            };

            return almanac;
        }

        private static long[] ProcessSeedList(string line)
        {            
            return line.Split(':')[1]
                .Trim()
                .Split(" ")
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(long.Parse)
                .ToArray();
        }
    }
}