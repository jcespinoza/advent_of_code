using Advent24.Days.Day02;
using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day02Solver : Solver<Report[], long>
    {
        public Day02Solver() : base(2024, 02) { }

        public override Report[] ParseInput(IEnumerable<string> input)
        {
            return input.Select(line => {
                var levels = line.Split(' ')
                                .Select(int.Parse)
                                .ToArray();
                return new Report { Levels = levels };
            }).ToArray();            
        }

        public override long PartOne(Report[] reports)
        {
            var safeReports = reports.Select(r => r.IsSafe());

            return safeReports.Count(safe => safe);
        }

        public override long PartTwo(Report[] input)
        {
            throw new NotImplementedException();
        }
    }
}