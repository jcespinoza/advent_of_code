using Advent23.Days.Day06;
using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day06Test : TestEngine<Day06Solver, RaceRecord[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 2_612_736L;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day06Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "Time:      7  15   30",
                    "Distance:  9  40  200",
                ],
                Input = [
                    new()
                    {
                        Time = 7,
                        Distance = 9,
                    },
                    new()
                    {
                        Time = 15,
                        Distance = 40,
                    },
                    new()
                    {
                        Time = 30,
                        Distance = 200,
                    },
                ],
                Result = 288,
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
                Result = 8,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}