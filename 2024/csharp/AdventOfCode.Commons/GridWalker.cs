namespace AdventOfCode.Commons
{
    public static class GridWalker<T> where T: notnull
    {
        public static Result<(int row, int col),string> Move(T[][] grid, Direction direction, int startRow, int startCol)
        {
            // ensure the starting cell is within the grid
            if (startRow < 0 || startRow >= grid.Length || startCol < 0 || startCol >= grid[startRow].Length)
            {
                return Result<(int,int), string>.Failure("The starting cell is out of bounds");
            }

            switch (direction)
            {
                case Direction.North:
                    if (startRow - 1 < 0)
                    {
                        return Result<(int,int),string>.Failure("The resulting row is out of bounds");
                    }
                    return Result<(int, int), string>.Success((startRow - 1, startCol));
                case Direction.NorthEast:
                    if (startRow - 1 < 0 || startCol + 1 >= grid[startRow].Length)
                    {
                        return Result<(int,int), string>.Failure("The resulting row is out of bounds");
                    }
                    return Result<(int,int),string>.Success((startRow - 1, startCol + 1));
                case Direction.East:
                    if (startCol + 1 >= grid[startRow].Length)
                    {
                        return Result<(int, int), string>.Failure("The resulting column is out of bounds");
                    }
                    return Result<(int,int),string>.Success((startRow, startCol + 1));
                case Direction.SouthEast:
                    if (startRow + 1 >= grid.Length || startCol + 1 >= grid[startRow].Length)
                    {
                        return Result<(int,int), string>.Failure("The resulting row is out of bounds");
                    }
                    return Result<(int,int),string>.Success((startRow + 1, startCol + 1));
                case Direction.South:
                    if (startRow + 1 >= grid.Length)
                    {
                        return Result<(int,int), string>.Failure("The resulting row is out of bounds");
                    }
                    return Result<(int,int),string>.Success((startRow + 1, startCol));
                case Direction.SouthWest:
                    if (startRow + 1 >= grid.Length || startCol - 1 < 0)
                    {
                        return Result<(int,int), string>.Failure("The resulting row is out of bounds");
                    }
                    return Result<(int,int),string>.Success((startRow + 1, startCol - 1));
                case Direction.West:
                    if (startCol - 1 < 0)
                    {
                        return Result<(int,int), string>.Failure("The resulting column is out of bounds");
                    }
                    return Result<(int,int),string>.Success((startRow, startCol - 1));
                case Direction.NorthWest:
                    if (startRow - 1 < 0 || startCol - 1 < 0)
                    {
                        return Result<(int,int), string>.Failure("The resulting row is out of bounds");
                    }
                    return Result<(int,int),string>.Success((startRow - 1, startCol - 1));
                default:
                    return Result<(int, int), string>.Failure("Invalid direction specified");
            }
        }
    }
}