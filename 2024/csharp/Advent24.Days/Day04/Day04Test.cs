using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day04Test : TestEngine<Day04Solver, char[][], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 123;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day04Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "MMMSXXMASM",
                    "MSAMXMSMSA",
                    "AMXSXMAAMM",
                    "MSAMASMSMX",
                    "XMASAMXAMM",
                    "XXAMMXXAMA",
                    "SMSMSASXSS",
                    "SAXAMASAAA",
                    "MAMMMXMMMM",
                    "MXMXAXMASX",
                ],
                Result = 18,
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