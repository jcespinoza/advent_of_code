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
            List<long> newStoes = PlutonianPebbles.CalculateState(stones, 25);

            return newStoes.Count;
        }

        public override long PartTwo(long[] stones)
        {
            List<long> newStoes = PlutonianPebbles.CalculateState(stones, 75);

            return newStoes.Count;
        }
    }
}