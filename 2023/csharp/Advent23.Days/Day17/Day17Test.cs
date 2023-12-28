using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day17Test : TestEngine<Day17Solver, object[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 123;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

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
                    "2413432311323",
                    "3215453535623",
                    "3255245654254",
                    "3446585845452",
                    "4546657867536",
                    "1438598798454",
                    "4457876987766",
                    "3637877979653",
                    "4654967986887",
                    "4564679986453",
                    "1224686865563",
                    "2546548887735",
                    "4322674655533",
                ],
                Result = 102,
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