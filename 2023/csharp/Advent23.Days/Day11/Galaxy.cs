namespace Advent23.Days.Day11
{
    public record Galaxy
    {
        public required int Code { get; init; }
        public required int Row { get; init; }
        public required int Col { get; init; }
    }

    public record GalaxyPair
    {
        public required int Source { get; init; }
        public required int Target { get; init; }
        public required long Steps { get; init; }
    }
}