using AdventOfCode.Commons;
using System.Collections.Immutable;
using System.Configuration;

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
                currentSecret = AdvanceSecret(currentSecret);
            }
            return currentSecret;
        }

        private static long AdvanceSecret(long currentSecret)
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
            return currentSecret;
        }

        public override long PartTwo(int[] initialSecrets)
        {
            int stepOfInterest = 2000;
            long maxBananas = 0;
            List<List<int>> monkeyPriceLists = ComputePrices(initialSecrets, stepOfInterest);
            HashSet<(int,int,int,int)> seenSequences = [];
            Dictionary<(int, int, int, int), int> possibleBananas = [];
            foreach (var priceList in monkeyPriceLists)
            {
                for (int index = 0; index < priceList.Count - 5; index++)
                {
                    var chunk = priceList.GetRange(index, 5);
                    var differences = chunk.Zip(chunk.Skip(1), (a, b) => b - a).Select(i => i).ToList();
                    var asSequence = (differences[0], differences[1], differences[2], differences[3]);
                    (int a, int b, int c, int d) = asSequence;
                    int e = chunk[4];
                    
                    if (seenSequences.Contains(asSequence)) continue;
                    seenSequences.Add(asSequence);

                    possibleBananas.TryAdd(asSequence, 0);
                    possibleBananas[asSequence] += e;
                }
            }
            maxBananas = possibleBananas.Values.Max();
            return maxBananas;
        }

        private static List<List<int>> ComputePrices(int[] initialSecrets, int stepOfInterest)
        {
            List<List<int>> monkeyPrices = [];
            foreach (int secretNumber in initialSecrets)
            {
                List<int> prices = ComputePriceList(secretNumber, stepOfInterest);

                monkeyPrices.Add(prices);
            }
            return monkeyPrices;
        }

        public static List<int> ComputePriceList(int secretNumber, int stepOfInterest)
        {
            List<int> prices = [secretNumber % 10];
            int currentSecret = secretNumber;
            for (int step = 0; step < stepOfInterest-1; step++)
            {
                currentSecret = (int)AdvanceSecret(currentSecret);
                prices.Add(currentSecret % 10);
            }

            return prices;
        }
    }
}