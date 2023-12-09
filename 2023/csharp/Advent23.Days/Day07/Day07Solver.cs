using Advent23.Days.Day07;
using AdventOfCode.Commons;

namespace Advent23.Days
{
    public class Day07Solver : Solver<BidHand[], long>
    {
        public Day07Solver() : base(2023, 07) { }

        public override BidHand[] ParseInput(IEnumerable<string> input)
            => input.Select(BidHand.Parse).ToArray();


        public override long PartOne(BidHand[] input)
        {
            var sorted = input.OrderBy(bh => bh.HandType).ThenBy(bh => bh.Hand).ToArray();
            var overallWinnings = 0L;
            for (int index = 0; index < sorted.Length; index++)
            {
                var handWinnings = sorted[index].BidAmt * (index + 1);
                overallWinnings += handWinnings;
            }
            return overallWinnings;
        }

        public override long PartTwo(BidHand[] input)
        {
            throw new NotImplementedException();
        }
    }
}