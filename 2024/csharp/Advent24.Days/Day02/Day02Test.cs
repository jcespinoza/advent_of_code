using Advent24.Days.Day02;
using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day02Test : TestEngine<Day02Solver, Report[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 631;
        private const long EXPECTED_SOLUTION_PART_2 = 665;

        public Day02Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "7 6 4 2 1",
                    "1 2 7 8 9",
                    "9 7 6 2 1",
                    "1 3 2 4 5",
                    "8 6 4 4 1",
                    "1 3 6 7 9",
                ],
                Result = 2,
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
                    "7 6 4 2 1",
                    "1 2 7 8 9",
                    "9 7 6 2 1",
                    "1 3 2 4 5",
                    "8 6 4 4 1",
                    "1 3 6 7 9",
                ],
                Result = 4,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}