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

        public override long PartTwo(Contraption original)
        {
            long maxEnergizedBeams = 0;
            for (int row = 0; row < original.Grid.Length; row++)
            {
                Contraption test = original.Duplicate();
                test.BeamWalk(new Step { Col = 0, Row = row, Towards = Direction.East});
                maxEnergizedBeams = Math.Max(maxEnergizedBeams, test.GetEnergizedBeams());


                test = original.Duplicate();
                test.BeamWalk(new Step { 
                        Col = test.Grid[row].Length - 1, Row = row, Towards = Direction.West 
                    });
                maxEnergizedBeams = Math.Max(maxEnergizedBeams, test.GetEnergizedBeams());
            }
            
            for (int col = 0; col < original.Grid[0].Length; col++)
            {
                Contraption test = original.Duplicate();
                test.BeamWalk(new Step { Col = col, Row = 0, Towards = Direction.South});
                maxEnergizedBeams = Math.Max(maxEnergizedBeams, test.GetEnergizedBeams());


                test = original.Duplicate();
                test.BeamWalk(new Step { 
                        Col = col, Row = test.Grid.Length - 1, Towards = Direction.North 
                    });
                maxEnergizedBeams = Math.Max(maxEnergizedBeams, test.GetEnergizedBeams());
            }


            return maxEnergizedBeams;
        }
    }
}