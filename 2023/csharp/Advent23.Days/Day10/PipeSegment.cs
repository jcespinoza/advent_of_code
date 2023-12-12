namespace Advent23.Days.Day10
{
    public record PipeSegment
    {
        public required int RowIndex { get; init;  }
        public required int ColIndex { get; init;}
        public required SegmentType SegmentType { get; init; }
        public bool Visited { get; set; }

        public (Direction primary, Direction secondary) GetDirections()
        {
            switch (SegmentType)
            {
                case SegmentType.Start:
                    return (Direction.None, Direction.None);
                case SegmentType.Ground:
                    return (Direction.None, Direction.None);
                case SegmentType.LineNS:
                    return (Direction.North, Direction.South);
                case SegmentType.LineEW:
                    return (Direction.East, Direction.West);
                case SegmentType.AngleNE:
                    return (Direction.North, Direction.East);
                case SegmentType.AngleNW:
                    return (Direction.North, Direction.West);
                case SegmentType.AngleSW:
                    return (Direction.South, Direction.West);
                case SegmentType.AngleSE:
                    return (Direction.South, Direction.East);
                default:
                    return (Direction.None, Direction.None);
            }
        }

        public static SegmentType ParseType(char c)
        {
            switch (c)
            {
                case 'S':
                    return SegmentType.Start;
                case '.':
                    return SegmentType.Ground;
                case '|':
                    return SegmentType.LineNS;
                case '-':
                    return SegmentType.LineEW;
                case 'L':
                    return SegmentType.AngleNE;
                case 'J':
                    return SegmentType.AngleNW;
                case '7':
                    return SegmentType.AngleSW;
                case 'F':
                    return SegmentType.AngleSE;

                default:
                    throw new NotImplementedException("Not a valid segment");
            }
        }

        public bool AcceptsVisit(Direction srcDirection)
        {
            switch (srcDirection)
            {
                case Direction.North:
                    return SegmentType == SegmentType.LineNS
                        || SegmentType == SegmentType.AngleNE
                        || SegmentType == SegmentType.AngleNW;
                case Direction.East:
                    return SegmentType == SegmentType.LineEW
                        || SegmentType == SegmentType.AngleNE
                        || SegmentType == SegmentType.AngleSE;
                case Direction.South:
                    return SegmentType == SegmentType.LineNS
                        || SegmentType == SegmentType.AngleSW
                        || SegmentType == SegmentType.AngleSE;
                case Direction.West:
                    return SegmentType == SegmentType.LineEW
                        || SegmentType == SegmentType.AngleSW
                        || SegmentType == SegmentType.AngleNW;
                case Direction.None:
                default:
                    throw new Exception("Invalid visit direction");
            }
        }

        public bool IsTraversable => SegmentType != SegmentType.Start
                                    && SegmentType != SegmentType.Ground;        
    }

    public enum SegmentType
    {
        Start,
        Ground,
        LineNS,
        LineEW,
        AngleNE,
        AngleNW,
        AngleSW,
        AngleSE,
    }
}