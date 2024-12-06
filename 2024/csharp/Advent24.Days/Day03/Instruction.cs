namespace Advent24.Days.Day03
{
    public enum InstructionType { Do, Dont, Mul };
    public record Instruction(InstructionType Type);
    public record MulOperation(int OperandA, int OperandB) : Instruction(InstructionType.Mul);
    public record DoInstruction() : Instruction(InstructionType.Do);
    public record DontInstruction() : Instruction(InstructionType.Dont);
}
