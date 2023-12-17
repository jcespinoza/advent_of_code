using Advent23.Days.Day10;
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
            var stepsUntilFurthestPoint = TraversePipe(pipeMap);
            return stepsUntilFurthestPoint;
        }

        private static long TraversePipe(PipeMap pipeMap)
        {
            int steps = 0;
            var start = pipeMap.GetStartingCoordinates();

            pipeMap.MarkVisited(start);

            Point prevSegA = start;
            Point prevSegB = start;

            Tuple<PipeSegment, PipeSegment, Direction, Direction> firstSegs = pipeMap.GetFirstSteps(start);
            Point currentSegmentA = new(firstSegs.Item1.ColIndex, firstSegs.Item1.RowIndex);
            Point currentSegmentB = new(firstSegs.Item2.ColIndex, firstSegs.Item2.RowIndex);
            
            var startingSegType = PipeSegment.BuildFromDirections([firstSegs.Item3, firstSegs.Item4]);
            pipeMap.MutateSegmentWithType(start, startingSegType);

            do
            {
                steps++;
                pipeMap.MarkVisited(currentSegmentA);
                pipeMap.MarkVisited(currentSegmentB);

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

        public override long PartTwo(PipeMap pipeMap)
        {
            _ = TraversePipe(pipeMap);

            var notVisitedSegments = pipeMap.GetNotVisitedSegments()
                                    .Where(s => s.ColIndex != 0  && s.RowIndex != 0
                                        && s.RowIndex < pipeMap.Segments.Length - 1
                                        && s.ColIndex < pipeMap.Segments[s.RowIndex].Length - 1
                                    );

            var containedSegments = 0;
            foreach (var nvSegment in notVisitedSegments)
            {
                var obstructionArray = pipeMap.GetEdgeObstructions(nvSegment);
                var oddObstructions = obstructionArray.Where(o => o % 2 != 0);
                if(oddObstructions.Any())
                {
                    containedSegments++;
                }
            }
            
            return containedSegments;
        }
    }
}