

namespace Advent23.Days.Day10
{
    public record PipeMap
    {
        public required PipeSegment[][] Segments { get; init; }
        public static PipeMap Parse(string[] lines)
        {
            var segments = new PipeSegment[lines.Length][];
            for (int rowIndex = 0; rowIndex < lines.Length; rowIndex++)
            {
                string currentLine = lines[rowIndex];
                var lineSegments = new PipeSegment[currentLine.Length];
                for (int colIndex = 0; colIndex < currentLine.Length; colIndex++)
                {
                    char currentChar = currentLine[colIndex];
                    var segmentType = PipeSegment.ParseType(currentChar);
                    lineSegments[colIndex] = new PipeSegment
                    {
                        RowIndex = rowIndex,
                        ColIndex = colIndex,
                        SegmentType = segmentType
                    };
                }
                segments[rowIndex] = lineSegments;
            }

            var map = new PipeMap
            {
                Segments = segments
            };

            return map;
        }

        public Point GetStepTarget(Point previous, Point source)
        {
            int rowIndex = source.Row;
            int colIndex = source.Col;
            var sourceSegment = Segments[rowIndex][colIndex];
            var directions = sourceSegment.GetDirections();
            var primaryTarget = Point.Move(source, directions.primary);
            if (primaryTarget != previous) { return primaryTarget; }

            return Point.Move(source, directions.secondary);
        }

        public Point GetStartingCoordinates()
        {
            for (int row = 0; row < Segments.Length; row++)
            {
                for (int col = 0; col < Segments[row].Length; col++)
                {
                    if (Segments[row][col].SegmentType == SegmentType.Start)
                    {
                        return new Point { Col = col, Row = row };
                    }
                }
            }
            throw new Exception("Starting point not found");
        }

        public Tuple<PipeSegment, PipeSegment> GetFirstSteps(Point start)
        {
            var segments = new List<PipeSegment>();
            PipeSegment target;
            if (TryGetNeighbor(start, Direction.North, out target)
                && target.IsTraversable && target.AcceptsVisit(Direction.South))
            {
                segments.Add(target);
            }
            if (TryGetNeighbor(start, Direction.East, out target)
                && target.IsTraversable && target.AcceptsVisit(Direction.West))
            {
                segments.Add(target);
            }
            if (TryGetNeighbor(start, Direction.South, out target)
                && target.IsTraversable && target.AcceptsVisit(Direction.North))
            {
                segments.Add(target);
            }
            if (TryGetNeighbor(start, Direction.West, out target)
                && target.IsTraversable && target.AcceptsVisit(Direction.East))
            {
                segments.Add(target);
            }

            if (segments.Count != 2) throw new Exception("Count is incorrect");

            return Tuple.Create(segments[0], segments[1]);
        }

        private bool TryGetNeighbor(Point start, Direction direction, out PipeSegment target)
        {
            var targetPoint = Point.Move(start, direction);
            int rowIndex = targetPoint.Row;
            int colIndex = targetPoint.Col;
            if (
                rowIndex < 0
                || colIndex < 0
                || rowIndex >= Segments.Length
                || colIndex >= Segments[rowIndex].Length
                )
            {
                target = null;
                return false;
            }

            target = Segments[rowIndex][colIndex];
            return true;
        }
    }

    public enum Direction
    {
        North,
        East,
        South,
        West,
        None
    }
}