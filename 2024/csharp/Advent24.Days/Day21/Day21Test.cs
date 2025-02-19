﻿using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day21Test : TestEngine<Day21Solver, string[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 202274;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day21Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "029A",
                    "980A",
                    "179A",
                    "456A",
                    "379A",
                ],
                Result = 126384,
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