using AdventOfCode.Commons;
using System.Globalization;
using System.Reflection.Metadata;

namespace Advent24.Days
{
    public class Day12Solver : Solver<char[][], long>
    {
        public Day12Solver() : base(2024, 12) { }

        public override char[][] ParseInput(IEnumerable<string> input)
            => input.Select(line => line.ToCharArray()).ToArray();


        public override long PartOne(char[][] garden)
        {
            Dictionary<Region, HashSet<(int, int)>> regionToCells = MapRegions(garden);

            long totalPrice = 0;
            foreach (var regionKey in regionToCells)
            {
                var region = regionKey.Key;
                long regionPrice = region.Area * region.Perimeter;
                totalPrice += regionPrice;
            }

            return totalPrice;
        }

        private static Dictionary<Region, HashSet<(int, int)>> MapRegions(char[][] garden)
        {
            Dictionary<Region, HashSet<(int, int)>> regionToCells = [];
            HashSet<(int, int)> seen = [];

            for (int row = 0; row < garden.Length; row++)
            {
                for (int col = 0; col < garden[row].Length; col++)
                {
                    if (seen.Contains((row, col))) continue;
                    seen.Add((row, col));
                    HashSet<(int, int)> regionCells = [];
                    regionCells.Add((row, col));
                    Queue<(int, int)> queue = [];
                    queue.Enqueue((row, col));
                    char plantType = garden[row][col];
                    int perimeter = 0;
                    while (queue.TryDequeue(out (int, int) qCell))
                    {
                        (int cRow, int cCol) = qCell;
                        IEnumerable<Direction> directions = [Direction.North, Direction.East, Direction.South, Direction.West];
                        foreach (var direction in directions)
                        {
                            var cellResult = GridWalker<char>.Move(garden, direction, cRow, cCol);
                            if (cellResult.IsFailure)
                            {
                                perimeter++;
                                continue;
                            }
                            (int nRow, int nCol) = cellResult.Value;

                            char nPlantType = garden[nRow][nCol];
                            if (nPlantType != plantType)
                            {
                                perimeter++;
                                continue;
                            }

                            if (regionCells.Contains((nRow, nCol))) continue;

                            regionCells.Add((nRow, nCol));
                            queue.Enqueue((nRow, nCol));
                            seen.Add((nRow, nCol));
                        }
                    }

                    Region newRegion = new() { Plant = plantType, Area = regionCells.Count, Perimeter = perimeter };
                    regionToCells.Add(newRegion, regionCells);
                }
            }

            return regionToCells;
        }

        public override long PartTwo(char[][] garden)
        {
            Dictionary<Region, HashSet<(int, int)>> regionToCells = MapRegions(garden);


            long totalPrice = 0;
            foreach (var regionEntry in regionToCells)
            {
                var region = regionEntry.Key;
                //region.Sides = BAD_GETSIDECOUT(garden, regionEntry.Value);
                region.Sides = CalculateSides(regionEntry.Value);
                long regionPrice = region.Area * region.Sides;
                totalPrice += regionPrice;
            }

            return totalPrice;
        }

        [Obsolete]
        private int BAD_GETSIDECOUT(char[][] garden, HashSet<(int, int)> region)
        {
            Dictionary<(double, double), (double, double)> edges = [];

            foreach (var cell in region)
            {
                (int cRow, int cCol) = cell;
                IEnumerable<Direction> directions = [Direction.North, Direction.East, Direction.South, Direction.West];
                foreach (var direction in directions)
                {
                    var cellResult = GridWalker<char>.Move(garden, direction, cRow, cCol);
                        
                    (int nRow, int nCol) = cellResult.Value;

                    if (region.Contains((nRow, nCol))) continue;
                    // Use half-cartesian coordinates to describe edges and facing direction towards the current cell
                    double edgeRow = (cRow + nRow) / 2.0;
                    double edgeCol = (cCol + nCol) / 2.0;
                    if (!edges.ContainsKey((edgeRow, edgeCol)))
                    {
                        edges.Add((edgeRow, edgeCol), (edgeRow - cRow, edgeCol - cCol));
                    }
                }
            }
            int sideCount = 0;
            HashSet<(double, double)> seen = [];
            foreach (var edge in edges)
            {
                (double eRow, double eCol) = edge.Key;
                (double dirRow, double dirCol) = edge.Value;

                if (seen.Contains((eRow, eCol))) continue;
                seen.Add((eRow, eCol));
                sideCount++;

                if (eRow % 1 == 0)
                {
                    foreach (var diffRow in new int[] { -1, 1 })
                    {
                        double curRow = eRow + diffRow;
                        (double, double) nextEdge = (curRow, eCol);
                        while (edges.TryGetValue((curRow, eCol), out (double, double) nextDir) && nextDir == edge.Value)
                        {
                            seen.Add(nextEdge);
                            curRow += diffRow;
                        }
                    }
                }
                else
                {
                    foreach (var diffCol in new int[] { -1, 1 })
                    {
                        double curCol = eCol + diffCol;
                        (double, double) nextEdge = (eRow, curCol);
                        while (edges.TryGetValue((eRow, curCol), out (double, double) nextDir) && nextDir == edge.Value)
                        {
                            seen.Add(nextEdge);
                            curCol += diffCol;
                        }
                    }

                }
            }
            return sideCount;
        }

        public int CalculateSides(HashSet<(int, int)> region)
        {
            var edges = new Dictionary<(double, double), (double, double)>();

            foreach (var cell in region)
            {
                (int cRow, int cCol) = cell;
                var neighbors = new (int, int)[] { (cRow + 1, cCol), (cRow - 1, cCol), (cRow, cCol + 1), (cRow, cCol - 1) };

                foreach (var neighbor in neighbors)
                {
                    (int nRow, int nCol) = neighbor;
                    if (region.Contains((nRow, nCol))) continue;

                    double avgRow = (cRow + nRow) / 2.0;
                    double avgCol = (cCol + nCol) / 2.0;
                    double diffRow = avgRow - cRow;
                    double diffCol = avgCol - cCol;
                    edges[(avgRow, avgCol)] = (diffRow, diffCol);
                }
            }

            var visited = new HashSet<(double, double)>();
            int sideCount = 0;

            foreach (var edge in edges)
            {
                (double edgeRow, double edgeCol) = edge.Key;
                (double dirRow, double dirCol) = edge.Value;

                if (visited.Contains((edgeRow, edgeCol))) continue;
                visited.Add((edgeRow, edgeCol));
                sideCount++;

                if (edgeRow % 1 == 0)
                {
                    foreach (var deltaRow in new int[] { -1, 1 })
                    {
                        double curRow = edgeRow + deltaRow;
                        while (edges.TryGetValue((curRow, edgeCol), out var direction) && direction == edge.Value)
                        {
                            visited.Add((curRow, edgeCol));
                            curRow += deltaRow;
                        }
                    }
                }
                else
                {
                    foreach (var deltaCol in new int[] { -1, 1 })
                    {
                        double curCol = edgeCol + deltaCol;
                        while (edges.TryGetValue((edgeRow, curCol), out var direction) && direction == edge.Value)
                        {
                            visited.Add((edgeRow, curCol));
                            curCol += deltaCol;
                        }
                    }
                }
            }

            return sideCount;
        }
    }
}