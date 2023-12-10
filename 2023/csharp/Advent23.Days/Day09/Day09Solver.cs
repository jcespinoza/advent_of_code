using Advent23.Days.Day09;
using AdventOfCode.Commons;
using System.Diagnostics;

namespace Advent23.Days
{
    public class Day09Solver : Solver<HistoryLine[], long>
    {
        public Day09Solver() : base(2023, 09) { }

        public override HistoryLine[] ParseInput(IEnumerable<string> input)
            => input.Select(HistoryLine.Parse).ToArray();


        public override long PartOne(HistoryLine[] historyLines)
        {
            long sumOfExtrapolations = 0;
            foreach (var line in historyLines)
            {
                var extrapolation = line.ExtrapolateFuture();
                sumOfExtrapolations += extrapolation;
            }
            return sumOfExtrapolations;
        }

        public override long PartTwo(HistoryLine[] historyLines)
        {
            long sumOfExtrapolations = 0;
            foreach (var line in historyLines)
            {
                var extrapolation = line.ExtrapolatePast();
                sumOfExtrapolations += extrapolation;
            }
            return sumOfExtrapolations;
        }
    }
}