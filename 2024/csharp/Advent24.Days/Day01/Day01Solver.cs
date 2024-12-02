using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day01Solver : Solver<int[][], long>
    {
        public Day01Solver() : base(2024, 01) { }

        public override int[][] ParseInput(IEnumerable<string> input)
        {
            return input
                .Select(line =>
                {
                    var parts = line.Split(' ')
                        .Where(p => !string.IsNullOrWhiteSpace(p))
                        .Select(part => part.Trim())
                        .Select(int.Parse)
                        .ToArray();
                    return parts;
                })
                .ToArray();
        }


        public override long PartOne(int[][] pairsArray)
        {
            IEnumerable<int> leftIds = pairsArray.Select(i => {
                    return i[0];
                });
            IEnumerable<int> rightIds = pairsArray.Select(i =>
            {
                return i[1];
            });
            int[] sortedLeftIds = leftIds.OrderBy(i => i).ToArray();
            int[] sortedRightIds = rightIds.OrderBy(i => i).ToArray();

            int[] differences = new int[pairsArray.Length];

            for (int i = 0; i < pairsArray.Length; i++)
            {
                differences[i] = Math.Abs(sortedRightIds[i] - sortedLeftIds[i]);
            }

            int finalSumOfDifference = differences.Sum();
            return finalSumOfDifference;
        }

        public override long PartTwo(int[][] pairsArray)
        {
            IEnumerable<int> rightIds = pairsArray.Select(i =>
            {
                return i[1];
            });

            int[] similarityScore = new int[pairsArray.Length];
            // create a dictionary to store the frequencies with which the items in the leftIds array appear in the rightIds array
            Dictionary<int, int> frequencies = [];
            for (int index = 0; index < pairsArray.Length; index++)
            {
                int currentLefty = pairsArray[index][0];
                if (frequencies.TryGetValue(currentLefty, out int value))
                {
                    similarityScore[index] = currentLefty * value;
                    continue;
                }

                // find how many times the rightIds array contain currentLefty
                int frequency = rightIds.Count(i => i == currentLefty);
                frequencies.Add(currentLefty, frequency);
                similarityScore[index] = currentLefty * frequency;
            }

            int finalSimilarityScore = similarityScore.Sum();
            return finalSimilarityScore;
        }
    }
}