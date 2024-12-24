using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day18Solver : SteppedSolver<(int,int)[], (int, int)[], long, (int,int)>
    {
        public Day18Solver() : base(2024, 18) { }

        public override (int,int)[] ParseInputOne(IEnumerable<string> input)
            => input
            .Select(l => l.Split(','))
            .Select(l =>
                (
                    int.Parse(l[0]),
                    int.Parse(l[1])
                )
            )
            .ToArray();

        public override (int, int)[] ParseInputTwo(IEnumerable<string> input) => ParseInputOne(input);

        public override long PartOne((int,int)[] byteLocations)
        {
            int gridSize = 70;
            int bytesToSimulate = 1024;
            if (byteLocations.Length < 500)
            {
                // This is an example and not the real puzzle
                gridSize = 6;
                bytesToSimulate = 12;
            }

            long stepsToExit = ComputeStepsToExit(gridSize, byteLocations, bytesToSimulate);

            return stepsToExit;
        }

        private static char[][] GetMap(int gridSize)
        {
            var map = new char[gridSize][];
            for (int i = 0; i < gridSize; i++)
            {
                map[i] = new char[gridSize];
            }
            return map;
        }

        private static long ComputeStepsToExit(int gridSize, (int, int)[] byteLocations, int bytesToSimulate)
        {
            char[][] map = GetMap(gridSize + 1);
            (int, int) exitLocation = (map.Length - 1, map.Length - 1);
            for(int index = 0; index < bytesToSimulate; index++)
            {
                (int x, int y) = byteLocations[index];
                map[y][x] = '#';
            }

            Queue<(int, int, int)> cellsToVisit = [];
            cellsToVisit.Enqueue((0, 0, 0));
            HashSet<(int, int)> visited = [];

            while (cellsToVisit.Count > 0)
            {
                (int cRow, int cCol, int cSteps) = cellsToVisit.Dequeue();

                foreach (var nLocation in new (int, int)[] {
                    (cRow + 1, cCol),
                    (cRow - 1, cCol),
                    (cRow, cCol + 1),
                    (cRow, cCol - 1)
                })
                {
                    (int nRow, int nCol) = nLocation;
                    if (nRow < 0 || nRow >= map.Length || nCol < 0 || nCol >= map[0].Length)
                    {
                        continue;
                    }

                    if (map[nRow][nCol] == '#')
                    {
                        continue;
                    }

                    if (visited.Contains((nRow, nCol)))
                    {
                        continue;
                    }

                    visited.Add((nRow, nCol));
                    if ((nRow, nCol) == exitLocation)
                    {
                        return cSteps + 1;
                    }

                    visited.Add((nRow, nCol));
                    cellsToVisit.Enqueue((nRow, nCol, cSteps + 1));
                }
            }

            return long.MaxValue;
        }

        public override (int,int) PartTwo((int,int)[] byteLocations)
        {
            int gridSize = 70;

            if (byteLocations.Length < 500)
            {
                // This is an example and not the real puzzle
                gridSize = 6;
            }

            return FindFirstBlockage(byteLocations, gridSize);
        }

        private static (int, int) FindFirstBlockage((int, int)[] byteLocations, int gridSize)
        {
            int minIndex = 0;
            int maxIndex = byteLocations.Length - 1;

            // Narrow down search
            while (minIndex < maxIndex)
            {
                // Look at the mid point
                var mid = (minIndex + maxIndex) / 2;
                long stepsToExit = ComputeStepsToExit(gridSize, byteLocations, mid + 1);
                if (stepsToExit != long.MaxValue)
                {
                // If there's a path to the exit, set the midpoint as the new lower bound
                    minIndex = mid + 1;
                }
                else
                {
                    // Otherwise, make the midpoint the higher boundary
                    maxIndex = mid;
                }
            }

            return byteLocations[minIndex];
        }
    }
}