using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day15Test : TestEngine<Day15Solver, Warehouse, long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 1412971L;
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
                    "##########",
                    "#..O..O.O#",
                    "#......O.#",
                    "#.OO..O.O#",
                    "#..O@..O.#",
                    "#O#..O...#",
                    "#O..O..O.#",
                    "#.OO.O.OO#",
                    "#....O...#",
                    "##########",
                    "",
                    "<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^",
                    "vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v",
                    "><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<",
                    "<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^",
                    "^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><",
                    "^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^",
                    ">^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^",
                    "<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>",
                    "^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>",
                    "v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^",
                ],
                Result = 10092,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "########",
                        "#..O.O.#",
                        "##@.O..#",
                        "#...O..#",
                        "#.#.O..#",
                        "#...O..#",
                        "#......#",
                        "########",
                        "",
                        "<^^>>>vv<v>>v<<",
                    ],
                    Result = 2028,
                },
            ],
            Solution = EXPECTED_SOLUTION_PART_1,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                        "########",
                        "#..O.O.#",
                        "##@.O..#",
                        "#...O..#",
                        "#.#.O..#",
                        "#...O..#",
                        "#......#",
                        "########",
                        "",
                        "<^^>>>vv<v>>v<<",
                ],
                Result = 9021,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}