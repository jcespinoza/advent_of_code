﻿using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day18Test : TestEngine<Day18Solver, object[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 123;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day18Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = true,
            Example = new()
            {
                RawInput = [
                ],
                Result = 8,
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