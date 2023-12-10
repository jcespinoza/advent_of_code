using Advent23.Days.Day09;
using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day09Test : TestEngine<Day09Solver, HistoryLine[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 1969958987; //NOT 1_969_958_228;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day09Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "0 3 6 9 12 15",
                    "1 3 6 10 15 21",
                    "10 13 16 21 30 45",
                ],
                Result = 114,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "2 5 7 8 23",
                        "1 3 6 10 15 21",
                        "10 13 16 21 30 45",
                    ],
                }  
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