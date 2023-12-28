using Advent23.Days.Day10;

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

        public Tuple<PipeSegment, PipeSegment, Direction, Direction> GetFirstSteps(Point start)
        {
            var segments = new List<PipeSegment>(2);
            var directions = new List<Direction>(2);
            PipeSegment target;
            if (TryGetNeighbor(start, Direction.North, out target)
                && target.IsTraversable && target.AcceptsVisit(Direction.South))
            {
                segments.Add(target);
                directions.Add(Direction.North);
            }
            if (TryGetNeighbor(start, Direction.East, out target)
                && target.IsTraversable && target.AcceptsVisit(Direction.West))
            {
                segments.Add(target);
                directions.Add(Direction.East);
            }
            if (TryGetNeighbor(start, Direction.South, out target)
                && target.IsTraversable && target.AcceptsVisit(Direction.North))
            {
                segments.Add(target);
                directions.Add(Direction.South);
            }
            if (TryGetNeighbor(start, Direction.West, out target)
                && target.IsTraversable && target.AcceptsVisit(Direction.East))
            {
                segments.Add(target);
                directions.Add(Direction.West);
            }

            if (segments.Count != 2 || directions.Count != 2) throw new Exception("Count is incorrect");

            return Tuple.Create(segments[0], segments[1], directions[0], directions[1]);
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
                target = new PipeSegment {ColIndex = -1, RowIndex = -1,SegmentType = SegmentType.Start };
                return false;
            }

            target = Segments[rowIndex][colIndex];
            return true;
        }

        public void MarkVisited(Point location)
        {
            Segments[location.Row][location.Col].Visited = true;
        }

        public IEnumerable<PipeSegment> GetNotVisitedSegments()
        {
            return Segments.SelectMany(s => s)
                    .Where(s => !s.Visited);
        }

        public int[] GetEdgeObstructions(PipeSegment sourceSegment)
        {
            var srcRow = sourceSegment.RowIndex;
            var srcCol = sourceSegment.ColIndex;
            int topObstructions = 0;
            int bottomObstructions = 0;
            int leftObstructions = 0;
            int rightObstructions = 0;
            Stack<SegmentType> foundNorthward = new();
            Stack<SegmentType> foundEastward = new();
            Stack<SegmentType> foundSouthward = new();
            Stack<SegmentType> foundWestward = new();

            // Use same offset in all directions
            for (int offset = 1; offset < Math.Max(Segments.Length, Segments.Max(s => s.Length)); offset++)
            {
                // Validate if index would go outside the boundaries
                if(srcRow - offset >= 0 )
                {
                    var segment = Segments[srcRow - offset][srcCol];
                    SegmentType segType = segment.SegmentType;

                    if(foundNorthward.TryPeek(out SegmentType foundN) && segment.Visited)
                    {
                        if(foundN == SegmentType.AngleNW && segType == SegmentType.AngleSE
                            || foundN == SegmentType.AngleNE && segType == SegmentType.AngleSW)
                        {
                            foundNorthward.Pop();
                            topObstructions++;
                        }else if(foundN == SegmentType.AngleNW && segType == SegmentType.AngleSW
                            || foundN == SegmentType.AngleNE && segType == SegmentType.AngleSE)
                        {
                            foundNorthward.Pop();
                        }
                    }

                    var isPerpendicular = segType == SegmentType.LineEW;
                    var shouldAdd = isPerpendicular && segment.Visited;

                    topObstructions += shouldAdd ? 1 : 0;

                    if (segType == SegmentType.AngleNW || segType == SegmentType.AngleNE)
                    {
                        foundNorthward.Push(segType);
                    }
                }
                if(srcRow + offset < Segments.Length)
                {
                    var segment = Segments[srcRow + offset][srcCol];
                    SegmentType segType = segment.SegmentType;

                    if (foundSouthward.TryPeek(out SegmentType foundS) && segment.Visited)
                    {
                        if (foundS == SegmentType.AngleSW && segType == SegmentType.AngleNE
                            || foundS == SegmentType.AngleSE && segType == SegmentType.AngleNW)
                        {
                            foundSouthward.Pop();
                            bottomObstructions++;
                        }else if(foundS == SegmentType.AngleSW && segType == SegmentType.AngleNW
                            || foundS == SegmentType.AngleSE && segType == SegmentType.AngleNE)
                        {
                            foundSouthward.Pop();
                        }
                    }
                    
                    var isPerpendicular = segType == SegmentType.LineEW;
                    var shouldAdd = isPerpendicular && segment.Visited;
                    bottomObstructions += shouldAdd ? 1 : 0;

                    if (segType == SegmentType.AngleSW || segType == SegmentType.AngleSE)
                    {
                        foundSouthward.Push(segType);
                    }
                }
                if(srcCol - offset >= 0)
                {
                    var segment = Segments[srcRow][srcCol - offset];
                    SegmentType segType = segment.SegmentType;

                    if (foundWestward.TryPeek(out SegmentType foundW) && segment.Visited)
                    {
                        if (foundW == SegmentType.AngleNW && segType == SegmentType.AngleSE
                            || foundW == SegmentType.AngleSW && segType == SegmentType.AngleNE)
                        {
                            foundWestward.Pop();
                            leftObstructions++;
                        }else if(foundW == SegmentType.AngleNW && segType == SegmentType.AngleNE
                            || foundW == SegmentType.AngleSW && segType == SegmentType.AngleSE)
                        {
                            foundWestward.Pop();
                        }
                    }


                    var isPerpendicular = segType == SegmentType.LineNS;
                    var shouldAdd = isPerpendicular && segment.Visited;
                    leftObstructions += shouldAdd ? 1 : 0;

                    if (segType == SegmentType.AngleNW || segType == SegmentType.AngleSW)
                    {
                        foundWestward.Push(segType);
                    }
                }
                if(srcCol + offset < Segments[srcRow].Length)
                {
                    var segment = Segments[srcRow][srcCol + offset];
                    SegmentType segType = segment.SegmentType;

                    if (foundEastward.TryPeek(out SegmentType foundE) && segment.Visited)
                    {
                        if (foundE == SegmentType.AngleSE && segType == SegmentType.AngleNW
                            || foundE == SegmentType.AngleNE && segType == SegmentType.AngleSW)
                        {
                            foundEastward.Pop();
                            rightObstructions++;
                        }else if(foundE == SegmentType.AngleSE && segType == SegmentType.AngleSW
                            || foundE == SegmentType.AngleNE && segType == SegmentType.AngleNW)
                        {
                            foundEastward.Pop();
                        }
                    }

                    var isPerpendicular = segType == SegmentType.LineNS;
                    var shouldAdd = isPerpendicular && segment.Visited;
                    rightObstructions += shouldAdd ? 1 : 0;

                    if (segType == SegmentType.AngleSE || segType == SegmentType.AngleNE)
                    {
                        foundEastward.Push(segType);
                    }
                }
            }

            return [topObstructions, bottomObstructions, leftObstructions, rightObstructions];
        }

        public void MutateSegmentWithType(Point start, SegmentType targetType)
        {
            Segments[start.Row][start.Col].SegmentType = targetType;
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