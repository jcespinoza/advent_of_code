using AdventOfCode.Commons;

namespace Advent23.Days
{
    public class Day01Solver : Solver<string[], long>
    {
        public Day01Solver() : base(2023, 01) { }

        public override string[] ParseInput(IEnumerable<string> input)
            => input.ToArray();


        public override long PartOne(string[] input)
        {
            var lineNumbers = input.Select(x => {
                var firstDigit = x.FirstOrDefault(c => char.IsDigit(c));
                var lastDigit = x.LastOrDefault(c => char.IsDigit(c));
                return (firstDigit - 48)*10 + (lastDigit - 48);
            }).ToArray();
            var numbers = lineNumbers.Sum();
            return numbers;
        }

        public override long PartTwo(string[] input)
        {
            throw new NotImplementedException();
        }
    }
}
