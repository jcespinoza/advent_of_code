using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day23Test : TestEngine<Day23Solver, (string, string)[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 1194;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day23Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "kh-tc",
                    "qp-kh",
                    "de-cg",
                    "ka-co",
                    "yn-aq",
                    "qp-ub",
                    "cg-tb",
                    "vc-aq",
                    "tb-ka",
                    "wh-tc",
                    "yn-cg",
                    "kh-ub",
                    "ta-co",
                    "de-co",
                    "tc-td",
                    "tb-wq",
                    "wh-td",
                    "ta-ka",
                    "td-qp",
                    "aq-cg",
                    "wq-ub",
                    "ub-vc",
                    "de-ta",
                    "wq-aq",
                    "wq-vc",
                    "wh-yn",
                    "ka-de",
                    "kh-ta",
                    "co-tc",
                    "wh-qp",
                    "tb-vc",
                    "td-yn",
                ],
                Result = 7,
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