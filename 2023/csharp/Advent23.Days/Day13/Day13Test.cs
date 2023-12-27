using Advent23.Days.Day13;
using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day13Test : TestEngine<Day13Solver, TerrainPattern[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 123;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day13Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "#.##..##.",
                    "..#.##.#.",
                    "##......#",
                    "##......#",
                    "..#.##.#.",
                    "..##..##.",
                    "#.#.##.#.",
                    "",
                    "#...##..#",
                    "#....#..#",
                    "..##..###",
                    "#####.##.",
                    "#####.##.",
                    "..##..###",
                    "#....#..#",
                ],
                Result = 405,
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