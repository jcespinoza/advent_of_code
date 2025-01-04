using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day23Solver : Solver<(string,string)[], long>
    {
        public Day23Solver() : base(2024, 23) { }

        public override (string,string)[] ParseInput(IEnumerable<string> input)
            => input
            .Select(l => l.Split('-'))
            .Select(l => (l[0], l[1]))
            .ToArray();

        public override long PartOne((string,string)[] networkMap)
        {
            HashSet<(string,string,string)> groups = [];
            Dictionary<string, List<string>> connections = [];
            foreach (var (a, b) in networkMap)
            {
                if (connections.TryGetValue(a, out List<string>? groupA))
                {
                    groupA.Add(b);
                }
                else
                {
                    connections.Add(a, [b]);
                }

                if (connections.TryGetValue(b, out List<string>? groupB))
                {
                    groupB.Add(a);
                }
                else
                {
                    connections.Add(b, [a]);
                }
            }

            foreach (var rootA in connections.Keys)
            {
                foreach (var rootB in connections[rootA])
                {
                    foreach (var rootC in connections[rootB])
                    {
                        if(rootA != rootC && connections[rootC].Contains(rootA))
                        {
                            var newGroup = new List<string> { rootA, rootB, rootC };
                            newGroup.Sort();
                            groups.Add((newGroup[0], newGroup[1], newGroup[2]));
                        }
                    }
                }
            }
            var groupsWithT = groups
                .Where(g => g.Item1.StartsWith("t") || g.Item2.StartsWith("t") || g.Item3.StartsWith("t"))
                .ToList();

            return groupsWithT.Count();
        }

        public override long PartTwo((string,string)[] networkMap)
        {
            throw new NotImplementedException();
        }
    }
}