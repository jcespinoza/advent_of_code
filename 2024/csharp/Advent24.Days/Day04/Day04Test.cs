using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day04Test : TestEngine<Day04Solver, char[][], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 2618;
        private const long EXPECTED_SOLUTION_PART_2 = 2011;

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
                new(){
                    RawInput = [
                        "..S..X..S",
                        "..SAMX.A.",
                        "...AM.M..",
                        "..SA.X..."
                        ],
                    Result = 4
                    }
            ],
            Solution = EXPECTED_SOLUTION_PART_1,
        };

        public override Puzzle PartTwo => new()
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
                Result = 9,
            },
            Examples = [
                new(){
                    RawInput = [
                        "M.S",
                        ".A.",
                        "M.S",
                    ],
                    Result = 1,
                },
                new(){
                    RawInput = [
                        "S.M",
                        ".A.",
                        "S.M",
                    ],
                    Result = 1,
                },
                new(){
                    RawInput = [
                        "M.M",
                        ".A.",
                        "S.S",
                    ],
                    Result = 1,
                },
                new(){
                    RawInput = [
                        "S.S",
                        ".A.",
                        "M.M",
                    ],
                    Result = 1,
                }
            ],
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}