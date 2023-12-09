using Advent23.Days.Day07;
using AdventOfCode.Commons;

namespace Advent23.Days
{
    public class Day07Solver : Solver<BidHand[], long>
    {
        public const char JOKER_SYMBOL = 'J';

        public Day07Solver() : base(2023, 07) { }

        public override BidHand[] ParseInput(IEnumerable<string> input)
            => input.Select(BidHand.Parse).ToArray();


        public override long PartOne(BidHand[] biddingHands)
        {
            var sortedHands = biddingHands.OrderBy(bh => bh.HandType).ThenBy(bh => bh.Hand).ToArray();
            var overallWinnings = 0L;
            for (int index = 0; index < sortedHands.Length; index++)
            {
                var handWinnings = sortedHands[index].BidAmt * (index + 1);
                overallWinnings += handWinnings;
            }
            return overallWinnings;
        }

        public override long PartTwo(BidHand[] biddingHands)
        {
            foreach (var biddingHand in biddingHands)
            {
                string originalHand = biddingHand.Hand;
                string bestHand = BidHand.ComputeBestHand(originalHand);
                biddingHand.HandType = BidHand.ComputeHandType(bestHand);
                biddingHand.Hand = originalHand.Replace(BidHand.CARD_NAMES[JOKER_SYMBOL], 'J');
            }
            var sortedHands = biddingHands.OrderBy(bh => bh.HandType).ThenBy(bh => bh.Hand).ToArray();
            var overallWinnings = 0L;
            for (int index = 0; index < sortedHands.Length; index++)
            {
                var handWinnings = sortedHands[index].BidAmt * (index + 1);
                overallWinnings += handWinnings;
            }
            return overallWinnings;
        }
    }
}