using Advent23.Days.Day08;
using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day08Test : TestEngine<Day08Solver, DesertMap, long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 13_939;
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
                    "RL",
                    "",
                    "AAA = (BBB, CCC)",
                    "BBB = (DDD, EEE)",
                    "CCC = (ZZZ, GGG)",
                    "DDD = (DDD, DDD)",
                    "EEE = (EEE, EEE)",
                    "GGG = (GGG, GGG)",
                    "ZZZ = (ZZZ, ZZZ)",
                ],
                Result = 2,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "LLR",
                        "",
                        "AAA = (BBB, BBB)",
                        "BBB = (AAA, ZZZ)",
                        "ZZZ = (ZZZ, ZZZ)",
                    ],
                    Result = 6,
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
                    "RL",
                    "",
                    "AAA = (BBB, CCC)",
                    "BBB = (DDD, EEE)",
                    "CCC = (ZZZ, GGG)",
                    "DDD = (DDD, DDD)",
                    "EEE = (EEE, EEE)",
                    "GGG = (GGG, GGG)",
                    "ZZZ = (ZZZ, ZZZ)",
                ],
                Result = 8,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}