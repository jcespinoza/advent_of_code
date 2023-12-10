﻿using Advent23.Days.Day10;
using AdventOfCode.Commons;

namespace Advent23.Days
{
    public class Day10Solver : Solver<PipeMap, long>
    {
        public Day10Solver() : base(2023, 10) { }

        public override PipeMap ParseInput(IEnumerable<string> input)
            => PipeMap.Parse(input.ToArray());


        public override long PartOne(PipeMap pipeMap)
        {
            int steps = 0;
            var start = pipeMap.GetStartingCoordinates();
            Point prevSegA = start;
            Point prevSegB = start;

            Tuple<PipeSegment, PipeSegment> firstSegs = pipeMap.GetFirstSteps(start);
            Point currentSegmentA = new(firstSegs.Item1.ColIndex, firstSegs.Item1.RowIndex);
            Point currentSegmentB = new(firstSegs.Item2.ColIndex, firstSegs.Item2.RowIndex);

            do
            {
                steps++;

                if (start != currentSegmentA && currentSegmentA == currentSegmentB) break;
                var backupA = currentSegmentA;
                var backupB = currentSegmentB;
                currentSegmentA = pipeMap.GetStepTarget(prevSegA, currentSegmentA);
                currentSegmentB = pipeMap.GetStepTarget(prevSegB, currentSegmentB);
                prevSegA = backupA;
                prevSegB = backupB;

            } while (true);

            return steps;
        }

        public override long PartTwo(PipeMap input)
        {
            throw new NotImplementedException();
        }
    }
}