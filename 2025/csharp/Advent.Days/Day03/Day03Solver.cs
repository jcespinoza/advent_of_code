using AdventOfCode.Commons;

namespace Advent.Days
{
    public class Day03Solver : Solver<BatteryBank[], long>
    {
        public Day03Solver() : base(AocConstants.Year, 03) { }

        public override BatteryBank[] ParseInput(IEnumerable<string> input)
            => input.Select(BatteryBank.From).ToArray();

        public override long PartOne(BatteryBank[] input)
        {
            var numberOfBatteries = 2;
            long totalCharge = 0;

            foreach (var bank in input)
            {
                var top = bank.TopNBatteries(numberOfBatteries);
                var chargeStr = string.Concat(top.Select(d => d.ToString()));
                var charge = long.Parse(chargeStr);
                totalCharge += charge;
            }

            return totalCharge;
        }

        public override long PartTwo(BatteryBank[] input)
        {
            var numberOfBatteries = 12;
            long totalCharge = 0;

            foreach (var bank in input)
            {
                var top = bank.TopNBatteries(numberOfBatteries);
                var chargeStr = string.Concat(top.Select(d => d.ToString()));
                var charge = long.Parse(chargeStr);
                totalCharge += charge;
            }

            return totalCharge;
        }
    }
}