using Advent23.Days.Day11;
using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day11Test : TestEngine<Day11Solver, SpaceMap, long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 10_289_334;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day11Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new() {
                    RawInput = [
                    ".#.",
                    "...",
                    ".#.",
                /*
                    "..#..",
                    ".....",
                    ".....",
                    "..#..",
                            */
                    ],
                    Result = 3
                },
            Examples = [
                new()
                {
                    RawInput = [
                        "...#......",
                        ".......#..",
                        "#.........",
                        "..........",
                        "......#...",
                        ".#........",
                        ".........#",
                        "..........",
                        ".......#..",
                        "#...#.....",
                    ],
                    Result = 374,
                },
                new()
                {
                    RawInput = [
                    ".#.",
                        "...",
                        "..#",
                        /*
                            "..#.",
                            "....",
                            "....",
                            "...#",
                                    */
                    ],
                    Result = 4
                },
                new()
                {
                    RawInput = [
                    ".#.",
                    "#..",
                    "..#",
                    /*
                        "..#.",
                        "....",
                        "....",
                        "...#",
                                */
                    ],
                    Result = 2+3+3
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