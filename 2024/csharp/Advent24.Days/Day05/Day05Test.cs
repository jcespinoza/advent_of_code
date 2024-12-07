using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day05Test : TestEngine<Day05Solver, PrintingConfig, long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 123;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day05Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "47|53",
                    "97|13",
                    "97|61",
                    "97|47",
                    "75|29",
                    "61|13",
                    "75|53",
                    "29|13",
                    "97|29",
                    "53|29",
                    "61|53",
                    "97|53",
                    "61|29",
                    "47|13",
                    "75|47",
                    "97|75",
                    "47|61",
                    "75|61",
                    "47|29",
                    "75|13",
                    "53|13",
                    "",
                    "75,47,61,53,29",
                    "97,61,53,29,13",
                    "75,29,13",
                    "75,97,47,61,53",
                    "61,13,29",
                    "97,13,75,29,47",
                ],
                Result = 143,
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
                ],
                Result = 8,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}