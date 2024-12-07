using AdventOfCode.Commons;
using System.Net;
using Xunit;

namespace Advent24.Days
{
    public class Day05Solver : Solver<PrintingConfig, long>
    {
        public Day05Solver() : base(2024, 05) { }

        public override PrintingConfig ParseInput(IEnumerable<string> input)
        {
            int index = 0;
            var lineArray = input.ToArray();
            var currentLine = string.Empty;
            List<OrderingRule> rules = [];
            do
            {
                currentLine = lineArray[index];

                var parts = currentLine.Split('|');

                if(parts.Length != 2)
                {
                    break;
                }

                var pageX = int.Parse(parts[0]);
                var pageY = int.Parse(parts[1]);

                rules.Add(new OrderingRule(pageX, pageY));

                index++;
            } while (!string.IsNullOrWhiteSpace(currentLine));

            // A blank line was found. Skip to the next line
            index++;

            List<int[]> updates = [];
            while(index < lineArray.Length)
            {
                currentLine = lineArray[index];

                Assert.False(string.IsNullOrWhiteSpace(currentLine));

                var pages = currentLine.Split(',').ToList();
                updates.Add(pages.Select(int.Parse).ToArray());

                index++;
            }

            return new PrintingConfig { Rules = rules, Updates = updates };
        }


        public override long PartOne(PrintingConfig input)
        {
            List<int[]> correctUpdates = PrintingOrderChecker
                                        .FindCorrectUpdates(input.Updates, input.Rules)
                                        .ToList();

            List<int> middlePages = correctUpdates.Select(u => u[u.Length / 2]).ToList();

            long sumOfPageNumber = middlePages.Sum();

            return sumOfPageNumber;
        }

        public override long PartTwo(PrintingConfig input)
        {
            throw new NotImplementedException();
        }
    }
}