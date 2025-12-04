using AdventOfCode.Commons;

namespace Advent.Days
{
    public class Day02Solver : Solver<Range[], long>
    {
        public Day02Solver() : base(2025, 02) { }

        public override Range[] ParseInput(IEnumerable<string> input)
            => input
            .SelectMany(line => line.Split(","))
            .Select(Range.From)
            .ToArray();

        public override long PartOne(Range[] ranges)
        {
            var invalidIds = new List<long>();
            foreach (var range in ranges)
            {
                invalidIds.AddRange(IdentifyInvalidIds(range, ValidationStrategy.HalvesMatch));
            }

            return invalidIds.Sum();
        }

        public override long PartTwo(Range[] ranges)
        {
            var invalidIds = new List<long>();
            foreach (var range in ranges)
            {
                invalidIds.AddRange(IdentifyInvalidIds(range, ValidationStrategy.RepeatingPattern));
            }

            return invalidIds.Sum();
        }

        private static IEnumerable<long> IdentifyInvalidIds(Range range, ValidationStrategy strategy)
        {
            for (var id = range.Start; id <= range.End; id++)
            {
                if (!IsIdValid(id, strategy))
                    yield return id;
            }
        }

        private static bool IsIdValid(long id, ValidationStrategy strategy)
            => strategy switch
            {
                ValidationStrategy.HalvesMatch => IsValidHalvesMatch(id),
                ValidationStrategy.RepeatingPattern => IsValidNoRepeatingPattern(id),
                _ => true
            };

        private static bool IsValidHalvesMatch(long id)
        {
            var idStr = id.ToString();
            var len = idStr.Length;

            if (len % 2 != 0)
                return true; // odd lengths considered valid

            var mid = len / 2;
            var firstHalf = idStr[..mid];
            var secondHalf = idStr[mid..];
            return firstHalf != secondHalf;
        }

        private static bool IsValidNoRepeatingPattern(long id)
        {
            var idStr = id.ToString();
            var len = idStr.Length;

            for (var patternLen = 1; patternLen <= len / 2; patternLen++)
            {
                if (len % patternLen != 0)
                    continue;

                var pattern = idStr[..patternLen];
                var repeatCount = len / patternLen;
                var repeated = string.Concat(Enumerable.Repeat(pattern, repeatCount));
                if (repeated == idStr)
                    return false; // invalid id (made of repeated pattern)
            }

            return true;
        }
    }
}