using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day01Test : TestEngine<Day01Solver, int[][], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 3714264L;
        private const long EXPECTED_SOLUTION_PART_2 = 18805872L;

        public Day01Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                        "3   4",
                        "4   3",
                        "2   5",
                        "1   3",
                        "3   9",
                        "3   3",                    
                ],
                Result = 11,
            },
            Examples = [
            ],
            Solution = EXPECTED_SOLUTION_PART_1,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                        "3   4",
                        "4   3",
                        "2   5",
                        "1   3",
                        "3   9",
                        "3   3",
                ],
                Result = 31,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}