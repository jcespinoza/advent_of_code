using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day18Test : SteppedTestEngine<Day18Solver, (int,int)[], (int,int)[], long, (int,int)>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 298;
        private (int,int) EXPECTED_SOLUTION_PART_2 = (52, 32);

        public Day18Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "5,4",
                    "4,2",
                    "4,5",
                    "3,0",
                    "2,1",
                    "6,3",
                    "2,4",
                    "1,5",
                    "0,6",
                    "3,3",
                    "2,6",
                    "5,1",
                    "1,2",
                    "5,5",
                    "2,5",
                    "6,5",
                    "1,4",
                    "0,4",
                    "6,4",
                    "1,1",
                    "6,1",
                    "1,0",
                    "0,5",
                    "1,6",
                    "2,0",
                ],
                Result = 22,
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
                    "5,4",
                    "4,2",
                    "4,5",
                    "3,0",
                    "2,1",
                    "6,3",
                    "2,4",
                    "1,5",
                    "0,6",
                    "3,3",
                    "2,6",
                    "5,1",
                    "1,2",
                    "5,5",
                    "2,5",
                    "6,5",
                    "1,4",
                    "0,4",
                    "6,4",
                    "1,1",
                    "6,1",
                    "1,0",
                    "0,5",
                    "1,6",
                    "2,0",
                ],
                ResultTwo = (6,1),
            },
            SolutionTwo = (EXPECTED_SOLUTION_PART_2),
        };
    }
}