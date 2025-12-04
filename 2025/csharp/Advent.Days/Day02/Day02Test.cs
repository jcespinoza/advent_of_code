using AdventOfCode.Commons;
using dotenv.net;

namespace Advent.Days
{
    public class Day02Test : TestEngine<Day02Solver, Range[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 31000881061;
        private const long EXPECTED_SOLUTION_PART_2 = 46769308485;

        public Day02Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = ["11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124"
                ],
                Result = 1227775554,
            },
            Examples = [
                new () {
                    RawInput = ["11-22"],
                    Result = 33
                },
                new () {
                    RawInput = ["95-115"],
                    Result = 99
                },
                new () {
                    RawInput = ["998-1012"],
                    Result = 1010
                },
                new () {
                    RawInput = ["1188511880-1188511890"],
                    Result = 1188511885
                },
                new () {
                    RawInput = ["222220-222224"],
                    Result = 222222
                },
                new () {
                    RawInput = ["1698522-1698528"],
                    Result = 0
                },
                new () {
                    RawInput = ["446443-446449"],
                    Result = 446446
                },
                new () {
                    RawInput = ["38593856-38593862"],
                    Result = 38593859
                },
                new () {
                    RawInput = ["565653-565659"],
                    Result = 0
                },
                new () {
                    RawInput = ["824824821-824824827"],
                    Result = 0
                },
                new () {
                    RawInput = ["2121212118-2121212124"],
                    Result = 0
                }
            ],
            Solution = EXPECTED_SOLUTION_PART_1,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = ["11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124"
                ],
                Result = 4174379265,
            },
            Examples = [
                new () {
                    RawInput = ["11-22"],
                    Result = 33
                },
                new () {
                    RawInput = ["95-115"],
                    Result = 210
                },
                new () {
                    RawInput = ["998-1012"],
                    Result = 2009
                },
                new () {
                    RawInput = ["1188511880-1188511890"],
                    Result = 1188511885
                },
                new () {
                    RawInput = ["222220-222224"],
                    Result = 222222
                },
                new () {
                    RawInput = ["1698522-1698528"],
                    Result = 0
                },
                new () {
                    RawInput = ["446443-446449"],
                    Result = 446446
                },
                new () {
                    RawInput = ["38593856-38593862"],
                    Result = 38593859
                },
                new () {
                    RawInput = ["565653-565659"],
                    Result = 565656
                },
                new () {
                    RawInput = ["824824821-824824827"],
                    Result = 824824824
                },
                new () {
                    RawInput = ["2121212118-2121212124"],
                    Result = 2121212121
                }
            ],
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}