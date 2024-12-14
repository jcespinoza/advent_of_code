using AdventOfCode.Commons;
using dotenv.net;
using FluentAssertions;
using Xunit;

namespace Advent24.Days
{
    public class Day09Test : TestEngine<Day09Solver, int[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 6398252054886L;
        private const long EXPECTED_SOLUTION_PART_2 = 6415666220005L;

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
                    "2333133121414131402"
                ],
                Result = 1928,
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
                    "2333133121414131402"
                ],
                Result = 2858,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}