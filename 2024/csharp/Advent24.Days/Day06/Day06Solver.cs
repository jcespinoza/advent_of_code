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
            }while(true);

            return visited;
        }

        public override long PartTwo(char[][] input)
        {
            throw new NotImplementedException();
        }
    }
}