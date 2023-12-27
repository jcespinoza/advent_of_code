using Advent23.Days.Day13;
using AdventOfCode.Commons;

namespace Advent23.Days
{
    public class Day13Solver : Solver<TerrainPattern[], long>
    {
        public Day13Solver() : base(2023, 13) { }

        public override TerrainPattern[] ParseInput(IEnumerable<string> input)
            => TerrainPattern.ParsePatternGroups(input);

        public override long PartOne(TerrainPattern[] patterns)
        {
            long totalSummary = 0;
            foreach (var pattern in patterns)
            {
                pattern.HMirrorIndexes = TerrainPattern.ComputeHMirrorIndexes(pattern);

                pattern.VMirrorIndexes = TerrainPattern.ComputeVMirrorIndexes(pattern);

                int hReflectedLines = TerrainPattern.ComputeHReflectedLines(pattern);
                int vReflectedLines = TerrainPattern.ComputeVReflectedLines(pattern);

                int hCount = 100 * hReflectedLines;
                int patternSummary = vReflectedLines + hCount;

                totalSummary += patternSummary;
            }
            return totalSummary;
        }

        public override long PartTwo(TerrainPattern[] input)
        {
            throw new NotImplementedException();
        }
    }
}