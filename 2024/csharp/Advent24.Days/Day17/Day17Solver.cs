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

            SimulateProgram(computer, ref strBuilder);

            var output = strBuilder.ToString();

            return output;
        }

        private static void SimulateProgram(Computer computer, ref StringBuilder strBuilder)
        {
            long cycles = 0;
            while (computer.Ready)
            {
                cycles++;
                int opCode = computer.ReadNext();
                switch ((Computer.Instruction)opCode)
                {
                    case Computer.Instruction.ADV:
                        ExecuteAdv(computer);
                        break;
                    case Computer.Instruction.BXL:
                        ExecuteBxl(computer);
                        break;
                    case Computer.Instruction.BST:
                        ExecuteBst(computer);
                        break;
                    case Computer.Instruction.JNZ:
                        ExecuteJnz(computer);
                        break;
                    case Computer.Instruction.BXC:
                        ExecuteBxc(computer);
                        break;
                    case Computer.Instruction.OUT:
                        ExecuteOut(computer, strBuilder);
                        break;
                    case Computer.Instruction.BDV:
                        ExecuteBdv(computer);
                        break;
                    case Computer.Instruction.CDV:
                        ExecuteCdv(computer);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        private static void ExecuteCdv(Computer computer)
        {
            PerformDv(computer, result => computer.RegC = result);
        }

        private static void ExecuteBdv(Computer computer)
        {
            PerformDv(computer, result => computer.RegB = result);
        }

        private static void ExecuteOut(Computer computer, StringBuilder strBuilder)
        {
            var operand = computer.ReadNext();
            var actualOperand = ResolveComboOperand(computer, operand);
            var modulo8 = actualOperand % 8;
            if(strBuilder.Length == 0)
            {
                strBuilder.Append(modulo8);
            }
            else
            {
                strBuilder.Append($",{modulo8}");
            }
        }

        private static void ExecuteBxc(Computer computer)
        {
            // Read operand but ignore it
            var operand = computer.ReadNext();
            var xorResult = computer.RegB ^ computer.RegC;
            computer.RegB = xorResult;
        }

        private static void ExecuteJnz(Computer computer)
        {
            if (computer.RegA == 0)
            {
                computer.MovePointer(2);
            }
            else
            {
                int operand = GetLiteral(computer, isForJump: true);
                computer.JumpTo(operand);
            }
        }

        private static void ExecuteBst(Computer computer)
        {
            var operand = computer.ReadNext();
            var actualOperand = ResolveComboOperand(computer, operand);
            var modulo8 = actualOperand % 8;
            computer.RegB = modulo8;
        }

        private static void ExecuteBxl(Computer computer)
        {
            var operand = computer.ReadNext();
            var actualOperand = operand;
            var xorResult = computer.RegB ^ actualOperand;
            computer.RegB = xorResult;
        }

        private static void ExecuteAdv(Computer computer)
        {
            PerformDv(computer, result => computer.RegA = result);
        }

        private static void PerformDv(Computer computer, Action<int> action)
        {
            var numerator = computer.RegA;
            var comboOperand = computer.ReadNext();
            var actualOperand = ResolveComboOperand(computer, comboOperand);
            var denominator = Math.Pow(2, actualOperand);

            var result = (int)Math.Floor(numerator / denominator);
            action(result);
        }

        private static int GetLiteral(Computer computer, bool isForJump = false)
        {
            var literal = computer.Program[computer.ProgramPointer];

            if (!isForJump)
            {
                computer.MovePointer(1);
            }

            if (literal < 0 || literal > 3) { 
                throw new InvalidOperationException("Expected a literal combo operand"); 
            }
            
            return literal;
        }

        private static int ResolveComboOperand(Computer computer, int comboOperand)
        {
            if (comboOperand > 0 && comboOperand <= 3) return comboOperand;

            if (comboOperand == 4) return computer.RegA;
            if (comboOperand == 5) return computer.RegB;
            if (comboOperand == 6) return computer.RegC;

            throw new Exception($"Combo Operand {comboOperand} is not valid!");
        }

        public override string PartTwo(Computer computer)
        {
            throw new NotImplementedException();
        }
    }
}