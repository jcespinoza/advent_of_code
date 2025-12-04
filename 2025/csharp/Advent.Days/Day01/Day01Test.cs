using AdventOfCode.Commons;
using dotenv.net;

namespace Advent.Days
{
    public class Day01Test : TestEngine<Day01Solver, Rotation[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 989;
        private const long EXPECTED_SOLUTION_PART_2 = 5941;

        public Day01Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "L68", "L30", "R48", "L5", "R60", "L55", "L1", "L99", "R14", "L82",
                ],
                Result = 3,
            },
            Examples = [
            ],
            Solution = EXPECTED_SOLUTION_PART_1,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "L68", "L30", "R48", "L5", "R60", "L55", "L1", "L99", "R14", "L82",
                ],
                Result = 6,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "R1000",
                    ],
                    Result = 10,
                },
            ],
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}