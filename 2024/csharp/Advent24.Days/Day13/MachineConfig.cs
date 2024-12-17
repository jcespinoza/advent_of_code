namespace Advent24.Days
{
    public record MachineConfig(int PrizeX, int PrizeY, MachineButton A, MachineButton B);
    public record MachineButton(int XOffset, int YOffset);
}