﻿using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day06Test : TestEngine<Day06Solver, object[], long>
    {
        private const int EXPECTED_SOLUTION_PART_1 = 123;
        private const int EXPECTED_SOLUTION_PART_2 = 123;

        public Day06Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = true,
            Example = new()
            {
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
                Result = 8,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}