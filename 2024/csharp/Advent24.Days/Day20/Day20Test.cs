﻿using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day20Test : SteppedTestEngine<Day20Solver, char[][], char[][], long, long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 1415;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day20Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "###############",
                    "#...#...#.....#",
                    "#.#.#.#.#.###.#",
                    "#S#...#.#.#...#",
                    "#######.#.#.###",
                    "#######.#.#...#",
                    "#######.#.###.#",
                    "###..E#...#...#",
                    "###.#######.###",
                    "#...###...#...#",
                    "#.#####.#.###.#",
                    "#.#...#.#.#...#",
                    "#.#.#.#.#.#.###",
                    "#...#...#...###",
                    "###############",
                ],
                Result = 0,
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