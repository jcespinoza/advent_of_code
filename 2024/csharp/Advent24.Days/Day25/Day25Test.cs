using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day25Test : TestEngine<Day25Solver, List<Schematic>, long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 3127;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day25Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "#####",
                    ".####",
                    ".####",
                    ".####",
                    ".#.#.",
                    ".#...",
                    ".....",
                    "",
                    "#####",
                    "##.##",
                    ".#.##",
                    "...##",
                    "...#.",
                    "...#.",
                    ".....",
                    "",
                    ".....",
                    "#....",
                    "#....",
                    "#...#",
                    "#.#.#",
                    "#.###",
                    "#####",
                    "",
                    ".....",
                    ".....",
                    "#.#..",
                    "###..",
                    "###.#",
                    "###.#",
                    "#####",
                    "",
                    ".....",
                    ".....",
                    ".....",
                    "#....",
                    "#.#..",
                    "#.#.#",
                    "#####",
                ],
                Result = 3,
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