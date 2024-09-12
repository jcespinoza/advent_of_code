using Advent23.Days.Day14;
using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day14Test : TestEngine<Day14Solver, Dish, long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 103_614;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day14Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "O....#....",
                    "O.OO#....#",
                    ".....##...",
                    "OO.#O....O",
                    ".O.....O#.",
                    "O.#..O.#.#",
                    "..O..#O..O",
                    ".......O..",
                    "#....###..",
                    "#OO..#....",
                ],
                Result = 136,
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