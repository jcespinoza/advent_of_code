using AdventOfCode.Commons;
using FluentAssertions.Equivalency.Tracing;

namespace Advent24.Days
{
    public class Day10Solver : Solver<int[][], long>
    {
        public Day10Solver() : base(2024, 10) { }

        public override int[][] ParseInput(IEnumerable<string> input)
            => input.Select(
                l => l.Select(c => c == '.' ? -1 : c - '0')
                      .ToArray()
            ).ToArray();


        public override long PartOne(int[][] topoMap)
        {
            List<(int, int)> trailheads = GetTrailHeads(topoMap);

            long totalScore = 0;
            foreach (var head in trailheads)
            {
                (int row, int col) = head;
                int score = GetHikingScore(topoMap, row, col);
                totalScore += score;
            }

            return totalScore;
        }

        private static List<(int, int)> GetTrailHeads(int[][] topoMap)
        {
            List<(int, int)> heads = [];
            for (int row = 0; row < topoMap.Length; row++)
            {
                for (int col = 0; col < topoMap[row].Length; col++)
                {
                    if(topoMap[row][col] == 0)
                    {
                        heads.Add((row, col));
                    }
                }
            }

            return heads;
        }

        private static int GetHikingScore(int[][] topoMap, int startRow, int startCol)
        {
            HashSet<(int, int)> visited = [];
            Queue<(int, int)> toVisit = new();
            toVisit.Enqueue((startRow, startCol));
            HashSet<(int, int)> uniqueTops = [];

            while(toVisit.Count > 0)
            {
                (int row, int col) = toVisit.Dequeue();
                visited.Add((row, col));
                if (topoMap[row][col] == 9)
                {
                    uniqueTops.Add((row, col));
                    continue;
                }

                ProcessAdjacentCell(topoMap, visited, toVisit, row, col, Direction.North);
                ProcessAdjacentCell(topoMap, visited, toVisit, row, col, Direction.East);
                ProcessAdjacentCell(topoMap, visited, toVisit, row, col, Direction.South);
                ProcessAdjacentCell(topoMap, visited, toVisit, row, col, Direction.West);
            }

            return uniqueTops.Count;
        }

        private static void ProcessAdjacentCell(int[][] topoMap, HashSet<(int, int)> visited, Queue<(int, int)> toVisit, int row, int col, Direction direction)
        {
            Result<(int, int), string> targetCell = GridWalker<int>.Move(topoMap, direction, row, col);
            if (targetCell.IsFailure) return;

            (int nRow, int nCol) = targetCell.Value;

            bool alreadyVisited = visited.Contains(targetCell.Value);
            bool isGradualAscension = topoMap[nRow][nCol] - topoMap[row][col] == 1;
            if (!alreadyVisited && isGradualAscension)
            {
                toVisit.Enqueue(targetCell.Value);
            }
        }

        public override long PartTwo(int[][] input)
        {
            throw new NotImplementedException();
        }
    }
}