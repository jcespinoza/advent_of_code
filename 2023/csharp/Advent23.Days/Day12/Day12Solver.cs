using Advent23.Days.Day12;
using AdventOfCode.Commons;
using FluentAssertions.Formatting;
using System.Security.AccessControl;
using System.Text;

namespace Advent23.Days
{
    public class Day12Solver : Solver<ConditionRecord[], long>
    {
        public Day12Solver() : base(2023, 12) { }

        public override ConditionRecord[] ParseInput(IEnumerable<string> input)
            => input.Select(ConditionRecord.Parse).ToArray();


        public override long PartOne(ConditionRecord[] conditionRecords)
        {
            Dictionary<ConditionRecord, long> cache = new();
            long sumOfArrangements = 0;
            foreach (var line in conditionRecords)
            {
                var arrangements = GetPossibleArrangements(new ConditionRecord { 
                    Text = $".{line.Text}.", Sizes = line.Sizes 
                    }, cache);
                sumOfArrangements += arrangements;
            }

            return sumOfArrangements;
        }

        private long GetPossibleArrangements(ConditionRecord line, Dictionary<ConditionRecord, long> cache)
        {
            if (cache.ContainsKey(line)) return cache[line];

            if (line.Sizes.Length == 0)
            {
                return line.Text.Contains('#') ? 0 : 1;
            }

            if (string.IsNullOrWhiteSpace(line.Text))
            {
                return line.Sizes.Length != 0 ? 0 : 1;
            }

            var size = line.Sizes[0];
            var groups = line.Sizes.Skip(1).ToArray();
            var result = 0L;

            for (int end = 0; end < line.Text.Length; end++)
            {
                var start = end - (size - 1);

                if(ConditionRecord.Fits(line.Text, start, end))
                {
                    result += GetPossibleArrangements(
                        new() {
                            Text = line.Text.Substring(end + 1),
                            Sizes = groups
                        },
                        cache
                    );
                }
            }

            cache[line] = result;
            return result;
        }

        public override long PartTwo(ConditionRecord[] input)
        {
            ConditionRecord[] conditionRecords = ExpandRecords(input);
            Dictionary<ConditionRecord, long> cache = new();
            long sumOfArrangements = 0;
            foreach (var line in conditionRecords)
            {
                var arrangements = GetPossibleArrangements(new ConditionRecord
                {
                    Text = $".{line.Text}.",
                    Sizes = line.Sizes
                }, cache);
                sumOfArrangements += arrangements;
            }

            return sumOfArrangements;
        }

        private ConditionRecord[] ExpandRecords(ConditionRecord[] input)
        {
            return input.Select(line =>
            {
                return new ConditionRecord {
                    Text = string.Join('?',Enumerable.Repeat(line.Text, 5)),
                    Sizes = Enumerable.Repeat(line.Sizes, 5).SelectMany(a => a).ToArray()
                };
            }).ToArray();
        }
    }
}