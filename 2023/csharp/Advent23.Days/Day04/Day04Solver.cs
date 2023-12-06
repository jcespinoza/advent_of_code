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
            long totalPoints = 0;
            foreach (var card in input)
            {
                var points = ComputePoints(card);
                totalPoints += points;
            }
            return totalPoints;
        }

        private long ComputePoints(ScratchCard card)
        {
            var intersectionCount = card.Winners.Intersect(card.Possesion).Count();
            if (intersectionCount <= 0) return 0;

            return Convert.ToInt64(Math.Pow(2, intersectionCount - 1));
        }

        public override long PartTwo(ScratchCard[] input)
        {
            throw new NotImplementedException();
        }
    }
}