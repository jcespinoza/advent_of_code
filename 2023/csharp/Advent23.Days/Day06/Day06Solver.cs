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
                long ways = 0;
                for (int tOffset = 1; tOffset < race.Time; tOffset++)
                {
                    var timeHeld = tOffset;
                    var timeRaced = (race.Time - timeHeld);
                    var distance = timeHeld * timeRaced;
                    if(distance > race.Distance)
                    {
                        ways++;
                    }
                }
                product *= ways;
            }
            return product;
        }

        public override long PartTwo(RaceRecord[] input)
        {
            throw new NotImplementedException();
        }
    }
}