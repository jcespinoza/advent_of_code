using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day10Solver : Solver<int[][], long>
    {
        public Day10Solver() : base(2024, 10) { }

        public override int[][] ParseInput(IEnumerable<string> input)
            => input.Select(
                l => l.Select(c => c == '.' ? -1 : c - '0')
                      .ToArray()
            ).ToArray();


        public override long PartOne(int[][] input)
        {
            throw new NotImplementedException();
        }

        public override long PartTwo(int[][] input)
        {
            throw new NotImplementedException();
        }
    }
}