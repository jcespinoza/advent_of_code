using AdventOfCode.Commons;
using System.Net;

namespace Advent24.Days
{
    public class Day25Solver : Solver<List<Schematic>, long>
    {
        public Day25Solver() : base(2024, 25) { }

        public override List<Schematic> ParseInput(IEnumerable<string> input)
        {
            List<Schematic> schematics = [];
            List<string> currentLines = [];
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if(currentLines.Count > 0)
                    {
                        schematics.Add(Schematic.Parse(currentLines));
                        currentLines.Clear();
                    }
                    continue;
                }
                currentLines.Add(line);
            }
            if (currentLines.Count > 0)
            {
                schematics.Add(Schematic.Parse(currentLines));
            }
            return schematics;
        }


        public override long PartOne(List<Schematic> schematics)
        {
            var locks = schematics
                .Where(s => s.Type == SchematicType.Lock)
                .Select(s => s.Pinout)
                .Select(p => (p[0], p[1], p[2], p[3], p[4]))
                .ToList();
            var keys = schematics
                .Where(s => s.Type == SchematicType.Key)
                .Select(s => s.Pinout)
                .Select(p => (p[0], p[1], p[2], p[3], p[4]))
                .ToList();

            long matches = 0;
            foreach (var keyPinout in keys)
            {
                foreach (var sLock in locks)
                {
                    bool match = Schematic.AreCompatible(keyPinout, sLock);
                    if (match)
                    {
                        matches++;
                    }
                }
            }

            return matches;
        }

        public override long PartTwo(List<Schematic> input)
        {
            throw new NotImplementedException();
        }
    }
}