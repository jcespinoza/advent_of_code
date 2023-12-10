
namespace Advent23.Days.Day08
{
    public record DesertMap
    {
        public required string Directions { get; init; }
        public required Dictionary<string, DessertNode> Nodes { get; init; }

        public static DesertMap Parse(IEnumerable<string> input)
        {
            var lines = input.ToArray();
            var directions = lines[0];

            var nodes = new Dictionary<string, DessertNode>();
            for (int index = 2; index < lines.Length; index++)
            {
                DessertNode node = DessertNode.Parse(lines[index]);
                nodes[node.Name] = node;
            }
            return new() { Directions = directions, Nodes = nodes };
        }
    }
}