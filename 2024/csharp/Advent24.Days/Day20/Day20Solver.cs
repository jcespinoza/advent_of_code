using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day20Solver : SteppedSolver<char[][], char[][], long, long>
    {
        public Day20Solver() : base(2024, 20) { }

        public override char[][] ParseInputOne(IEnumerable<string> input)
            => input
            .Select(l => l.ToCharArray())
            .ToArray();
        
        public override char[][] ParseInputTwo(IEnumerable<string> input)
            => ParseInputOne(input);

        public override long PartOne(char[][] map)
        {
            List<(int, int)> path = FindPath(map);

            long cheatsForPicoseconds = FindCheatsForPicosends(map, path, targetTimeSaved: 100);
             
            return cheatsForPicoseconds;
        }

        private static long FindCheatsForPicosends(char[][] map, List<(int, int)> path, int targetTimeSaved)
        {
            throw new NotImplementedException();
        }

        public static List<(int, int)> FindPath(char[][] map)
        {
            // Starting at 'S' and ending at 'E', find all the (row, column) that are part of the path
            (int, int) start = FindInMap(map, 'S');
            (int, int) goal = FindInMap(map, 'E');

            List<(int, int)> path = new();

            (int cRow, int cCol) = start;
            while ((cRow, cCol) != goal)
            {
                // Find the next step
                (int nRow, int nCol) = FindNextStep(map, cRow, cCol, path);
                path.Add((nRow, nCol));
                (cRow, cCol) = (nRow, nCol);
            }
            return path;
        }

        private static (int nRow, int nCol) FindNextStep(char[][] map, int cRow, int cCol, List<(int, int)> path)
        {
            foreach ((int nRow, int nCol) in new (int,int)[]{
                (cRow - 1, cCol),
                (cRow + 1, cCol),
                (cRow, cCol - 1),
                (cRow, cCol + 1)
            })
            {
                char cellValue = map[nRow][nCol];
                if (path.Contains((nRow, nCol))) continue;
                if (cellValue == '.' || cellValue == 'E')
                {
                    return (nRow, nCol);
                }
            }
            return (-1, -1);
        }

        private static (int, int) FindInMap(char[][] map, char v)
        {
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    if (map[row][col] == v)
                    {
                        return (row, col);
                    }
                }
            }
            return (-1, -1);
        }

        public override long PartTwo(char[][] input)
        {
            throw new NotImplementedException();
        }
    }
}