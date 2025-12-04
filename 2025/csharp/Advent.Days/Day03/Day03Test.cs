using AdventOfCode.Commons;
using dotenv.net;

namespace Advent.Days
{
    public class Day03Test : TestEngine<Day03Solver, BatteryBank[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 17155;
        private const long EXPECTED_SOLUTION_PART_2 = 169685670469164;

        public Day03Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "987654321111111",
                    "811111111111119",
                    "234234234234278",
                    "818181911112111",
                ],
                Result = 357,
            },
            Examples = [
                new () {
                    RawInput = ["987654321111111"],
                    Result = 98
                },
                new () {
                    RawInput = ["811111111111119"],
                    Result = 89
                },
                new () {
                    RawInput = ["234234234234278"],
                    Result = 78
                },
                new () {
                    RawInput = ["818181911112111"],
                    Result = 92
                }
            ],
            Solution = EXPECTED_SOLUTION_PART_1,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "987654321111111",
                    "811111111111119",
                    "234234234234278",
                    "818181911112111",
                ],
                Result = 3121910778619,
            },
            Examples = [
                new () {
                    RawInput = ["987654321111111"],
                    Result = 987654321111
                },
                new () {
                    RawInput = ["811111111111119"],
                    Result = 811111111119
                },
                new () {
                    RawInput = ["234234234234278"],
                    Result = 434234234278
                },
                new () {
                    RawInput = ["818181911112111"],
                    Result = 888911112111
                }
            ],
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}