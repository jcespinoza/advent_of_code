using AdventOfCode.Commons;

namespace Advent.Days
{
    public class Day04Solver : Solver<PaperWarehouse, long>
    {
        public Day04Solver() : base(AocConstants.Year, 04) { }

        public override PaperWarehouse ParseInput(IEnumerable<string> input)
            => PaperWarehouse.FromLines(input);

        public override long PartOne(PaperWarehouse input)
        {
            return input.CountAccessibleRolls();
        }

        public override long PartTwo(PaperWarehouse input)
        {
            var warehouse = input.Clone();
            warehouse.RemoveAccessibleRollsQueue();
            return warehouse.CountRemovedRolls();
        }
    }
}