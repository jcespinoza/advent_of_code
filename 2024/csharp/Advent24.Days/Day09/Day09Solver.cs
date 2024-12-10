using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day09Solver : Solver<int[], long>
    {
        public Day09Solver() : base(2024, 09) { }

        public override int[] ParseInput(IEnumerable<string> input)
            => input.First().Select(c => c - '0').ToArray();


        public override long PartOne(int[] input)
        {
            throw new NotImplementedException();
        }

        public override long PartTwo(int[] input)
        {
            throw new NotImplementedException();
        }
    }
}