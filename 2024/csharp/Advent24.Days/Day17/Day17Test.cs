using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day17Test : TestEngine<Day17Solver, Computer, string>
    {
        private const string EXPECTED_SOLUTION_PART_1 = "3,4,3,1,7,6,5,6,0";
        private const string EXPECTED_SOLUTION_PART_2 = "123";

        public Day17Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "Register A: 729",
                    "Register B: 0",
                    "Register C: 0",
                    "",
                    "Program: 0,1,5,4,3,0",
                ],
                Result = "4,6,3,5,6,3,5,2,1,0",
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
                Result = "8",
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}