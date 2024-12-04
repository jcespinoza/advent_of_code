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

        public override long PartTwo(Report[] reports)
        {
            List<Report> safeReports = [];
            List<Report> unsafeReports = [];
            foreach (var item in reports)
            {
                if(item.IsSafe())
                {
                    safeReports.Add(item);
                }
                else
                {
                    unsafeReports.Add(item);
                }
            }

            List<Report> problemDampened = CheckWithProblemDampening(unsafeReports).ToList();

            return safeReports.Count + problemDampened.Count;
        }

        private static IEnumerable<Report> CheckWithProblemDampening(List<Report> unsafeReports)
        {
            foreach (var item in unsafeReports)
            {
                // try removing levels and check again
                for (int index = 0; index < item.Levels.Length; index++)
                {
                    var newLevels = item.Levels.ToList();
                    newLevels.RemoveAt(index);
                    var newReport = new Report { Levels = [.. newLevels] };
                    if (newReport.IsSafe())
                    {
                        yield return newReport;
                        break; // make sure we stop after first success
                    }
                }
            }
        }
    }
}