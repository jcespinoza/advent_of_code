using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day19Solver : Solver<OnsenWarehouse, long>
    {
        public Day19Solver() : base(2024, 19) { }

        public override OnsenWarehouse ParseInput(IEnumerable<string> input)
        {
            var availability = input
                .First()
                .Split(", ")
                .ToList();

            var patterns
                = input
                .Skip(2)
                .ToList();

            return new()
            {
                Patterns = availability,
                Designs = patterns
            };
        }


        public override long PartOne(OnsenWarehouse warehouse)
        {
            List<string> designs = GetFeasibleDesigns(warehouse);

            return designs.Count;
        }

        private List<string> GetFeasibleDesigns(OnsenWarehouse warehouse)
        {
            Dictionary<string, bool> feasibilityCache = [];
            var feasibleDesigns = new List<string>();
            var maxPatterLength = warehouse.Patterns.Max(x => x.Length);

            foreach (var design in warehouse.Designs) {
                var possible = IsDesignPossible(design, warehouse.Patterns, feasibilityCache, maxPatterLength);
                if (possible)
                {
                    feasibleDesigns.Add(design);
                }
            }
            return feasibleDesigns;
        }

        private static bool IsDesignPossible(string design, List<string> patterns, Dictionary<string, bool> feasibilityCache, int maxPatterLength)
        {
            if(feasibilityCache.TryGetValue(design, out bool cachedResult))
            {
                return cachedResult;
            }

            if(design.Length == 0)
            {
                return true;
            }
            var maxLength = Math.Min(design.Length, maxPatterLength);
            foreach (var countToGrab in Enumerable.Range(0, maxLength +1 ))
            {
                bool patternExists = patterns.Contains(design[..countToGrab]);
                if (!patternExists) continue;
                bool subPatternFeasible = IsDesignPossible(design[countToGrab..], patterns, feasibilityCache, maxPatterLength);
                if (patternExists && subPatternFeasible)
                {
                    feasibilityCache.Add(design, true);
                    return true;
                }
            }
            feasibilityCache.Add(design, false);
            return false;
        }
        private static long GetFeasibleDesignPermutations(OnsenWarehouse warehouse)
        {
            Dictionary<string, int> feasibilityCache = [];
            List<long> combinations = [];
            var maxPatterLength = warehouse.Patterns.Max(x => x.Length);
            foreach (var design in warehouse.Designs)
            {
                int count = GetPossibleDesignArrangements(design, warehouse.Patterns, feasibilityCache, maxPatterLength);
                if (count > 0)
                {
                    combinations.Add(count);
                }
            }
            long sumFeasible = feasibilityCache.Values.Select(i => (long)i).Sum();
            long subCombinations = combinations.Sum();
            return subCombinations;
        }

        private static int GetPossibleDesignArrangements(string design, List<string> patterns, Dictionary<string, int> feasibilityCache, int maxPatterLength)
        {
            if (feasibilityCache.TryGetValue(design, out int cachedResult))
            {
                return cachedResult;
            }

            if (design.Length == 0)
            {
                return 1;
            }

            var maxLength = Math.Min(design.Length, maxPatterLength);
            var count = 0;
            // Repeatedly match smaller chunks of the string against the available patterns
            foreach (var countToGrab in Enumerable.Range(0, maxLength + 1))
            {
                // Recursively check the remainder of the pattern
                if (patterns.Contains(design[..countToGrab]))
                {
                    int subPermuttations = GetPossibleDesignArrangements(design[countToGrab..], patterns, feasibilityCache, maxPatterLength);
                    count += subPermuttations;
                }
            }
            feasibilityCache.Add(design, count);
            return count;
        }

        public override long PartTwo(OnsenWarehouse warehouse)
        {
            long combinations = GetFeasibleDesignPermutations(warehouse);

            return combinations;
        }
    }

    public class OnsenWarehouse
    {
        public List<string> Patterns { get; set; } = [];
        public List<string> Designs { get; set; } = default!;
    }
}