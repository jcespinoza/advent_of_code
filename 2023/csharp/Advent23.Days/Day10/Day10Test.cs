using Advent23.Days.Day10;
using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day10Test : TestEngine<Day10Solver, PipeMap, long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 6_923;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day10Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    ".....",
                    ".S-7.",
                    ".|.|.",
                    ".L-J.",
                    ".....",
                ],
                Result = 4,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "....",
                        ".S7.",
                        ".LJ.",
                        "....",
                    ],
                    Result = 2,
                },
                new()
                {
                    RawInput = [
                        ".....",
                        ".S-7.",
                        ".L-J.",
                        ".....",
                    ],
                    Result = 3,
                },
                new()
                {
                    RawInput = [
                        "..F7.",
                        ".FJ|.",
                        "SJ.L7",
                        "|F--J",
                        "LJ...",
                    ],
                    Result = 8,
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