using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day22Solver : Solver<int[], long>
    {
        private const int PRUNE_FACTOR = 16777216;
        public Day22Solver() : base(2024, 22) { }

        public override int[] ParseInput(IEnumerable<string> input)
            => input.Select(int.Parse).ToArray();


        public override long PartOne(int[] initialSecrets)
        {
            int stepOfInterest = 2000;
            long totalSum = 0;
            foreach (int secretNumber in initialSecrets)
            {
                long nthSecret = FindNthSecretNumber(initial: secretNumber, stepOfInterest);
                totalSum += nthSecret;
            }
            return totalSum;
        }

        public static long FindNthSecretNumber(int initial, int stepOfInterest)
        {
            long currentSecret = initial;
            for (int step = 0; step < stepOfInterest; step++)
            {
                long prod = currentSecret * 64;
                currentSecret ^= prod;
                currentSecret %= PRUNE_FACTOR;
                long quotient = currentSecret / 32;
                currentSecret ^= quotient;
                currentSecret %= PRUNE_FACTOR;
                prod = currentSecret * 2048;
                currentSecret ^= prod;
                currentSecret %= PRUNE_FACTOR;
            }
            return currentSecret;
        }

        public override long PartTwo(int[] initialSecrets)
        {
            throw new NotImplementedException();
        }
    }
}