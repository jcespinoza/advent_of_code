﻿using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day16Test : TestEngine<Day16Solver, char[][], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 66404L;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day16Test()
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
                Result = 64,
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
                    Result = 45,
                },
            ],
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}