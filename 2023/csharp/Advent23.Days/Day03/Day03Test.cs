using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day03Test : TestEngine<Day03Solver, string[], long>
    {
        private const int EXPECTED_SOLUTION_PART_1 = 527446;
        private const int EXPECTED_SOLUTION_PART_2 = 73201705;

        public Day03Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                Input = [
                    "467..114..",
                    "...*......",
                    "..35..633.",
                    "......#...",
                    "617*......",
                    ".....+.58.",
                    "..592.....",
                    "......755.",
                    "...$.*....",
                    ".664.598..",
                ],
                Result = 4361,
            },
            Examples = [
                new()
                {
                    Input = [
                        "..........",
                        ".......500",
                        "...*..*...",
                    ],
                    Result = 500,
                }, 
                new()
                {
                    Input = [
                        ".....*....",
                        ".....*.123",
                        "...*.*....",
                    ],
                    Result = 0,
                },  
                new()
                {
                    Input = [
                        ".....*.....%",
                        ".....*.124.%",
                        "...*.*.....%",
                    ],
                    Result = 0,
                },
                new()
                {
                    Input = [
                        ".....*^^^^^%",
                        ".....*.....%",
                        ".....*.125.%",
                        "...*.*.....%",
                        "...*.*^^^^^%",
                    ],
                    Result = 0,
                },
                new()
                {
                    Input = [
                        ".....^^^^^%",
                        "..........%",
                        "......126.%",
                        "...*......%",
                        "...*.^^^^^%",
                    ],
                    Result = 0,
                }, 
                new()
                {
                    Input = [
                        "..........",
                        "500.......",
                        "...*......",
                    ],
                    Result = 500,
                },      
                new()
                {
                    Input = [
                        ".....*....",
                        "*.....761.",
                        "@.........",
                    ],
                    Result = 761,
                },
                new()
                {
                    Input = [
                        "!.....%...",
                        "*.467.&...",
                        "@.....#...",
                    ],
                    Result = 0,
                },      
                new()
                {
                    Input = [
                        "*467..114..",
                        "..........",
                    ],
                    Result = 467,
                },
                new()
                {
                    Input = [
                        "467..114..",
                        "...*......",
                    ],
                    Result = 467,
                },
                new()
                {
                    Input = [
                        "467..114..",
                        "$.........",
                    ],
                    Result = 467,
                },
                new()
                {
                    Input = [
                        "467..114*.",
                    ],
                    Result = 114,
                },
                new()
                {
                    Input = [
                        "...*.....",
                        "892..114.",
                    ],
                    Result = 892,
                },
                new()
                {
                    Input = [
                        ".^..",
                        "2...",
                        "....",
                        ".*..",
                    ],
                    Result = 2,
                },
                new()
                {
                    Input = [
                        ".^..",
                        ".3..",
                        "....",
                        ".*..",
                    ],
                    Result = 3,
                },
                new()
                {
                    Input = [
                        "...%",
                        ".4..",
                        "....",
                        ".*..",
                    ],
                    Result = 0,
                },
                new()
                {
                    Input = [
                        "...%",
                        "..5.",
                        "....",
                        ".*..",
                    ],
                    Result = 5,
                },
            ],
            Solution = EXPECTED_SOLUTION_PART_1,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                Input = [
                    "467..114..",
                    "...*......",
                    "..35..633.",
                    "......#...",
                    "617*......",
                    ".....+.58.",
                    "..592.....",
                    "......755.",
                    "...$.*....",
                    ".664.598..",
                ],
                Result = 467835,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}