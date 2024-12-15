using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day11Solver : Solver<long[], long>
    {
        public Day11Solver() : base(2024, 11) { }

        public override long[] ParseInput(IEnumerable<string> input)
            => input.First().Split(' ').Select(long.Parse).ToArray();


        public override long PartOne(long[] stones)
        {
            long newStonesCount = PlutonianPebbles.CountStones(stones, 25);

            return newStonesCount;
        }

        public override long PartTwo(long[] stones)
        {
            long newStonesCount = PlutonianPebbles.CountStones(stones, 75);

            return newStonesCount;
        }
    }
}