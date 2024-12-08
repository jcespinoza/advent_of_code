namespace Advent24.Days
{
    public class PrintingConfig
    {
        public List<OrderingRule> Rules { get; init; } = [];
        public List<int[]> Updates { get; init; } = [];
    }

    /// <summary>
    /// Represents a rule for ordering pages.
    /// PageX must come before PageY.
    /// </summary>
    /// <param name="PageX"></param>
    /// <param name="PageY"></param>
    public record OrderingRule(int PageX, int PageY);
}