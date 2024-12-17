namespace AdventOfCode.Commons
{
    public enum Direction
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    }

    public static class DirectionExtensions
    {
        public static Direction Turn90(this Direction direction)
        {
            return direction.RotateOrtogonal(1);
        }

        public static Direction RotateOrtogonal(this Direction direction, int steps)
        {
            var directions = new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West };
            var index = Array.IndexOf(directions, direction);
            index = (index + steps) % directions.Length;
            if (index < 0)
            {
                index += directions.Length;
            }
            return directions[index];
        }

        public static int RowOffset(this Direction direction)
        {
            return direction switch
            {
                Direction.North => -1,
                Direction.South => 1,
                _ => 0,
            };
        }

        public static int ColOffset(this Direction direction)
        {
            return direction switch
            {
                Direction.East => 1,
                Direction.West => -1,
                _ => 0,
            };
        }
    }
}