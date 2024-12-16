using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day12Test : TestEngine<Day12Solver, char[][], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 1449902L;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day12Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "RRRRIICCFF",
                    "RRRRIICCCF",
                    "VVRRRCCFFF",
                    "VVRCCCJFFF",
                    "VVVVCJJCFE",
                    "VVIVCCJJEE",
                    "VVIIICJJEE",
                    "MIIIIIJJEE",
                    "MIIISIJEEE",
                    "MMMISSJEEE",
                ],
                Result = 1930,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "AAAA",
                        "BBCD",
                        "BBCC",
                        "EEEC",
                    ],
                    Result = 140,
                },
                new()
                {
                    RawInput = [
                        "OOOOO",
                        "OXOXO",
                        "OOOOO",
                        "OXOXO",
                        "OOOOO",
                    ],
                    Result = 772,
                },
            ],
            Solution = EXPECTED_SOLUTION_PART_1,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "AAAAAA",
                    "AAABBA",
                    "AAABBA",
                    "ABBAAA",
                    "ABBAAA",
                    "AAAAAA",
                ],
                Result = 368,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "AAAA",
                        "BBCD",
                        "BBCC",
                        "EEEC",
                    ],
                    Result = 80,
                },
                new()
                {
                    RawInput = [
                        "OOOOO",
                        "OXOXO",
                        "OOOOO",
                        "OXOXO",
                        "OOOOO",
                    ],
                    Result = 436,
                },
                new()
                {
                    RawInput = [
                        "EEEEE",
                        "EXXXX",
                        "EEEEE",
                        "EXXXX",
                        "EEEEE",
                    ],
                    Result = 236,
                },
            ],
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}