using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day13Test : TestEngine<Day13Solver, MachineConfig[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 123;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day13Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "Button A: X+94, Y+34",
                    "Button B: X+22, Y+67",
                    "Prize: X=8400, Y=5400",
                    "",
                    "Button A: X+26, Y+66",
                    "Button B: X+67, Y+21",
                    "Prize: X=12748, Y=12176",
                    "",
                    "Button A: X+17, Y+86",
                    "Button B: X+84, Y+37",
                    "Prize: X=7870, Y=6450",
                    "",
                    "Button A: X+69, Y+23",
                    "Button B: X+27, Y+71",
                    "Prize: X=18641, Y=10279",
                ],
                Result = 480,
            },
            Examples = [
            ],
            Solution = EXPECTED_SOLUTION_PART_1,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = true,
            Example = new()
            {
                RawInput = [
                ],
                Result = 8,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}