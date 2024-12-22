using System.Text;

namespace Advent24.Days
{
    public record Computer
    {
        public enum Instruction
        {
            ADV,
            BXL,
            BST,
            JNZ,
            BXC,
            OUT,
            BDV,
            CDV
        }

        public bool Ready { get; set; }
        public int ProgramPointer { get; set; }
        public int RegA { get; set; }
        public int RegB { get; set; }
        public int RegC { get; set; }
        public int[] Program { get; set; } = [];

        public static void SimulateProgram(Computer computer, ref StringBuilder strBuilder)
        {
            long cycles = 0;
            while (computer.Ready)
            {
                cycles++;
                int opCode = computer.ReadNext();
                Instruction instruction = (Instruction)opCode;
                switch (instruction)
                {
                    case Instruction.ADV:
                        ExecuteAdv(computer);
                        break;
                    case Instruction.BXL:
                        ExecuteBxl(computer);
                        break;
                    case Instruction.BST:
                        ExecuteBst(computer);
                        break;
                    case Instruction.JNZ:
                        ExecuteJnz(computer);
                        break;
                    case Instruction.BXC:
                        ExecuteBxc(computer);
                        break;
                    case Instruction.OUT:
                        ExecuteOut(computer, strBuilder);
                        break;
                    case Instruction.BDV:
                        ExecuteBdv(computer);
                        break;
                    case Instruction.CDV:
                        ExecuteCdv(computer);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        public void MovePointer(int offset)
        {
            if (ProgramPointer + offset < 0 || ProgramPointer + offset >= Program.Length)
            {
                Ready = false;
                return;
            }
            ProgramPointer += offset;
        }

        public void JumpTo(int position)
        {
            if(position < 0 || position >= Program.Length)
            {
                Ready = false;
                return;
            }
            ProgramPointer = position;
        }

        public int ReadNext()
        {
            if (ProgramPointer >= Program.Length)
            {
                Ready = false;
                return -1;
            }
            int instruction = Program[ProgramPointer];
            MovePointer(1);
            return instruction;
        }
        
        public static void ExecuteCdv(Computer computer)
        {
            PerformDv(computer, result => computer.RegC = result);
        }

        public static void ExecuteBdv(Computer computer)
        {
            PerformDv(computer, result => computer.RegB = result);
        }

        public static void ExecuteOut(Computer computer, StringBuilder strBuilder)
        {
            var operand = GetComboOperand(computer);
            var modulo8 = operand % 8;
            if (strBuilder.Length == 0)
            {
                strBuilder.Append(modulo8);
            }
            else
            {
                strBuilder.Append($",{modulo8}");
            }
        }

        public static void ExecuteBxc(Computer computer)
        {
            // Read operand but ignore it
            var operand = computer.ReadNext();
            var xorResult = computer.RegB ^ computer.RegC;
            computer.RegB = xorResult;
        }

        public static void ExecuteJnz(Computer computer)
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

        public static void ExecuteBst(Computer computer)
        {
            var operand = GetComboOperand(computer);
            var modulo8 = operand % 8;
            computer.RegB = modulo8;
        }

        public static void ExecuteBxl(Computer computer)
        {
            var operand = GetLiteral(computer);
            var xorResult = computer.RegB ^ operand;
            computer.RegB = xorResult;
        }

        public static void ExecuteAdv(Computer computer)
        {
            PerformDv(computer, result => computer.RegA = result);
        }

        private static void PerformDv(Computer computer, Action<int> action)
        {
            var numerator = computer.RegA;
            int actualOperand = GetComboOperand(computer);
            var denominator = Math.Pow(2, actualOperand);

            var result = (int)Math.Floor(numerator / denominator);
            action(result);
        }

        private static int GetComboOperand(Computer computer)
        {
            var comboOperand = computer.ReadNext();
            var actualOperand = ResolveComboOperand(computer, comboOperand);
            return actualOperand;
        }

        public static int GetLiteral(Computer computer, bool isForJump = false)
        {
            var literal = computer.Program[computer.ProgramPointer];

            if (!isForJump)
            {
                computer.MovePointer(1);
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
    }
}