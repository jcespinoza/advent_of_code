using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day13Solver : Solver<MachineConfig[], long>
    {
        public Day13Solver() : base(2024, 13) { }

        public override MachineConfig[] ParseInput(IEnumerable<string> input)
        {
            List<MachineConfig> machineConfigs = [];
            var lines = input.ToArray();
            for (int index = 0; index < lines.Length; index++)
            {
                var line = lines[index];
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (!line.StartsWith("Button A")) break;

                var buttonAconfig = line.Split(":")[1].Trim();
                var buttonBconfig = lines[index + 1].Split(":")[1].Trim();
                var prizeConfig = lines[index + 2].Split(":")[1].Trim();

                var buttonAtext = buttonAconfig.Split(", ");
                var buttonBtext = buttonBconfig.Split(", ");
                var prizeText = prizeConfig.Split(", ");

                var buttonAX = int.Parse(buttonAtext[0].Split("+")[1]);
                var buttonAY = int.Parse(buttonAtext[1].Split("+")[1]);
                MachineButton buttonA = new(buttonAX, buttonAY);

                var buttonBX = int.Parse(buttonBtext[0].Split("+")[1]);
                var buttonBY = int.Parse(buttonBtext[1].Split("+")[1]);
                MachineButton buttonB = new(buttonBX, buttonBY);

                var prizeX = int.Parse(prizeText[0].Split("=")[1]);
                var prizeY = int.Parse(prizeText[1].Split("=")[1]);

                MachineConfig machineConfig = new(prizeX, prizeY, buttonA, buttonB);
                machineConfigs.Add(machineConfig);
            }

            return machineConfigs.ToArray();
        }


        public override long PartOne(MachineConfig[] input)
        {
            throw new NotImplementedException();
        }

        public override long PartTwo(MachineConfig[] input)
        {
            throw new NotImplementedException();
        }
    }
}