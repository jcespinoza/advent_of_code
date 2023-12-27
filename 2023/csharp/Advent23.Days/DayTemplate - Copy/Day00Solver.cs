using AdventOfCode.Commons;

namespace Advent23.Days
{
    public class Day15Solver : Solver<string, long>
    {
        public Day15Solver() : base(2023, 15) { }

        public override string ParseInput(IEnumerable<string> input)
            => input.First();


        public override long PartOne(string input)
        {
            var stepStrs = input.Trim().Split(',', StringSplitOptions.TrimEntries);
            var currentHash = 0;
            var totalStepSum = 0;
            for (int stepIdx = 0; stepIdx < stepStrs.Length; stepIdx++)
            {
                var newHash = HashSequence(stepStrs[stepIdx], currentHash);
                totalStepSum += newHash;
            }

            return totalStepSum;
        }

        private int HashSequence(string sequence, int currentHash)
        {
            for (int index = 0; index < sequence.Length; index++)
            {
                var cChar = sequence[index];
                currentHash += cChar;
                currentHash *= 17;
                currentHash %= 256;
            }

            return currentHash;
        }

        public override long PartTwo(string input)
        {
            throw new NotImplementedException();
        }
    }
}