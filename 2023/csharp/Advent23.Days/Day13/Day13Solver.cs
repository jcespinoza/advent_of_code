using Advent23.Days.Day13;
using AdventOfCode.Commons;

namespace Advent23.Days
{
    public class Day13Solver : Solver<TerrainPattern[], long>
    {
        public Day13Solver() : base(2023, 13) { }

        public override TerrainPattern[] ParseInput(IEnumerable<string> input)
            => ParseLines(input);

        private TerrainPattern[] ParseLines(IEnumerable<string> lines)
        {
            var patterns = new List<TerrainPattern>();
            var patternLines = new List<string>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    var newPattern = new TerrainPattern
                    {
                        Lines = patternLines.ToArray(),
                    };
                    patterns.Add(newPattern);
                    patternLines = [];
                    continue;
                }
                patternLines.Add(line);
            }

            if (patternLines.Any())
            {
                var newPattern = new TerrainPattern
                {
                    Lines = patternLines.ToArray(),
                };
                patterns.Add(newPattern);
            }

            return patterns.ToArray();
        }

        public override long PartOne(TerrainPattern[] input)
        {
            throw new NotImplementedException();
        }

        public override long PartTwo(TerrainPattern[] input)
        {
            throw new NotImplementedException();
        }
    }
}