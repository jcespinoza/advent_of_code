using Advent24.Days.Day03;
using AdventOfCode.Commons;
using System.Text;

namespace Advent24.Days
{
    public class OperationParser
    {
        public string Input { get; init; } = string.Empty;
        public char CurrentChar => Input[Pointer];
        public int Pointer { get; private set; }

        public OperationParser(string input)
        {
            Input = input;
            Pointer = 0;
        }

        public bool HasNext()
        {
            return Pointer < Input.Length;
        }

        public void AdvancePointer()
        {
            if(HasNext())
            {
                Pointer++;
            }
        }

        public Result<int,string> ReadNumber()
        {
            StringBuilder number = new();
            while (HasNext() && char.IsDigit(CurrentChar))
            {
                number.Append(CurrentChar);
                AdvancePointer();
            }
            if (number.Length == 0)
            {
                return Result<int,string>.Failure("No number to read");
            }

            return Result<int,string>.Success(int.Parse(number.ToString()));
        }

        public Result<Instruction, string> ReadMulOperation()
        {
            if (HasNext() && CurrentChar == 'm')
            {
                AdvancePointer();
                if (HasNext() && CurrentChar == 'u')
                {
                    AdvancePointer();
                    if (HasNext() && CurrentChar == 'l')
                    {
                        AdvancePointer();
                        if (HasNext() && CurrentChar == '(')
                        {
                            AdvancePointer();
                            var operandA = ReadNumber();
                            if (operandA.IsFailure)
                            {
                                return Result<Instruction, string>.Failure(operandA.Error);
                            }
                            if (HasNext() && CurrentChar == ',')
                            {
                                AdvancePointer();
                                var operandB = ReadNumber();
                                if (operandB.IsFailure)
                                {
                                    return Result<Instruction, string>.Failure(operandB.Error);
                                }
                                if (HasNext() && CurrentChar == ')')
                                {
                                    AdvancePointer();
                                    return Result<Instruction, string>.Success(new MulOperation(operandA.Value, operandB.Value));
                                }
                                return Result<Instruction, string>.Failure("Expected closing parenthesis");
                            }
                            return Result<Instruction, string>.Failure("Expected comma");
                        }
                        return Result<Instruction, string>.Failure("Expected opening parenthesis");
                    }
                    return Result<Instruction, string>.Failure("Expected 'l'");
                }
                return Result<Instruction, string>.Failure("Expected 'u'");
            }
            return Result<Instruction, string>.Failure("Expected 'm'");
        }

        // Matches either "do()" or "don't()" in a single pass
        // Assigns the InstructionType depending on which one it matched
        // If successful, returns a DoInstruction or DontInstruction
        public Result<Instruction, string> ReadDoInstruction()
        {
            InstructionType type = InstructionType.Do;
            if(HasNext() && CurrentChar == 'd')
            {
                AdvancePointer();
                if (HasNext() && CurrentChar == 'o')
                {
                    AdvancePointer();
                    // attempt to find an "n" which means we need to match a "don't()"
                    if (HasNext() && CurrentChar == 'n')
                    {
                        AdvancePointer();
                        type = InstructionType.Dont;
                        if (HasNext() && CurrentChar == '\'')
                        {
                            AdvancePointer();
                            if (HasNext() && CurrentChar == 't')
                            {
                                AdvancePointer();
                            }
                            else
                            {
                                return Result<Instruction, string>.Failure("Expected 't'");
                            }
                        }
                        else
                        {
                            return Result<Instruction, string>.Failure("Expected single quote");
                        }
                    }
                    if(HasNext() && CurrentChar == '(')
                    {
                        AdvancePointer();
                        if (HasNext() && CurrentChar == ')')
                        {
                            AdvancePointer();
                            if (type == InstructionType.Dont)
                            {
                                return Result<Instruction, string>.Success(new DontInstruction());
                            }
                            return Result<Instruction, string>.Success(new DoInstruction());                            
                        }
                        return Result<Instruction, string>.Failure("Expected ')'");
                    }
                    return Result<Instruction, string>.Failure("Expected '('");
                }
                return Result<Instruction, string>.Failure("Expected 'o'");
            }
            return Result<Instruction, string>.Failure("Expected 'd'");
        }

        public Result<Instruction, string> ReadInstruction()
        {
            if(HasNext() && CurrentChar == 'm')
            {
                return ReadMulOperation();
            }else if(HasNext() && CurrentChar == 'd')
            {
                return ReadDoInstruction();
            }
            return Result<Instruction, string>.Failure("Expected 'm' or 'd'");
        }

        public List<Instruction> ReadOperations()
        {
            List<Instruction> operations = [];
            while (HasNext())
            {
                var operation = ReadMulOperation();
                if (operation.IsSuccess)
                {
                    operations.Add(operation.Value);
                }
                else
                {
                    AdvancePointer();
                }
            }
            return operations;
        }

        public List<Instruction> ReadInstructions()
        {
            List<Instruction> operations = [];
            while (HasNext())
            {
                var operation = ReadInstruction();
                if (operation.IsSuccess)
                {
                    operations.Add(operation.Value);
                }
                else
                {
                    AdvancePointer();
                }
            }
            return operations;
        }
    }
}