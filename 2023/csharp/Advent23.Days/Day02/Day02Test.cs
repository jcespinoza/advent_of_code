using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day02Test : TestEngine<Day02Solver, GameInfo[], long>
    {
        private const int EXPECTED_SOLUTION_PART_1 = 2_285;
        private const int EXPECTED_SOLUTION_PART_2 = 77_021;

        public Day02Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            /*
             * MAX: 12 red cubes, 13 green cubes, and 14 blue cubes
             * 
            */

            Example = new()
            {
                RawInput = [
                    "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
                    "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
                    "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
                    "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
                    "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
                ],
                Result = 8,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
                    ],
                    Input = [
                        new GameInfo
                        {
                            GameID = 1,
                            Sets = [
                                new()
                                {
                                    Blue = 3,
                                    Red = 4,
                                },
                                new()
                                {
                                    Red = 1,
                                    Green = 2,
                                    Blue = 6,
                                },
                                new()
                                {
                                    Green = 2,
                                },

                            ]
                        }
                    ],
                    Result = 1,
                }
            ],
            Solution = EXPECTED_SOLUTION_PART_1,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = false,

            Example = new()
            {
                RawInput = [
                    "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
                    "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
                    "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
                    "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
                    "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
                ],
                Result = 2286,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}