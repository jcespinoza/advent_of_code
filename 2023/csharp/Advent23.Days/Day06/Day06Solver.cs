using Advent23.Days.Day06;
using AdventOfCode.Commons;
using System.ComponentModel.DataAnnotations;

namespace Advent23.Days
{
    public class Day06Solver : Solver<RaceRecord[], long>
    {
        public Day06Solver() : base(2023, 06) { }

        public override RaceRecord[] ParseInput(IEnumerable<string> input)
            => RaceRecord.ParseList(input);


        public override long PartOne(RaceRecord[] input)
        {
            var product = 1L;
            foreach (var race in input)
            {
                long ways = GetHoldingArrangements(race);
                product *= ways;
            }
            return product;
        }

        private long GetHoldingArrangements(RaceRecord race)
        {
            long ways = 0;
            for (long tOffset = 1; tOffset < race.Time; tOffset++)
            {
                var timeHeld = tOffset;
                var timeRaced = (race.Time - timeHeld);
                var distance = timeHeld * timeRaced;
                if (distance > race.Distance)
                {
                    ways++;
                }
            }
            return ways;
        }

        public override long PartTwo(RaceRecord[] input)
        {
            RaceRecord race = MergeRecords(input);

            long arrangements = GetHoldingArrangements(race);

            return arrangements;
        }

        private RaceRecord MergeRecords(RaceRecord[] races)
        {
            var timeStr = string.Concat(races.Select(r => $"{r.Time}"));
            var distanceStr = string.Concat(races.Select(r => $"{r.Distance}"));

            return new() { Time = long.Parse(timeStr), Distance = long.Parse(distanceStr) };
        }
    }
}