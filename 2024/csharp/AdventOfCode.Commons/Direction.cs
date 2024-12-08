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
    }
}