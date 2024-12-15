using System;

namespace Advent24.Days
{
    public static class PlutonianPebbles
    {
        public static List<long> CalculateState(long[] stones, int blinkCount)
        {
            List<long> newStones = [.. stones];
            for (int blink = 1; blink <= blinkCount; blink++)
            {
                Shift(newStones);
            }
            return newStones;
        }

        public static long CountStones(long[] stones, int blinkCount)
        {
            if (blinkCount == 0) return stones.Length;

            Dictionary<(long, int), long> countsCache = [];
            long totalStones = 0;
            foreach (var stone in stones)
            {
                long stonesAfterBlinks = ComputeHistoryForStone(stone, blinkCount, countsCache);
                totalStones += stonesAfterBlinks;
            }

            return totalStones;
        }

        private static long ComputeHistoryForStone(long stoneValue, int blinkCount, Dictionary<(long, int), long> countsCache)
        {
            if (blinkCount <= 0) return 1;

            if(countsCache.ContainsKey((stoneValue, blinkCount)))
            {
                return countsCache[(stoneValue, blinkCount)];
            }

            long result;
            if (stoneValue == 0)
            {
                result = ComputeHistoryForStone(1, blinkCount - 1, countsCache);
                countsCache.Add((stoneValue, blinkCount), result);
                return result;
            }

            int digitCount = GetDigitCount(stoneValue);
            if (digitCount % 2 == 0)
            {
                // Split the stone in two
                long newLeftStone = ExtractDigits(stoneValue, 0, digitCount / 2);
                long newRightStone = ExtractDigits(stoneValue, digitCount / 2, digitCount / 2);
                var leftHistory = ComputeHistoryForStone(newLeftStone, blinkCount - 1, countsCache);
                var rightHistory = ComputeHistoryForStone(newRightStone, blinkCount - 1, countsCache);

                result = leftHistory + rightHistory;
                countsCache.Add((stoneValue, blinkCount), result);
                return result;
            }


            result = ComputeHistoryForStone(stoneValue * 2024, blinkCount - 1, countsCache);
            countsCache.Add((stoneValue, blinkCount), result);
            return result;
        }

        private static void Shift(List<long> newStones)
        {
            for (int index = 0; index < newStones.Count; index++)
            {
                var stoneValue = newStones[index];
                if (stoneValue == 0)
                {
                    newStones[index] = 1;
                    continue;
                }

                int digitCount = GetDigitCount(stoneValue);
                if (digitCount % 2 == 0)
                {
                    // Split the stone in two
                    long newLeftStone = ExtractDigits(stoneValue, 0, digitCount / 2);
                    long newRightStone = ExtractDigits(stoneValue, digitCount / 2, digitCount / 2);
                    // Remove current stone
                    newStones.RemoveAt(index);
                    // Insert new left stone
                    newStones.Insert(index, newLeftStone);
                    // Insert new right stone
                    newStones.Insert(index + 1, newRightStone);
                    index++; // skip the next stones as it will be a new one
                    continue;
                }

                newStones[index] = stoneValue * 2024;
            }
        }

        // Takes the <countOfDigitsToExtract> digits from the <stoneValue> starting from the <startingPosition>
        // Example2: For a <stoneValue> of 1000, a <startingPosition> of 0 and a <countOfDigitsToExtract> of 2; the result should be 10
        // Example1: For a <stoneValue> of 1000, a <startingPosition> of 2 and a <countOfDigitsToExtract> of 2; the result should be 00
        private static long ExtractDigits(long stoneValue, int startingPosition, int countOfDigitsToExtract)
        {
            string stoneStr = stoneValue.ToString();
            string newStoneStr = stoneStr.Substring(startingPosition, countOfDigitsToExtract);
            return long.Parse(newStoneStr);
        }

        private static int GetDigitCount(long number)
        {
            int count = 0;
            while (number != 0)
            {
                number /= 10;
                count++;
            }
            return count;
        }
    }
}