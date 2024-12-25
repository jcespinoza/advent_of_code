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
            int targetTimeSaved = 100;
            (int, int) start = FindInMap(map, 'S');
            (int, int) goal = FindInMap(map, 'E');

            int[][] distances = InitDistancesMap(map);
            distances[start.Item1][start.Item2] = 0;

            (int cRow, int cCol) = start;

            while ((cRow, cCol) != goal)
            {
                foreach ((int nRow, int nCol) in new (int, int)[] {
                    (cRow + 1, cCol),
                    (cRow - 1, cCol),
                    (cRow, cCol + 1),
                    (cRow, cCol - 1),
                })
                {
                    if (nRow < 0 || nRow >= map.Length || nCol < 0 || nCol >= map[nRow].Length) continue;
                    if (map[nRow][nCol] == '#') continue;
                    if (distances[nRow][nCol] != -1) continue;
                    distances[nRow][nCol] = distances[cRow][cCol] + 1;
                    cRow = nRow;
                    cCol = nCol;
                }
            }

            int count = 0;
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    if (map[row][col] == '#') continue;
                    foreach ((int nRow, int nCol) in new (int, int)[] {
                        (row + 2, col),
                        (row + 1, col + 1),
                        (row, col + 2),
                        (row - 1, col + 1),
                        //(row - 2, col),
                        //(row - 1, col - 1),
                        //(row, col - 2),
                        //(row + 1, col - 1),
                    })
                    {
                        if (nRow < 0 || nRow >= map.Length || nCol < 0 || nCol >= map[nRow].Length) continue;
                        if (map[nRow][nCol] == '#') continue;
                        if (map[nRow][nCol] == 'S') continue;
                        int distA = distances[row][col];
                        int distB = distances[nRow][nCol];
                        
                        int localDist = Math.Abs(distA - distB);
                        if (localDist >= targetTimeSaved + 2)
                        {
                            count += 1;
                        }
                    }
                }
            }

            return count;
        }

        private static int[][] InitDistancesMap(char[][] map)
        {
            // Fill array with -1
            int[][] distances = new int[map.Length][];
            for (int i = 0; i < map.Length; i++)
            {
                distances[i] = new int[map[i].Length];
                for (int j = 0; j < map[i].Length; j++)
                {
                    distances[i][j] = -1;
                }
            }
            return distances;
        }

        public static long FindCheatsForPicosends(char[][] map, List<(int, int)> path, int targetTimeSaved)
        {
            int count = 0;
            foreach((int row, int col) in path) {
                if (map[row][col] == '#') continue;
                foreach ((int nRow, int nCol) in new (int, int)[] {
                    (row - 2, col),
                    (row + 2, col),
                    (row, col - 2),
                    (row, col + 2)
                })
                {
                    if (nRow < 0 || nRow >= map.Length || nCol < 0 || nCol >= map[nRow].Length) continue;
                    if (map[nRow][nCol] == '#') continue;
                    if (map[nRow][nCol] == 'S') continue;
                    var distA = path.IndexOf((row, col))+1;
                    var distB = path.IndexOf((nRow, nCol))+1;
                    if (distA > distB) continue;
                    int localDist = Math.Abs(distA - distB);
                    if (localDist >= targetTimeSaved + 2)
                    {
                        count++;
                    }
                }                
            }
            return count;
        }

        public static List<(int, int)> FindPath(char[][] map)
        {
            // Starting at 'S' and ending at 'E', find all the (row, column) that are part of the path
            (int, int) start = FindInMap(map, 'S');
            (int, int) goal = FindInMap(map, 'E');

            List<(int, int)> path = [];

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