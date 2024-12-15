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
        // Example1: For a <stoneValue> of 1000, a <startingPosition> of 2 and a <countOfDigitsToExtract> of 2; the result should be 00
        // Example: For a <stoneValue> of 1000, a <startingPosition> of 0 and a <countOfDigitsToExtract> of 2; the result should be 10
        private static long ExtractDigits(long stoneValue, int startingPosition, int countOfDigitsToExtract)
        {
            long result = 0;
            for (int i = 0; i < countOfDigitsToExtract; i++)
            {
                long digit = (stoneValue / (long)Math.Pow(10, startingPosition + i)) % 10;
                result += digit * (long)Math.Pow(10, i);
            }
            return result;
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