namespace Advent23.Days
{
    public record MachinePart
    {
        public required int Number { get; init; }
        public required char Symbol { get; init; }
        public required int SymbolRow { get; init; }
        public required int SymbolCol { get; init; }
    }
}