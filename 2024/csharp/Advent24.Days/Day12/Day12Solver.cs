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

            MapGarden(garden, regions, cellToRegion);
            //Dictionary<Region, List<(int, int)>> = MapRegionsToCells(cellToRegion);

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
            Dictionary<(int, int), Region> cellToRegion
        )
        {
            for (int row = 0; row < garden.Length; row++)
            {
                for (int col = 0; col < garden[row].Length; col++)
                {
                    char plantType = garden[row][col];
                    if (cellToRegion.ContainsKey((row, col)))
                    {
                        continue;
                    }

                    var results = CheckAllNeighbors(garden, regions, cellToRegion, row, col, plantType);
                    var neighboringRegion = results.FirstOrDefault(r => r.Region != null && r.Region.Plant == plantType)?.Region;

                    if(neighboringRegion != null)
                    {
                        neighboringRegion.Area++;
                        int neighboringPerimeter = results.Count(r => r.PlantType != plantType && r.IsInGarden || !r.IsInGarden);
                        neighboringRegion.Perimeter += neighboringPerimeter;
                        cellToRegion[(row, col)] = neighboringRegion;
                    }
                    else
                    {
                        var neighboringPerimeter = results
                            .Where(
                                r => r.IsInGarden && r.PlantType == plantType
                            )
                            .Count();

                        Region newRegion = new() { Plant = plantType, Area = 1, Perimeter = 4 - neighboringPerimeter };
                        regions.Add(newRegion);
                        cellToRegion[(row, col)] = newRegion;
                    }
                }
            }
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
                return new NeighborCheckResult(null, plantType, false);
            }

            (int nRow, int nCol) = neighborCell.Value;

            // check if the neighbor has a region
            if (cellToRegion.ContainsKey((nRow, nCol)))
            {
                Region neighborRegion = cellToRegion[(nRow, nCol)];
                return new NeighborCheckResult(neighborRegion, neighborRegion.Plant, true);
            }

            return new NeighborCheckResult(null, garden[nRow][nCol], true);            
        }

        public override long PartTwo(char[][] garden)
        {
            throw new NotImplementedException();
        }
    }

    public record NeighborCheckResult(Region? Region, char PlantType, bool IsInGarden);
}