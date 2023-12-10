
namespace Advent23.Days.Day08
{
    public record DesertMap
    {
        public required Direction[] Directions { get; init; }
        public required Dictionary<string, DesertNode> Nodes { get; init; }

        public static DesertMap Parse(IEnumerable<string> input)
        {
            var lines = input.ToArray();
            var directions = lines[0].Select(s => s == 'L' ? Direction.Left : Direction.Right).ToArray();

            var nodes = new Dictionary<string, DesertNode>();
            for (int index = 2; index < lines.Length; index++)
            {
                DesertNode node = DesertNode.Parse(lines[index]);
                nodes[node.Name] = node;
            }
            return new() { Directions = directions, Nodes = nodes };
        }
    }
}