using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day06Test : TestEngine<Day06Solver, char[][], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 5461;
        private const long EXPECTED_SOLUTION_PART_2 = 1836;

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
                    "....#.....",
                    ".........#",
                    "..........",
                    "..#.......",
                    ".......#..",
                    "..........",
                    ".#..^.....",
                    "........#.",
                    "#.........",
                    "......#...",
                ],
                Result = 41,
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
                    "....#.....",
                    ".........#",
                    "..........",
                    "..#.......",
                    ".......#..",
                    "..........",
                    ".#..^.....",
                    "........#.",
                    "#.........",
                    "......#...",
                ],
                Result = 6,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}