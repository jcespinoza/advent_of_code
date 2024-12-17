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
                index += 2;
            }

            return [.. machineConfigs];
        }


        public override long PartOne(MachineConfig[] configs)
        {
            const int A_PRICE = 3;
            const int B_PRICE = 1;

            /* Upon analysis; the solution is to solve the system of equations and use that to determine the first press-count that will get to the given coordinates
             The system of equations is as follows:
             AxS + BxT = Px
             AyS + ByT = Py
                where:
                    Ax, Ay are the x and y offsets of button A
                    Bx, By are the x and y offsets of button B
                    Px, Py are the x and y coordinates of the prize
                    S, T are the number of times button A and B are pressed respectively

                We can solve this system of equations by multiplying the first equation by By and the second by Bx and subtracting the two equations to get:
                (AxBy - AyBx)S = (PxBy - PyBx)
                S = (PxBy - PyBx) / (AxBy - AyBx)

                When computed we can use the value of S in the equation for T:
                T = (Px - AxS) / Bx

                We can then use the first equation to determine the number of presses needed to get to the prize coordinates
             */

            long tokensUsed = 0;
            foreach (var config in configs)
            {
                var Ax = config.A.XOffset;
                var Ay = config.A.YOffset;
                var Bx = config.B.XOffset;
                var By = config.B.YOffset;
                var Px = config.PrizeX;
                var Py = config.PrizeY;
                var S = (Px * By - Py * Bx) / (Ax * By - Ay * Bx);
                var T = (Px - Ax * S) / Bx;

                // Discard solutions that are not integers
                if (S % 1 != 0 || T % 1 != 0) continue;
                // Discard solutions that represent more than 100 button presses
                if (S > 100 || T > 100) continue;

                // Now mutiple the butto presses by the price of each
                var costA = S * A_PRICE;
                var costB = T * B_PRICE;
                tokensUsed += costA + costB;
            }

            return tokensUsed;
        }

        public override long PartTwo(MachineConfig[] input)
        {
            throw new NotImplementedException();
        }
    }
}