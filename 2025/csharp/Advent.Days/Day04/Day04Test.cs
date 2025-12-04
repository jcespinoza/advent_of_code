using AdventOfCode.Commons;
using dotenv.net;

namespace Advent.Days
{
    public class Day04Test : TestEngine<Day04Solver, PaperWarehouse, long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 1553;
        private const long EXPECTED_SOLUTION_PART_2 = 8442;

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
                    "..@@.@@@@.",
                    "@@@.@.@.@@",
                    "@@@@@.@.@@",
                    "@.@@@@..@.",
                    "@@.@@@@.@@",
                    ".@@@@@@@.@",
                    ".@.@.@.@@@",
                    "@.@@@.@@@@",
                    ".@@@@@@@@.",
                    "@.@.@@@.@.",
                ],
                Result = 13,
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
                    "..@@.@@@@.",
                    "@@@.@.@.@@",
                    "@@@@@.@.@@",
                    "@.@@@@..@.",
                    "@@.@@@@.@@",
                    ".@@@@@@@.@",
                    ".@.@.@.@@@",
                    "@.@@@.@@@@",
                    ".@@@@@@@@.",
                    "@.@.@@@.@.",
                ],
                Result = 43,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}