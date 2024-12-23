using AdventOfCode.Commons;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text;

namespace Advent24.Days
{
    public class Day17Solver : Solver<Computer, string>
    {
        public Day17Solver() : base(2024, 17) { }

        public override Computer ParseInput(IEnumerable<string> input)
        {
            var regLines = input
                .TakeWhile(l => l.StartsWith("Reg"))
                .Select(l => l.Split(": "))
                .Select(l => new
                {
                    RegName = l[0].Last(),
                    RegValue = int.Parse(l[1].Trim())
                })
                .ToDictionary(l => l.RegName, l => l.RegValue);

            var program = input
                .SkipWhile(l => !l.StartsWith("Program"))
                .Select(l => l.Split(": ")[1].Trim())
                .SelectMany(l => l.Split(","))
                .Select(int.Parse)
                .ToArray();

            return new Computer
            {
                RegA = regLines['A'],
                RegB = regLines['B'],
                RegC = regLines['C'],
                Program = program,
                Ready = true,
            };
        }


        public override string PartOne(Computer computer)
        {
            StringBuilder strBuilder = new();

            Computer.SimulateProgram(computer, ref strBuilder);

            var output = strBuilder.ToString();

            return output;
        }

        public override string PartTwo(Computer computer)
        {
            throw new NotImplementedException();
        }
    }
}