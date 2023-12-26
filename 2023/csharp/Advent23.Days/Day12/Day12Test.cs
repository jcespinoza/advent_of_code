using Advent23.Days.Day12;
using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day12Test : TestEngine<Day12Solver, ConditionRecord[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 7_771;
        private const long EXPECTED_SOLUTION_PART_2 = 10_861_030_975_833;

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
                    "???.### 1,1,3",
                    ".??..??...?##. 1,1,3",
                    "?#?#?#?#?#?#?#? 1,3,1,6",
                    "????.#...#... 4,1,1",
                    "????.######..#####. 1,6,5",
                    "?###???????? 3,2,1",
                ],
                Result = 21,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "???.### 1,1,3",
                    ],
                    Result = 1,
                },
                new()
                {
                    RawInput = [
                        ".??..??...?##. 1,1,3",
                    ],
                    Result = 4,
                },
                new()
                {
                    RawInput = [
                        "?#?#?#?#?#?#?#? 1,3,1,6",
                    ],
                    Result = 1,
                },
                new()
                {
                    RawInput = [
                        "????.######..#####. 1,6,5",
                    ],
                    Result = 4,
                },
                new()
                {
                    RawInput = [
                        "????.#...#... 4,1,1",
                    ],
                    Result = 1,
                },
                new()
                {
                    RawInput = [
                        "?###???????? 3,2,1",
                    ],
                    Result = 10,
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
                    "???.### 1,1,3",
                    ".??..??...?##. 1,1,3",
                    "?#?#?#?#?#?#?#? 1,3,1,6",
                    "????.#...#... 4,1,1",
                    "????.######..#####. 1,6,5",
                    "?###???????? 3,2,1",
                ],
                Result = 525152,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "???.### 1,1,3",
                    ],
                    Result = 1,
                },
                new()
                {
                    RawInput = [
                        ".??..??...?##. 1,1,3",
                    ],
                    Result = 16384,
                },
                new()
                {
                    RawInput = [
                        "?#?#?#?#?#?#?#? 1,3,1,6",
                    ],
                    Result = 1,
                },
                new()
                {
                    RawInput = [
                        "????.######..#####. 1,6,5",
                    ],
                    Result = 2500,
                },
                new()
                {
                    RawInput = [
                        "????.#...#... 4,1,1",
                    ],
                    Result = 16,
                },
                new()
                {
                    RawInput = [
                        "?###???????? 3,2,1",
                    ],
                    Result = 506250,
                },
            ],
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}