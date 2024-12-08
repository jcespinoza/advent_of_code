using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day06Solver : Solver<char[][], long>
    {
        public Day06Solver() : base(2024, 06) { }

        public override char[][] ParseInput(IEnumerable<string> input)
            => input.Select(l => l.ToCharArray()).ToArray();


        public override long PartOne(char[][] map)
        {
            var start = GridWalker<char>.Find(map, '^');
            if (start.IsFailure)
            {
                throw new Exception("The starting point was not found");
            }

            HashSet<(int, int)> visited = PatrolMap(map, start.Value);

            return visited.Count;
        }

        private HashSet<(int, int)> PatrolMap(char[][] map, (int, int) start)
        {
            var (cRow, cCol) = start;
            Direction currentDirection = Direction.North;
            HashSet<(int, int)> visited = [start];
            (int, int) currentCell = start;
            do
            {
                var cellAhead = GridWalker<char>.Move(map, currentDirection, cRow, cCol);
                if (cellAhead.IsFailure)
                {
                    break;
                }

                char cellContent = map[cellAhead.Value.row][cellAhead.Value.col];
                bool isObstacle = cellContent == '#';
                if (isObstacle)
                {
                    currentDirection = currentDirection.Turn90();
                    continue;
                }
                (cRow, cCol) = cellAhead.Value;

                currentCell = cellAhead.Value;
                visited.Add(currentCell);
            } while (true);

            return visited;
        }

        public override long PartTwo(char[][] map)
        {
            var start = GridWalker<char>.Find(map, '^');
            if (start.IsFailure)
            {
                throw new Exception("The starting point was not found");
            }

            List<(int, int)> loopyObstacles = [];
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    if ((row, col) == start.Value)
                    {
                        // Skip the starting point
                        continue;
                    }
                    char cellChar = map[row][col];
                    if (cellChar == '#')
                    {
                        // Original configuration already has an obstacle here. Skip
                        continue;
                    }

                    // Temporarily replace the cell with an obstacle
                    map[row][col] = '#';

                    bool causesLoop = DoesObstacleCauseLoop(map, start.Value);
                    if (causesLoop)
                    {
                        loopyObstacles.Add((row, col));
                    }

                    // Restore the original cell content
                    map[row][col] = cellChar;
                }
            }

            return loopyObstacles.Count;
        }

        private bool DoesObstacleCauseLoop(char[][] map, (int, int) start)
        {
            var (cRow, cCol) = start;
            Direction currentDirection = Direction.North;
            HashSet<(int, int, Direction)> states = [];

            do
            {
                var cellAhead = GridWalker<char>.Move(map, currentDirection, cRow, cCol);
                if (cellAhead.IsFailure)
                {
                    break;
                }
                char cellContent = map[cellAhead.Value.row][cellAhead.Value.col];
                bool isObstacle = cellContent == '#';
                if (isObstacle)
                {
                    currentDirection = currentDirection.Turn90();
                    continue;
                }
                (cRow, cCol) = cellAhead.Value;

                var state = (cRow, cCol, currentDirection);
                if (states.Contains(state))
                {
                    return true;
                }
                states.Add(state);

            } while (true);

            return false;
        }
    }
}