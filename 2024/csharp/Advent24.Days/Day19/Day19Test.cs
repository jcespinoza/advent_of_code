using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day19Test : TestEngine<Day19Solver, OnsenWarehouse, long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 344;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day19Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "r, wr, b, g, bwu, rb, gb, br",
                    "",
                    "brwrr",
                    "bggr",
                    "gbbr",
                    "rrbgbr",
                    "ubwu",
                    "bwurrg",
                    "brgr",
                    "bbrgwb",
                ],
                Result = 6,
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
                    "r, wr, b, g, bwu, rb, gb, br",
                    "",
                    "brwrr",
                    "bggr",
                    "gbbr",
                    "rrbgbr",
                    "ubwu",
                    "bwurrg",
                    "brgr",
                    "bbrgwb",
                ],
                Result = 16,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}