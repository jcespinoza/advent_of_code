using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day11Test : TestEngine<Day11Solver, long[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 199982L;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day11Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "125 17"
                ],
                Result = 55312,
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
                ],
                Result = 8,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}