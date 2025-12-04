namespace Advent.Days
{
    public class BatteryBank(IEnumerable<byte> batteries)
    {
        public List<byte> Batteries { get; init; } = batteries.ToList();

        public static BatteryBank From(string s)
        {
            var batteries = s.Select(c => (byte)(c - '0'));
            return new BatteryBank(batteries);
        }

        // Returns the top N batteries as digits in the same order they appear
        public List<byte> TopNBatteries(int n)
        {
            var len = Batteries.Count;
            if (n >= len)
                return new List<byte>(Batteries);

            var result = new List<byte>(n);
            var start = 0;
            var remaining = n;

            while (remaining > 0)
            {
                var maxPos = len - remaining; // inclusive
                var bestIdx = start;
                var bestVal = Batteries[start];

                for (var i = start; i <= maxPos; i++)
                {
                    var v = Batteries[i];
                    if (v > bestVal)
                    {
                        bestVal = v;
                        bestIdx = i;
                        if (bestVal == 9)
                            break;
                    }
                }

                result.Add(bestVal);
                start = bestIdx + 1;
                remaining--;
            }

            return result;
        }
    }
}
