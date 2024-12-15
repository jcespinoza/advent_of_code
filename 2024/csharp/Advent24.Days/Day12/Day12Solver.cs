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
                    if (visited.Contains((row, col))) continue;

                    char plantType = garden[row][col];
                    Region region = new() { Plant = plantType };
                    regions.Add(region);
                    regionToCells[region] = [(row,col)];
                    cellToRegion.Add((row, col), region);
                    PropagateRegion(garden, regions, cellToRegion, regionToCells, visited, row, col, plantType, region);
                }
            }
        }

        private static void PropagateRegion(char[][] garden, HashSet<Region> regions,
            Dictionary<(int, int), Region> cellToRegion,
            Dictionary<Region, HashSet<(int, int)>> regionToCells, HashSet<(int, int)> visited,
            int row, int col, char plantType, Region region
        )
        {
            if(visited.Contains((row, col))) return;

            visited.Add((row, col));

            IEnumerable<Direction> directions = [Direction.East, Direction.South, Direction.West];
            foreach (var direction in directions)
            {
                var cellResult = GridWalker<char>.Move(garden, direction, row, col);
                if (cellResult.IsFailure)
                {
                    continue;
                }
                (int nRow, int nCol) = cellResult.Value;

                if (garden[nRow][nCol] != plantType)
                {
                    continue;
                }

                if (!cellToRegion.ContainsKey((nRow, nCol)))
                {
                    cellToRegion.Add((nRow, nCol), region);
                }
                regionToCells[region].Add((nRow, nCol));
                PropagateRegion(garden, regions, cellToRegion, regionToCells, visited, nRow, nCol, plantType, region);
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

        private static List<NeighborCheckResult> CheckAllNeighbors(
            char[][] garden, 
            HashSet<Region> regions, 
            Dictionary<(int, int), Region> cellToRegion, 
            int row, int col, 
            char plantType
        )
        {
            var northCheck = CheckNeighbor(garden, regions, cellToRegion, Direction.North, row, col, plantType);
            var eastCheck = CheckNeighbor(garden, regions, cellToRegion, Direction.East, row, col, plantType);
            var southCheck = CheckNeighbor(garden, regions, cellToRegion, Direction.South, row, col, plantType);
            var westCheck = CheckNeighbor(garden, regions, cellToRegion, Direction.West, row, col, plantType);

            return [northCheck, eastCheck, southCheck, westCheck];
        }

        private static NeighborCheckResult CheckNeighbor(
            char[][] garden, 
            HashSet<Region> regions, 
            Dictionary<(int, int), Region> cellToRegion, 
            Direction north, 
            int sRow, int sCol,
            char plantType
        )
        {
            Result<(int,int), string> neighborCell = GridWalker<char>.Move(garden, north, sRow, sCol);
            if (neighborCell.IsFailure)
            {
                return new NeighborCheckResult(-1, -1, null, plantType, false);
            }

            (int nRow, int nCol) = neighborCell.Value;

            // check if the neighbor has a region
            if (cellToRegion.ContainsKey((nRow, nCol)))
            {
                Region neighborRegion = cellToRegion[(nRow, nCol)];
                return new NeighborCheckResult(nRow, nCol, neighborRegion, neighborRegion.Plant, true);
            }

            return new NeighborCheckResult(nRow, nCol, null, garden[nRow][nCol], true);            
        }

        public override long PartTwo(char[][] garden)
        {
            throw new NotImplementedException();
        }
    }

    public record NeighborCheckResult(int Row, int Col, Region? Region, char PlantType, bool IsInGarden);
}