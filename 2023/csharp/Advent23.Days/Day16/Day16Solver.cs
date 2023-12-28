using Advent23.Days.Day16;
using AdventOfCode.Commons;

namespace Advent23.Days
{
    public class Day16Solver : Solver<Contraption, long>
    {
        public Day16Solver() : base(2023, 16) { }

        public override Contraption ParseInput(IEnumerable<string> input)
            => Contraption.Parse(input.ToArray());


        public override long PartOne(Contraption contraption)
        {
            var firstStep = new Step
            {
                Col = 0,
                Row = 0,
                Towards = Direction.East
            };

            contraption.BeamWalk(firstStep);

            var energizedBeams = contraption.GetEnergizedBeams();
            return energizedBeams;
        }

        public override long PartTwo(Contraption input)
        {
            throw new NotImplementedException();
        }
    }
}