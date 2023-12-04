using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day02Test : TestEngine<Day02Solver, string[], long>
    {
        private const int EXPECTED_SOLUTION_PART_2 = 123;
        private const int EXPECTED_SOLUTION_PART_1 = 123;

        public Day02Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,

            Example = new()
            {
                Input = [
                    "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
                    "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
                    "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
                    "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
                    "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
                ],
                Result = 8,
            },
            Solution = 123,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = true,

            Example = new()
            {
                Input = [
                   
                ],
                Result = 0,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}