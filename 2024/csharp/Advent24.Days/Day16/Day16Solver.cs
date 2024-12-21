using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day16Solver : Solver<char[][], long>
    {
        public Day16Solver() : base(2024, 16) { }

        public override char[][] ParseInput(IEnumerable<string> input)
            => input.Select(l => l.ToCharArray()).ToArray();

        public override long PartOne(char[][] map)
        {
            (int, int) startLocation = FindInMap(map, 'S');
            (int, int) goalLocation = FindInMap(map, 'E');

            PathResult path = ComputeLowestScorePath(map, startLocation, goalLocation);

            long lowestScode = path.LowestScore;

            return lowestScode;
        }

        private (int, int) FindInMap(char[][] map, char targetChar)
        {
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    if(map[row][col] == targetChar)
                    {
                        return (row, col);
                    }
                }
            }
            return (-1, -1);
        }

        private PathResult ComputeLowestScorePath(char[][] map, (int, int) startLocation, (int, int) goalLocation)
        {
            PriorityQueue<(int cost, int row, int col, Direction dir), int> pQueue = new();
            HashSet<(int cost, int row, int col, Direction dir)> visited = [];

            int lowestScore = -1;
            (int sRow, int sCol) = startLocation;
            pQueue.Enqueue((0, sRow, sCol, Direction.East), 0);
            visited.Add((0, sRow, sCol, Direction.East));
            while (pQueue.Count > 0)
            {
                (int cCost, int cRow, int cCol, Direction cDir) = pQueue.Dequeue();
                visited.Add((cCost, cRow, cCol, cDir));

                if((cRow, cCol) == goalLocation)
                {
                    lowestScore = cCost;
                    break;
                }

                foreach (var direction in new Direction[] {Direction.North, Direction.East, Direction.South, Direction.West })
                {
                    if(cDir.IsOpposite(direction)) continue;

                    (int nCost, int nRow, int nCol, Direction nDir) = direction switch {
                        Direction.North => (cCost + GetCost(cDir, Direction.North), cRow - 1, cCol, Direction.North),
                        Direction.South => (cCost + GetCost(cDir, Direction.South), cRow + 1, cCol, Direction.South),
                        Direction.West => (cCost + GetCost(cDir, Direction.West), cRow, cCol - 1, Direction.West),
                        Direction.East => (cCost + GetCost(cDir, Direction.East), cRow, cCol + 1, Direction.East),
                        _ => throw new InvalidOperationException()
                    };

                    char cellValue = map[nRow][nCol];
                    if (cellValue == '#') continue;

                    if (visited.Contains((nCost, nRow, nCol, nDir))) continue;

                    pQueue.Enqueue((nCost, nRow, nCol, nDir), nCost);
                }
            }

            return new PathResult { LowestScore = lowestScore };
        }

        private static int GetCost(Direction cDirection, Direction nDirection)
        {
            if (cDirection.IsPerpedicular(nDirection)) return 1000;

            return 1;
        }

        public override long PartTwo(char[][] map)
        {
            throw new NotImplementedException();
        }
    }
}