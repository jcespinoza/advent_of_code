using Advent23.Days.Day04;
using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day04Test : TestEngine<Day04Solver, ScratchCard[], long>
    {
        private const int EXPECTED_SOLUTION_PART_1 = 20107;
        private const int EXPECTED_SOLUTION_PART_2 = 123;

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
                    "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
                    "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19",
                    "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1",
                    "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83",
                    "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36",
                    "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11",
                ],
                Result = 13,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
                    ],
                    Input = [
                        new ScratchCard
                        {
                            Number = 1,
                            Winners = [41, 48, 83, 86, 17],
                            Possesion = [83, 86, 6, 31, 17, 9, 48, 53],
                        }
                    ],
                    Result = 8,
                }
            ],
            Solution = EXPECTED_SOLUTION_PART_1,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = true,
            Example = new()
            {
                Result = 8,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}