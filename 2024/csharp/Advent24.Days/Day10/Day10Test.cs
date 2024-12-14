using AdventOfCode.Commons;
using dotenv.net;

namespace Advent24.Days
{
    public class Day10Test : TestEngine<Day10Solver, int[][], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 517;
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
                        "89010123",
                        "78121874",
                        "87430965",
                        "96549874",
                        "45678903",
                        "32019012",
                        "01329801",
                        "10456732",
                    ],
                Result = 36,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "0123",
                        "1234",
                        "8765",
                        "9876",
                    ],
                    Result = 1,
                },
                new()
                {
                    RawInput = [
                        "...0...",
                        "...1...",
                        "...2...",
                        "6543456",
                        "7.....7",
                        "8.....8",
                        "9.....9",
                    ],
                    Result = 2,
                },
                new()
                {
                    RawInput = [
                        "..90..9",
                        "...1.98",
                        "...2..7",
                        "6543456",
                        "765.987",
                        "876....",
                        "987....",
                    ],
                    Result = 4,
                },
                new()
                {
                    RawInput = [
                        "10..9..",
                        "2...8..",
                        "3...7..",
                        "4567654",
                        "...8..3",
                        "...9..2",
                        ".....01",
                    ],
                    Result = 3,
                },
            ],
            Solution = EXPECTED_SOLUTION_PART_1,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "89010123",
                    "78121874",
                    "87430965",
                    "96549874",
                    "45678903",
                    "32019012",
                    "01329801",
                    "10456732",
                ],
                Result = 81,
            },

            Examples = [
                new(){
                    RawInput = [
                        ".....0.",
                        "..4321.",
                        "..5..2.",
                        "..6543.",
                        "..7..4.",
                        "..8765.",
                        "..9....",
                    ],
                    Result = 3,
                },
                new(){
                    RawInput = [
                        "..90..9",
                        "...1.98",
                        "...2..7",
                        "6543456",
                        "765.987",
                        "876....",
                        "987....",
                    ],
                    Result = 13,
                },
                new(){
                    RawInput = [
                        "012345",
                        "123456",
                        "234567",
                        "345678",
                        "4.6789",
                        "56789.",
                    ],
                    Result = 227,
                },
            ],
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}