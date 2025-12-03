

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

            int rowDiff = startRow - steps;
            int colSum = startCol + steps;
            int rowSum = startRow + steps;
            int colDiff = startCol - steps;
            switch (direction)
            {
                case Direction.North:
                    if (rowDiff < 0)
                    {
                        var result = Result<(int, int), string>.Failure("The resulting row is out of bounds");
                        result.Value = (rowDiff, startCol);
                        return result;
                    }
                    return Result<(int, int), string>.Success((rowDiff, startCol));
                case Direction.NorthEast:
                    if (rowDiff < 0 || colSum >= grid[startRow].Length)
                    {
                        Result<(int, int), string> result = Result<(int, int), string>.Failure("The resulting row is out of bounds");
                        result.Value = (rowDiff, colSum);
                        return result;
                    }
                    return Result<(int, int), string>.Success((rowDiff, colSum));
                case Direction.East:
                    if (colSum >= grid[startRow].Length)
                    {
                        Result<(int, int), string> result = Result<(int, int), string>.Failure("The resulting column is out of bounds");
                        result.Value = (startRow, colSum);
                        return result;
                    }
                    return Result<(int, int), string>.Success((startRow, colSum));
                case Direction.SouthEast:
                    if (rowSum >= grid.Length || colSum >= grid[startRow].Length)
                    {
                        Result<(int, int), string> result = Result<(int, int), string>.Failure("The resulting row is out of bounds");
                        result.Value = (rowSum, colSum);
                        return result;
                    }
                    return Result<(int, int), string>.Success((rowSum, colSum));
                case Direction.South:
                    if (rowSum >= grid.Length)
                    {
                        Result<(int, int), string> result = Result<(int, int), string>.Failure("The resulting row is out of bounds");
                        result.Value = (rowSum, startCol);
                        return result;
                    }
                    return Result<(int, int), string>.Success((rowSum, startCol));
                case Direction.SouthWest:
                    if (rowSum >= grid.Length || colDiff < 0)
                    {
                        Result<(int, int), string> result = Result<(int, int), string>.Failure("The resulting row is out of bounds");
                        result.Value = (rowSum, colDiff);
                        return result;
                    }
                    return Result<(int, int), string>.Success((rowSum, colDiff));
                case Direction.West:
                    if (colDiff < 0)
                    {
                        Result<(int, int), string> result = Result<(int, int), string>.Failure("The resulting column is out of bounds");
                        result.Value = (startRow, colDiff);
                        return result;
                    }
                    return Result<(int, int), string>.Success((startRow, colDiff));
                case Direction.NorthWest:
                    if (rowDiff < 0 || colDiff < 0)
                    {
                        Result<(int, int), string> result = Result<(int, int), string>.Failure("The resulting row is out of bounds");
                        result.Value = (rowDiff, colDiff);
                        return result;
                    }
                    return Result<(int, int), string>.Success((rowDiff, colDiff));
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