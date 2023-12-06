using Advent23.Days.Day04;
using AdventOfCode.Commons;

namespace Advent23.Days
{
    public class Day04Solver : Solver<ScratchCard[], long>
    {
        public Day04Solver() : base(2023, 04) { }

        public override ScratchCard[] ParseInput(IEnumerable<string> input)
            => input.Select(ScratchCard.Parse).ToArray();


        public override long PartOne(ScratchCard[] input)
        {
            throw new NotImplementedException();
        }

        public override long PartTwo(ScratchCard[] input)
        {
            throw new NotImplementedException();
        }
    }
}