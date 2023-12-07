
namespace Advent23.Days.Day04
{
    public record ScratchCard
    {
        public int Number { get; init; }
        public required int[] Winners { get; init; } = [];
        public int[] Possesion { get; init; } = [];
        public int InstanceCount { get; set; }

        public static ScratchCard Parse(string input)
        {
            var parts = input.Split(':');
            var cardNumber = int.Parse(
                parts[0]
                .Split(' ')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray()[1]
            );
            var numberParts = parts[1].Split('|');
            var winners = numberParts[0].Trim()
                .Split(' ')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(int.Parse)
                .ToArray();
            var possesion = numberParts[1].Trim()
                .Split(' ')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(int.Parse)
                .ToArray();

            var card = new ScratchCard
            {
                Number = cardNumber,
                Winners = winners,
                Possesion = possesion,
                InstanceCount = 1,
            };
            return card;
        }
    }
}
