
namespace Advent23.Days.Day11
{
    public record SpaceMap
    {
        public required List<Galaxy> Galaxies { get; init; }

        public static SpaceMap Parse(string[] input)
        {
            string[] expanded = ExpandUniverse(input);
            List<Galaxy> galaxies = new();
            int counter = 1;
            for (int row = 0; row < expanded.Length; row++)
            {
                for (int col = 0; col < expanded[row].Length; col++)
                {
                    char symbol = expanded[row][col];
                    if (symbol == '#')
                    {
                        var galaxy = new Galaxy { Code = counter, Row = row, Col = col };
                        galaxies.Add(galaxy);
                        counter++;
                    }
                }
            }
            var spaceMap = new SpaceMap()
            {
                Galaxies = galaxies
            };

            return spaceMap;
        }

        private static string[] ExpandUniverse(string[] input)
        {
            var emptyRows = new Stack<int>();
            var emptyCols = new Stack<int>();
            for (int row = 0; row < input.Length; row++)
            {
                if (!input[row].Contains('#'))
                {
                    emptyRows.Push(row);
                }
            }
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
                    emptyCols.Push(col);
                }
            }

            while (emptyCols.Count != 0)
            {
                var index = emptyCols.Pop();
                for (int row = 0; row < input.Length; row++)
                {
                    input[row] = input[row].Insert(index, ".");
                }
            }
            var newLines = input.ToList();
            while (emptyRows.Count != 0)
            {
                var index = emptyRows.Pop();
                newLines.Insert(index, input[index]);
            }
            return [.. newLines];
        }

        public List<GalaxyPair> GetDistances()
        {
            var stepsDict = new Dictionary<Tuple<int, int>, int?>();
            foreach (var galaxySrc in Galaxies)
            {
                foreach (var galaxyTrg in Galaxies)
                {
                    if (galaxyTrg == galaxySrc) continue;

                    int srcId = Math.Min(galaxySrc.Code, galaxyTrg.Code);
                    int trgId = Math.Max(galaxySrc.Code, galaxyTrg.Code);
                    Tuple<int, int> dictKey = Tuple.Create(srcId, trgId);

                    if (stepsDict.ContainsKey(dictKey)) continue;

                    int steps = ComputeManhattanFor(galaxySrc, galaxyTrg);
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

        private static int ComputeManhattanFor(Galaxy galaxySrc, Galaxy galaxyTrg)
        {
            int deltaX = Math.Abs(galaxyTrg.Col - galaxySrc.Col);
            int deltaY = Math.Abs(galaxyTrg.Row - galaxySrc.Row);

            return deltaX + deltaY;
        }
    }
}