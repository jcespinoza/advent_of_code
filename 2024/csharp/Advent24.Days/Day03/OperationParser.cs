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

        public Result<Operation, string> ReadOperation()
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
                                return Result<Operation, string>.Failure(operandA.Error);
                            }
                            if (HasNext() && CurrentChar == ',')
                            {
                                AdvancePointer();
                                var operandB = ReadNumber();
                                if (operandB.IsFailure)
                                {
                                    return Result<Operation, string>.Failure(operandB.Error);
                                }
                                if (HasNext() && CurrentChar == ')')
                                {
                                    AdvancePointer();
                                    return Result<Operation, string>.Success(new Operation(operandA.Value, operandB.Value));
                                }
                                return Result<Operation, string>.Failure("Expected closing parenthesis");
                            }
                            return Result<Operation, string>.Failure("Expected comma");
                        }
                        return Result<Operation, string>.Failure("Expected opening parenthesis");
                    }
                    return Result<Operation, string>.Failure("Expected 'l'");
                }
                return Result<Operation, string>.Failure("Expected 'u'");
            }
            return Result<Operation, string>.Failure("Expected 'm'");
        }

        public List<Operation> ReadOperations()
        {
            List<Operation> operations = [];
            while (HasNext())
            {
                var operation = ReadOperation();
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