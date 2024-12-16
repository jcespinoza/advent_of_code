using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day12Solver : Solver<char[][], long>
    {
        public Day12Solver() : base(2024, 12) { }

        public override char[][] ParseInput(IEnumerable<string> input)
            => input.Select(line => line.ToCharArray()).ToArray();


        public override long PartOne(char[][] garden)
        {
            HashSet<Region> regions = [];
            Dictionary<(int, int), Region> cellToRegion = [];

            Dictionary<Region, HashSet<(int, int)>> regionToCells = [];
            MapGarden(garden, regions, cellToRegion, regionToCells);

            ComputeRegionDimensions(garden, regionToCells);

            long totalPrice = 0;
            foreach (var region in regions)
            {
                long regionPrice = region.Area * region.Perimeter;
                totalPrice += regionPrice;
            }

            return totalPrice;
        }

        private static void MapGarden(
            char[][] garden, HashSet<Region> regions, 
            Dictionary<(int, int), Region> cellToRegion,
            Dictionary<Region, HashSet<(int, int)>> regionToCells
        )
        {
            HashSet<(int, int)> visited = [];

            for (int row = 0; row < garden.Length; row++)
            {
                for (int col = 0; col < garden[row].Length; col++)
                {
                    if(visited.Contains((row, col))) continue;
                    visited.Add((row, col));
                    HashSet<(int, int)> cellsInRegion = [];
                    Queue<(int, int)> toVisit = new([(row,col)]);

                    char cPlantType = garden[row][col];
                    Region region = new() { Plant = cPlantType };
                    regionToCells.Add(region, cellsInRegion);
                    while (toVisit.TryDequeue(out (int, int) qCell))
                    {
                        (int cRow, int cCol) = qCell;
                        IEnumerable<Direction> directions = [Direction.North, Direction.East, Direction.South, Direction.West];
                        foreach (var direction in directions)
                        {
                            var cellResult = GridWalker<char>.Move(garden, direction, cRow, cCol);
                            if (cellResult.IsFailure)
                            {
                                continue;
                            }
                            (int nRow, int nCol) = cellResult.Value;

                            char nPlantType = garden[nRow][nCol];
                            if (nPlantType != cPlantType) continue;
                            if(cellsInRegion.Contains((nRow, nCol))) continue;
                            cellsInRegion.Add((nRow, nCol));
                            regionToCells[region].Add((nRow, nCol));
                            visited.Add((nRow, nCol));
                            toVisit.Enqueue((nRow, nCol));
                        }
                    }
                    regions.Add(region);
                }
            }
        }

        private static void ComputeRegionDimensions(char[][] garden, Dictionary<Region, HashSet<(int, int)>> regionToCells)
        {
            foreach (var regionEntry in regionToCells)
            {
                var region = regionEntry.Key;
                var cells = regionEntry.Value;

                int perimeter = 0;
                foreach (var (row, col) in cells)
                {
                    int bordersWithOtherPlants = ComputeBordersWithOtherPlants(garden, row, col, region.Plant);
                    perimeter += bordersWithOtherPlants;
                }
                region.Area = cells.Count;
                region.Perimeter = perimeter;
            }
        }

        private static int ComputeBordersWithOtherPlants(char[][] garden, int row, int col, char plant)
        {
            int borders = 0;
            IEnumerable<Direction> directions = [Direction.North, Direction.East, Direction.South, Direction.West];
            foreach (var direction in directions)
            {
                var cellResult = GridWalker<char>.Move(garden, direction, row, col);
                if (cellResult.IsFailure) {
                    // Cell is outside of grid, therefore is a border
                    borders++;
                    continue;
                }
                (int nRow, int nCol) = cellResult.Value;

                if (garden[nRow][nCol] != plant)
                {
                    borders++;
                }
            }

            return borders;
        }

        public override long PartTwo(char[][] garden)
        {
            throw new NotImplementedException();
        }
    }
}