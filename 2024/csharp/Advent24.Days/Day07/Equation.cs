using System.Diagnostics;

namespace Advent24.Days
{
    public record Equation(long TestValue, long[] Operands)
    {
        public static Equation Parse(string line)
        {
            var parts = line.Split(": ");

            Debug.Assert(parts.Length == 2, "Invalid input format");

            var testValue = long.Parse(parts[0]);
            var operands = parts[1].Split(" ").Select(long.Parse).ToArray();

            return new Equation(testValue, operands);
        }
    }

    public enum Operation
    {
        Add,
        Multiply,
        Concatenation
    }
}