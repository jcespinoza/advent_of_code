using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days.Day01
{
    public class Day01Test : TestEngine<Day01Solver, string[], long>
    {
        public Day01Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,

            Example = new()
            {
                Input = new[] {
                     "1abc2",
                    "pqr3stu8vwx",
                    "a1b2c3d4e5f",
                    "treb7uchet",
                },
                Result = 142,
            },
            Solution = 55_477,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = true,

            Example = new()
            {
                Input = new[] {
                     "4",
                     "1fsd1",
                     "3fdg3gg2"
                },
                Result = 87,
            },
            Solution = 1737,
        };
    }
}