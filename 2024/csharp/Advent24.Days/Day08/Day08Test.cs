using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day08Test : TestEngine<Day08Solver, char[][], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 123;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day08Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "............",
                    "........0...",
                    ".....0......",
                    ".......0....",
                    "....0.......",
                    "......A.....",
                    "............",
                    "............",
                    "........A...",
                    ".........A..",
                    "............",
                    "............",
                ],
                Result = 14,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "..........",
                        "..........",
                        "..........",
                        "....a.....",
                        "..........",
                        ".....a....",
                        "..........",
                        "..........",
                        "..........",
                        "..........",
                    ],
                    Result = 2,
                },
                new()
                {
                    RawInput = [
                        "..........",
                        "..........",
                        "..........",
                        "....a.....",
                        "........a.",
                        ".....a....",
                        "..........",
                        "..........",
                        "..........",
                        "..........",
                    ],
                    Result = 4,
                },
                new()
                {
                    RawInput = [
                        "..........",
                        "..........",
                        "..........",
                        "....a.....",
                        "........a.",
                        ".....a....",
                        "..........",
                        "......A...",
                        "..........",
                        "..........",
                    ],
                    Result = 4,
                },
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