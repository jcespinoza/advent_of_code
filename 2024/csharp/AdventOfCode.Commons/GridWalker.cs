namespace AdventOfCode.Commons
{
    public static class GridWalker<T> where T: notnull
    {
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
    }
}