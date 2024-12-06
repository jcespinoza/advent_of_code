using Advent24.Days.Day03;
using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day03Solver : Solver<Operation[], long>
    {
        public Day03Solver() : base(2024, 03) { }

        public override Operation[] ParseInput(IEnumerable<string> input)
        {
            Operation[] operations = input.SelectMany(ParseOperationsInLine).ToArray();

            return operations;
        }

        private Operation[] ParseOperationsInLine(string line)
        {
            var parser = new OperationParser(line);
            var operations = parser.ReadOperations();
            return [.. operations];
        }

        public override long PartOne(Operation[] operations)
        {
            long totalSum = operations.Sum(o => o.OperandA * o.OperandB);

            return totalSum;
        }

        public override long PartTwo(Operation[] input)
        {
            throw new NotImplementedException();
        }
    }
}