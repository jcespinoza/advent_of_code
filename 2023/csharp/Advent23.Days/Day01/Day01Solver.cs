using AdventOfCode.Commons;
using System.Runtime.CompilerServices;

namespace Advent23.Days
{
    public class Day01Solver : Solver<string[], long>
    {
        private readonly List<string> numbersNames =
        [
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        ];

        private readonly List<string> reverseNumbersNames =
        [
            "eno",
            "owt",
            "eerht",
            "ruof",
            "evif",
            "xis",
            "neves",
            "thgie",
            "enin"
        ];

        private readonly char[] numberChars = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];

        public Day01Solver() : base(2023, 01) { }

        public override string[] ParseInput(IEnumerable<string> input)
            => input.ToArray();


        public override long PartOne(string[] input)
        {
            int[] lineNumbers = FindNumbersPart1(input);
            var numbers = lineNumbers.Sum();
            return numbers;
        }

        public override long PartTwo(string[] input)
        {
            int[] lineNumbers = FindNumbersPart2(input);
            var numbers = lineNumbers.Sum();
            return numbers;
        }

        private int[] FindNumbersPart1(string[] input)
        {
            return input.Select(x =>
            {
                var firstDigit = x.FirstOrDefault(c => char.IsDigit(c));
                var lastDigit = x.LastOrDefault(c => char.IsDigit(c));
                return (firstDigit - 48) * 10 + (lastDigit - 48);
            }).ToArray();
        }

        private int[] FindNumbersPart2(string[] input)
        {
            return input.Select(GetNumberForLine).ToArray();
        }

        private int GetNumberForLine(string input)
        {
            int firstDigit = -1;
            int lastDigit = -1;
            if (char.IsDigit(input[0]))
            {
                firstDigit = input[0] - 48;
            }

            string buffer = string.Empty;
            for (int index = 0; index < input.Length; index++)
            {
                if (firstDigit >= 0) break;
                buffer += input[index];

                var foundIndex = numbersNames.FindIndex(n => buffer.Contains(n));
                if (foundIndex >= 0)
                {
                    firstDigit = foundIndex + 1;
                    break;
                }
                if (char.IsDigit(input[index]))
                {
                    firstDigit = input[index] - 48;
                    break;
                }
            }
            
            if (char.IsDigit(input[^1]))
            {
                lastDigit = input[^1] - 48;
            }

            buffer = string.Empty;
            for (int index = input.Length-1; index >= 0; index--)
            {
                if (lastDigit >= 0) break;
                buffer += input[index];

                var foundIndex = reverseNumbersNames.FindIndex(n => buffer.Contains(n));
                if (foundIndex >= 0)
                {
                    lastDigit = foundIndex + 1;
                    break;
                }

                if (char.IsDigit(input[index]))
                {
                    lastDigit = input[index] - 48;
                    break;
                }
            }

            var calibrationValue = firstDigit * 10 + (lastDigit);
            return calibrationValue;
        }
    }
}
