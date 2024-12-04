
namespace Advent24.Days.Day02
{
    public record Report
    {

        public int[] Levels { get; init; } = [];

        public bool IsSafe()
        {
            if(Levels.Length <= 2)
            {
                return true;
            }

            var maxDifference = 0;
            bool directionFound = false;
            int direction = 0;

            for (int index = 0; index < Levels.Length; index++)
            {
                if(index == Levels.Length - 1)
                {
                    break;
                }
                
                var difference = Levels[index] - Levels[index + 1];

                var absDiff = Math.Abs(difference);

                if (absDiff > 3 || absDiff == 0) return false;

                if (absDiff > maxDifference)
                {
                    maxDifference = absDiff;
                }

                if (!directionFound) {
                    direction = Math.Sign(difference);
                    directionFound = true;
                }

                if(directionFound && direction != Math.Sign(difference))
                {
                    return false;
                }
            }

            return maxDifference <= 3;
        }
    }
}
