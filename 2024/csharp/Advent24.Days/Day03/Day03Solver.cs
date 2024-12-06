using Advent24.Days.Day03;
using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day03Solver : Solver<Instruction[], long>
    {
        public Day03Solver() : base(2024, 03) { }

        public override Instruction[] ParseInput(IEnumerable<string> input)
        {
            Instruction[] intructions = input.SelectMany(ParseOperationsInLine).ToArray();

            return intructions;
        }

        private Instruction[] ParseOperationsInLine(string line)
        {
            var parser = new OperationParser(line);
            var instructions = parser.ReadInstructions();
            return [.. instructions];
        }

        public override long PartOne(Instruction[] intructions)
        {
            long totalSum = intructions
                .Where(i => i is MulOperation mul)
                .Select(i => i as MulOperation)
                .Sum(o => o!.OperandA * o!.OperandB);

            return totalSum;
        }

        public override long PartTwo(Instruction[] instructions)
        {
            bool enableMuls = true;
            long totalSum = 0;
            for (int index = 0; index < instructions.Length; index++)
            {
                var current = instructions[index];
                if (current is DoInstruction)
                {
                    enableMuls = true;
                }
                else if (current is DontInstruction)
                {
                    enableMuls = false;
                }
                else if (current is MulOperation mul && enableMuls)
                {
                    totalSum += mul.OperandA * mul.OperandB;
                }
            }

            return totalSum;
        }
    }
}