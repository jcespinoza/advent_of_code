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

        private List<FarmingSetting> ComputeSettings(Almanac input)
        {
            throw new NotImplementedException();
        }

        public override long PartTwo(Almanac input)
        {
            throw new NotImplementedException();
        }
    }
}