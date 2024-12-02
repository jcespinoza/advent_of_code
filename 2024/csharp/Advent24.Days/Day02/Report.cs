namespace Advent24.Days.Day02
{
    public record Report
    {
        public record Level (int LevelNumber);

        public Level[] Levels { get; init; } = [];
    }
}
