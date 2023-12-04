﻿using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days.Day01
{
    public class Day01Test : TestEngine<Day01Solver, string[], long>
    {
        private const int EXPECTED_SOLUTION_PART_2 = 54_431;

        public Day01Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,

            Example = new()
            {
                Input = [
                    "1abc2",
                    "pqr3stu8vwx",
                    "a1b2c3d4e5f",
                    "treb7uchet",
                ],
                Result = 142,
            },
            Solution = 55_477,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = false,

            Example = new()
            {
                Input = [
                    "two1nine",
                    "eightwothree",
                    "abcone2threexyz",
                    "xtwone3four",
                    "4nineeightseven2",
                    "zoneight234",
                    "7pqrstsixteen",
                ],
                Result = 281,
            },
            Examples = [
                new()
                {
                    Input = ["bpk1prxsj"],
                    Result = 151,
                },
                new()
                {
                    Input = ["2s"],
                    Result = 282,
                },
            ],
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}