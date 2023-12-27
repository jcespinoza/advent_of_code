namespace Advent23.Days.Day15
{
    public record Lens
    {
        public int FocalLength { get; set; }
        public required string Label { get; init; }
    }
}