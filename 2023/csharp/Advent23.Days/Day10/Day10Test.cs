using Advent23.Days.Day10;
using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day10Test : TestEngine<Day10Solver, PipeMap, long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 6_923;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day10Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    ".....",
                    ".S-7.",
                    ".|.|.",
                    ".L-J.",
                    ".....",
                ],
                Result = 4,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "....",
                        ".S7.",
                        ".LJ.",
                        "....",
                    ],
                    Result = 2,
                },
                new()
                {
                    RawInput = [
                        ".....",
                        ".S-7.",
                        ".L-J.",
                        ".....",
                    ],
                    Result = 3,
                },
                new()
                {
                    RawInput = [
                        "..F7.",
                        ".FJ|.",
                        "SJ.L7",
                        "|F--J",
                        "LJ...",
                    ],
                    Result = 8,
                },
            ],
            Solution = EXPECTED_SOLUTION_PART_1,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = false,
            Example =new()
            {
                RawInput = [
                    "FF7FSF7F7F7F7F7F---7",
                    "L|LJ||||||||||||F--J",
                    "FL-7LJLJ||||||LJL-77",
                    "F--JF--7||LJLJ7F7FJ-",
                    "L---JF-JLJ.||-FJLJJ7",
                    "|F|F-JF---7F7-L7L|7|",
                    "|FFJF7L7F-JF7|JL---7",
                    "7-L-JL7||F7|L7F-7F7|",
                    "L.L7LFJ|||||FJL7||LJ",
                    "L7JLJL-JLJLJL--JLJ.L",
                ],
                Result = 10,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "...........",
                        ".S-------7.",
                        ".|F-----7|.",
                        ".||.....||.",
                        ".||.....||.",
                        ".|L-7.F-J|.",
                        ".|..|.|..|.",
                        ".L--J.L--J.",
                        "...........",
                    ],
                    Result = 4,
                },
                new()
                {
                    RawInput = [
                        "..........",
                        ".S------7.",
                        ".|F----7|.",
                        ".||....||.",
                        ".||....||.",
                        ".|L-7F-J|.",
                        ".|..||..|.",
                        ".L--JL--J.",
                        "..........",
                    ],
                    Result = 4,
                },
                new()
                {
                    RawInput = [
                        ".F----7F7F7F7F-7....",
                        ".|F--7||||||||FJ....",
                        ".||.FJ||||||||L7....",
                        "FJL7L7LJLJ||LJ.L-7..",
                        "L--J.L7...LJS7F-7L7.",
                        "....F-J..F7FJ|L7L7L7",
                        "....L7.F7||L7|.L7L7|",
                        ".....|FJLJ|FJ|F7|.LJ",
                        "....FJL-7.||.||||...",
                        "....L---J.LJ.LJLJ...",
                    ],
                    Result = 8,
                },
            ],
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}