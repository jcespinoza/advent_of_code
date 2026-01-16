
namespace Advent23.Days.Day14
{
    public record Rock
    {
        public required int Row { get; init; }
        public required int Col { get; init; }
        public required SlotType Type { get; init; }
        public required int EmptyAbove { get; init; }

        public static SlotType ParseType(char symbol)
        {
            return symbol switch
            {
                'O' => SlotType.Sphere,
                '#' => SlotType.Cube,
                _ => SlotType.Empty
            };
        }
    }

    public enum SlotType
    {
        Empty,
        Sphere,
        Cube
    }
}