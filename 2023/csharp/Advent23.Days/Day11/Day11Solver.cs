using Advent23.Days.Day11;
using AdventOfCode.Commons;

namespace Advent23.Days
{
    public class Day11Solver : Solver<SpaceMap, long>
    {
        public Day11Solver() : base(2023, 11) { }

        public override SpaceMap ParseInput(IEnumerable<string> input)
            => SpaceMap.Parse(input.ToArray());


        public override long PartOne(SpaceMap spaceMap)
        {
            List<GalaxyPair> distances = spaceMap.GetDistances();
            var sum = distances.Sum(p => p.Steps);
            return sum;
        }

        public override long PartTwo(SpaceMap spaceMap)
        {
            List<GalaxyPair> distances = spaceMap.GetDistances(1_000_000);
            long sum = distances.Sum(p => p.Steps);
            return sum;
        }
    }
}