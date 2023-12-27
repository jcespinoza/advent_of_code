

using System.Configuration;
using System.Configuration.Internal;

namespace Advent23.Days.Day13
{
    public record TerrainPattern
    {
        public required string[] Lines { get; init; }
        public List<int> HMirrorIndexes { get; set; } = [];
        public List<int> VMirrorIndexes { get; set; } = [];

        public static TerrainPattern[] ParsePatternGroups(IEnumerable<string> lines)
        {
            var patterns = new List<TerrainPattern>();
            var patternLines = new List<string>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    var newPattern = new TerrainPattern
                    {
                        Lines = [.. patternLines],
                    };
                    patterns.Add(newPattern);
                    patternLines = [];
                    continue;
                }
                patternLines.Add(line);
            }

            if (patternLines.Any())
            {
                var newPattern = new TerrainPattern
                {
                    Lines = [.. patternLines],
                };
                patterns.Add(newPattern);
            }

            return [.. patterns];
        }

        public static List<int> ComputeHMirrorIndexes(TerrainPattern pattern)
        {
            List<int> candidates = [];
            for (int index = 1; index < pattern.Lines.Length; index++)
            {
                if (pattern.Lines[index] == pattern.Lines[index - 1])
                {
                    candidates.Add(index);
                }
            }

            List<int> confirmedIndexes = [];
            foreach (var candidate in candidates)
            {
                var min = candidate - 1;
                var max = candidate;
                var allRowsMatch = true;
                while (true)
                {
                    if (min < 0 || max >= pattern.Lines.Length) break;

                    if (pattern.Lines[min] != pattern.Lines[max])
                    {
                        allRowsMatch = false;
                        break;
                    }
                    min--;
                    max++;
                }
                if (allRowsMatch)
                {
                    confirmedIndexes.Add(candidate);
                }
            }
            return confirmedIndexes;
        }

        public static List<int> ComputeVMirrorIndexes(TerrainPattern pattern)
        {
            List<int> candidates = [];
            for (int col = 1; col < pattern.Lines[0].Length; col++)
            {
                bool allMatches = true;
                for (int row = 0; row < pattern.Lines.Length; row++)
                {
                    if (pattern.Lines[row][col] != pattern.Lines[row][col - 1])
                    {
                        allMatches = false;
                        break;
                    }
                }
                if (allMatches)
                {
                    candidates.Add(col);
                }
            }
            List<int> confirmedIndexes = [];
            foreach (var candidate in candidates)
            {

                var minCol = candidate - 1;
                var maxCol = candidate;
                var allColsMatch = true;
                while (true)
                {
                    if (minCol < 0 || maxCol >= pattern.Lines[0].Length) break;

                    for (int row = 0; row < pattern.Lines.Length; row++)
                    {
                        if (pattern.Lines[row][minCol] != pattern.Lines[row][maxCol])
                        {
                            allColsMatch = false;
                            break;
                        }
                    }
                    if (!allColsMatch) break;

                    minCol--;
                    maxCol++;
                }
                if (allColsMatch)
                {
                    confirmedIndexes.Add(candidate);
                }
            }
            return confirmedIndexes;
        }

        public static int ComputeHReflectedLines(TerrainPattern pattern)
        {
            if (pattern.HMirrorIndexes.Count == 0) return 0;

            return pattern.HMirrorIndexes.Last();
        }

        public static int ComputeVReflectedLines(TerrainPattern pattern)
        {
            if (pattern.VMirrorIndexes.Count == 0) return 0;

            return pattern.VMirrorIndexes.Last();
        }
    }
}