﻿using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day04Test : TestEngine<Day04Solver, object[], long>
    {
        private const int EXPECTED_SOLUTION_PART_1 = 123;
        private const int EXPECTED_SOLUTION_PART_2 = 123;

        public Day04Test()
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