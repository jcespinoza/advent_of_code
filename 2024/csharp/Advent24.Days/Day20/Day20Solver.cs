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

            var path = FindPath(map);

            long cheats = FindCheatsForPicosends(map, path, targetTimeSaved, useExact: false);

            return cheats;
        }

        public static long FindCheatsForPicosends(char[][] map, List<(int, int)> path, int targetTimeSaved, bool useExact)
        {
            int offset = 2;
            int count = 0;
            foreach ((int row, int col) in path) {
                foreach ((int nRow, int nCol) in new (int, int)[] {
                    (row - offset, col),
                    (row + offset, col),
                    (row, col - offset),
                    (row, col + offset)
                })
                {
                    if (nRow < 0 || nRow >= map.Length || nCol < 0 || nCol >= map[nRow].Length) continue;
                    if (map[nRow][nCol] == '#') continue;
                    if (map[nRow][nCol] == 'S') continue;

                    var distA = path.IndexOf((row, col));
                    var distB = path.IndexOf((nRow, nCol));
                    if (distA > distB) continue;
                    int localDist = Math.Abs(distA - distB);
                    bool savesEqualOrMoreSteps = localDist >= targetTimeSaved + offset;
                    bool savesEqualSteps = localDist == targetTimeSaved + offset;
                    bool shouldCount = useExact ? savesEqualSteps : savesEqualOrMoreSteps;

                    if (shouldCount)
                    {
                        count++;
                    }
                }
                
            }
            return count;
        }

        public static long FindAdvancedCheatsForPicosends(char[][] map, List<(int, int)> path, int targetTimeSaved, bool useExact)
        {
            int maxTime = 20;
            int count = 0;
            foreach ((int row, int col) in path)
            {
                if (map[row][col] == '#') continue;
                for (int offset = 2; offset <= maxTime; offset++)
                {
                    foreach ((int nRow, int nCol) in new (int, int)[] {
                        (row - offset, col),
                        (row + offset, col),
                        (row, col - offset),
                        (row, col + offset)
                    })
                    {
                        if (nRow < 0 || nRow >= map.Length || nCol < 0 || nCol >= map[nRow].Length) continue;
                        if (map[nRow][nCol] == '#') continue;
                        if (map[nRow][nCol] == 'S') continue;

                        var distA = path.IndexOf((row, col));
                        var distB = path.IndexOf((nRow, nCol));
                        if (distA > distB) continue;
                        int localDist = Math.Abs(distA - distB);
                        bool savesEqualOrMoreSteps = localDist >= targetTimeSaved + offset;
                        bool savesEqualSteps = localDist == targetTimeSaved + offset;
                        bool shouldCount = useExact ? savesEqualSteps : savesEqualOrMoreSteps;

                        if (shouldCount)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        private static void AddIfNotInList(HashSet<(int, int, int, int)> cheats, (int, int) start, (int, int) end)
        {
            (int sRow, int sCol) = start;
            (int eRow, int eCol) = end;
            if (cheats.Contains((sRow, sCol, eRow, eCol))) return;
            if (cheats.Contains((eRow, eCol, sRow, sCol))) return;

            cheats.Add((sRow, sCol, eRow, eCol));
        }

        public static List<(int, int)> FindPath(char[][] map)
        {
            // Starting at 'S' and ending at 'E', find all the (row, column) that are part of the path
            (int, int) start = FindInMap(map, 'S');
            (int, int) goal = FindInMap(map, 'E');

            List<(int, int)> path = [start];

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