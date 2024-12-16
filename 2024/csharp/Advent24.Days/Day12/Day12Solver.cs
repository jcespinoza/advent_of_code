using AdventOfCode.Commons;
using System.Globalization;

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
                    while (queue.TryDequeue(out (int,int) qCell))
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
                        
                            if(regionCells.Contains((nRow, nCol))) continue;

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
            throw new NotImplementedException();
        }
    }
}