namespace Advent23.Days.Day05
{
    public record FarmingSetting
    {
        public required long Seed { get; init; }
        public required long Soil { get; init; }
        public required long Fertilizer { get; init; }
        public required long Water { get; init; }
        public required long Light { get; init; }
        public required long Temperature { get; init; }
        public required long Humidity { get; init; }
        public required long Location { get; init; }
    }
}