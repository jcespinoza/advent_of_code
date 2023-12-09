
namespace Advent23.Days.Day06
{
    public record RaceRecord
    {
        public required long Time { get; init; }
        public required long Distance { get; init; }
        public static RaceRecord[] ParseList(IEnumerable<string> input)
        {
            var timeDistParts = input.ToArray();
            var times = timeDistParts[0]
                .Split(":")[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => long.Parse(t.Trim()))
                .ToArray();
            var distances = timeDistParts[1]
                .Split(":")[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => long.Parse(t.Trim()))
                .ToArray();
            var records = new List<RaceRecord>();
            for (int index = 0; index < times.Length; index++)
            {
                records.Add(new()
                {
                    Time = times[index],
                    Distance = distances[index],
                });
            }

            return records.ToArray();
        }
    }
}