
namespace Advent23.Days.Day04
{
    public record ScratchCard
    {
        public int Number { get; init; }
        public required int[] Winners { get; init; } = [];
        public int[] Possesion { get; init; } = [];

        public static ScratchCard Parse(string input)
        {
            var parts = input.Split(':');
            var cardNumber = int.Parse(parts[0].Split(' ')[1]);
            var numberParts = parts[1].Split('|');
            var winners = numberParts[0].Trim()
                .Split(' ')
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
                Possesion = possesion
            };
            return card;
        }
    }
}
