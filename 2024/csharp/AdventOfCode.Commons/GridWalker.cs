

namespace AdventOfCode.Commons
{
    public static class GridWalker<T> where T: IEquatable<T>
    {
        private static readonly double COLINEARITY_TOLERANCE = 0.0001;
        public static int FindDistance((int,int) source, (int,int) target)
        {
            if (source == target) return 0;

            (int sRow, int sCol) = source;
            (int tRow, int tCol) = target;

            int manhattanDistance = Math.Abs(sRow - tRow) + Math.Abs(sCol - tCol);

            return manhattanDistance;
        }

        public static Result<(int row, int col), string> Find(char[][] map, char searchValue)
        {
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    if (map[row][col] == searchValue)
                    {
                        return Result<(int,int),string>.Success( (row, col));
                    }
                }
            }
            return Result<(int, int), string>.Failure("The search value was not found in the grid");
        }

        public static Result<(int row, int col), string> Move(T[][] grid, Direction direction, int startRow, int startCol)
        {
            return Move(grid, direction, startRow, startCol, 1);
        }

        public static Result<(int row, int col), string> Move(T[][] grid, Direction direction, int startRow, int startCol, int steps)
        {
            // ensure the starting cell is within the grid
            if (startRow < 0 || startRow >= grid.Length || startCol < 0 || startCol >= grid[startRow].Length)
            {
                return Result<(int,int), string>.Failure("The starting cell is out of bounds");
            }

            switch (direction)
            {
                case Direction.North:
                    if (startRow - steps < 0)
                    {
                        return Result<(int, int), string>.Failure("The resulting row is out of bounds");
                    }
                    return Result<(int, int), string>.Success((startRow - steps, startCol));
                case Direction.NorthEast:
                    if (startRow - steps < 0 || startCol + steps >= grid[startRow].Length)
                    {
                        return Result<(int, int), string>.Failure("The resulting row is out of bounds");
                    }
                    return Result<(int, int), string>.Success((startRow - steps, startCol + steps));
                case Direction.East:
                    if (startCol + steps >= grid[startRow].Length)
                    {
                        return Result<(int, int), string>.Failure("The resulting column is out of bounds");
                    }
                    return Result<(int, int), string>.Success((startRow, startCol + steps));
                case Direction.SouthEast:
                    if (startRow + steps >= grid.Length || startCol + steps >= grid[startRow].Length)
                    {
                        return Result<(int, int), string>.Failure("The resulting row is out of bounds");
                    }
                    return Result<(int, int), string>.Success((startRow + steps, startCol + steps));
                case Direction.South:
                    if (startRow + steps >= grid.Length)
                    {
                        return Result<(int, int), string>.Failure("The resulting row is out of bounds");
                    }
                    return Result<(int, int), string>.Success((startRow + steps, startCol));
                case Direction.SouthWest:
                    if (startRow + steps >= grid.Length || startCol - steps < 0)
                    {
                        return Result<(int, int), string>.Failure("The resulting row is out of bounds");
                    }
                    return Result<(int, int), string>.Success((startRow + steps, startCol - steps));
                case Direction.West:
                    if (startCol - steps < 0)
                    {
                        return Result<(int, int), string>.Failure("The resulting column is out of bounds");
                    }
                    return Result<(int, int), string>.Success((startRow, startCol - steps));
                case Direction.NorthWest:
                    if (startRow - steps < 0 || startCol - steps < 0)
                    {
                        return Result<(int, int), string>.Failure("The resulting row is out of bounds");
                    }
                    return Result<(int, int), string>.Success((startRow - steps, startCol - steps));
                default:
                    return Result<(int, int), string>.Failure("Invalid direction specified");
            }
        }

        public static bool ArePointsColinear((int, int) pointA, (int, int) pointB, (int, int) pointC, int distanceAB, int distanceBC)
        {
            if (distanceAB == 0 || distanceBC == 0) return false;

            double slopeAB = (pointB.Item2 - pointA.Item2) / (double)(pointB.Item1 - pointA.Item1);
            double slopeBC = (pointC.Item2 - pointB.Item2) / (double)(pointC.Item1 - pointB.Item1);
            return Math.Abs(slopeAB - slopeBC) < COLINEARITY_TOLERANCE;
        }

        public static bool IsPointInGrid(char[][] map, int antiRow1, int antiCol1)
        {
            bool rowIsWithinBounds = antiRow1 >= 0 && antiRow1 < map.Length;
            bool columnIsWithinBounds = antiCol1 >= 0 && antiCol1 < map[0].Length;

            return rowIsWithinBounds && columnIsWithinBounds;
        }
    }
}