using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day15Test : TestEngine<Day15Solver, string, long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 513_214;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day15Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7"
                ],
                Result = 1320,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "HASH"
                    ],
                    Result = 52,
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
                ],
                Result = 8,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}