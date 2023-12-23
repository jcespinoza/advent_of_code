
namespace Advent23.Days.Day11
{
    public record SpaceMap
    {
        public required List<Galaxy> Galaxies { get; init; }
        public required List<int> EmptyRows { get; init; }
        public required List<int> EmptyCols { get; init; }

        public static SpaceMap Parse(string[] input)
        {            
            List<Galaxy> galaxies = [];
            int galaxyCounter = 1;
            for (int row = 0; row < input.Length; row++)
            {
                for (int col = 0; col < input[row].Length; col++)
                {
                    char symbol = input[row][col];
                    if (symbol == '#')
                    {
                        var galaxy = new Galaxy { Code = galaxyCounter, Row = row, Col = col };
                        galaxies.Add(galaxy);
                        galaxyCounter++;
                    }
                }
            }
            List<int> emptyRows = GetEmptyRows(input);
            List<int> emptyCols = GetEmptyCols(input);
            var spaceMap = new SpaceMap()
            {
                Galaxies = galaxies,
                EmptyRows = emptyRows,
                EmptyCols = emptyCols
            };

            return spaceMap;
        }

        private static List<int> GetEmptyCols(string[] input)
        {
            var emptyCols = new List<int>();
            for (int col = 0; col < input[0].Length; col++)
            {
                bool foundGalaxy = false;
                for (int row = 0; row < input.Length; row++)
                {
                    if (input[row][col] == '#')
                    {
                        foundGalaxy = true;
                        break;
                    };
                }
                if (!foundGalaxy)
                {
                    emptyCols.Add(col);
                }
            }
            return emptyCols;
        }

        private static List<int> GetEmptyRows(string[] input)
        {
            var emptyRows = new List<int>();
            for (int row = 0; row < input.Length; row++)
            {
                if (!input[row].Contains('#'))
                {
                    emptyRows.Add(row);
                }
            }
            return emptyRows;
        }

        public List<GalaxyPair> GetDistances(int expansionFactor = 2)
        {
            var stepsDict = new Dictionary<Tuple<int, int>, long?>();
            foreach (var galaxySrc in Galaxies)
            {
                foreach (var galaxyTrg in Galaxies)
                {
                    if (galaxyTrg == galaxySrc) continue;

                    int srcId = Math.Min(galaxySrc.Code, galaxyTrg.Code);
                    int trgId = Math.Max(galaxySrc.Code, galaxyTrg.Code);
                    Tuple<int, int> dictKey = Tuple.Create(srcId, trgId);

                    if (stepsDict.ContainsKey(dictKey)) continue;

                    long steps = ComputeManhattanFor(galaxySrc, galaxyTrg, expansionFactor);
                    stepsDict[dictKey] = steps;
                }
            }
            var pairs = stepsDict.Select(d => new GalaxyPair { 
                Source = d.Key.Item1, 
                Target = d.Key.Item2, 
                Steps = stepsDict[d.Key] ?? 0
            }).ToList();

            return pairs;
        }

        private int ComputeManhattanFor(Galaxy galaxySrc, Galaxy galaxyTrg, int expansionFactor)
        {
            int minCol = Math.Min(galaxySrc.Col, galaxyTrg.Col);
            int minRow = Math.Min(galaxySrc.Row, galaxyTrg.Row);
            int maxCol = Math.Max(galaxySrc.Col, galaxyTrg.Col);
            int maxRow = Math.Max(galaxySrc.Row, galaxyTrg.Row);

            var steps = 0;

            for (int row = minRow+1; row <= maxRow; row++)
            {
                steps++;
                if (EmptyRows.Contains(row))
                {
                    steps += expansionFactor - 1;
                }
            }
            for (int col = minCol+1; col <= maxCol; col++)
            {
                steps++;
                if (EmptyCols.Contains(col))
                {
                    steps += expansionFactor - 1;
                }
            }

            return steps;
        }
    }
}