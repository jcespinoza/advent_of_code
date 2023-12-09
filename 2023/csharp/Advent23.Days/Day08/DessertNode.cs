
using System.ComponentModel.DataAnnotations;

namespace Advent23.Days.Day08
{
    public record DessertNode
    {
        public required string Name { get; init; }
        public required string RightName { get; init; }
        public required string LeftName { get; init; }

        public static DessertNode Parse(string line)
        {
            var parts = line
                .Split('=', StringSplitOptions.RemoveEmptyEntries);
            var name = parts[0].Trim();
            var pairStr = parts[1]
                .Trim()
                .Replace("(", string.Empty)
                .Replace(")", string.Empty)
                .Split(',', StringSplitOptions.TrimEntries);
            var leftValue = pairStr[0];
            var rightValue = pairStr[1];

            DessertNode node = new() {
                Name = name, 
                LeftName = leftValue, 
                RightName = rightValue 
            };
            return node;
        }
    }
}