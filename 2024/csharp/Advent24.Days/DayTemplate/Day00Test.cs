using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day00Test : TestEngine<Day00Solver, object[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 123;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day00Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "#################",
                    "#...#...#...#..E#",
                    "#.#.#.#.#.#.#.#.#",
                    "#.#.#.#...#...#.#",
                    "#.#.#.#.###.#.#.#",
                    "#...#.#.#.....#.#",
                    "#.#.#.#.#.#####.#",
                    "#.#...#.#.#.....#",
                    "#.#.#####.#.###.#",
                    "#.#.#.......#...#",
                    "#.#.###.#####.###",
                    "#.#.#...#.....#.#",
                    "#.#.#.#####.###.#",
                    "#.#.#.........#.#",
                    "#.#.#.#########.#",
                    "#S#.............#",
                    "#################",
                ],
                Result = 11048,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "###############",
                        "#.......#....E#",
                        "#.#.###.#.###.#",
                        "#.....#.#...#.#",
                        "#.###.#####.#.#",
                        "#.#.#.......#.#",
                        "#.#.#####.###.#",
                        "#...........#.#",
                        "###.#.#####.#.#",
                        "#...#.....#.#.#",
                        "#.#.#.###.#.#.#",
                        "#.....#...#.#.#",
                        "#.###.#.#.#.#.#",
                        "#S..#.....#...#",
                        "###############",
                    ],
                    Result = 7036,
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