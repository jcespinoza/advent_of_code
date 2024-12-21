using AdventOfCode.Commons;

namespace Advent24.Days
{
    public record Warehouse
    {
        public char[][] Map { get; set; } = [];
        public List<Direction> Moves { get; set; } = [];
    }
}