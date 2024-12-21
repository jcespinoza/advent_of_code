using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day16Solver : Solver<char[][], long>
    {
        public Day16Solver() : base(2024, 16) { }

        public override char[][] ParseInput(IEnumerable<string> input)
            => input.Select(l => l.ToCharArray()).ToArray();

        public override long PartOne(char[][] map)
        {
            (int, int) startLocation = FindInMap(map, 'S');
            (int, int) goalLocation = FindInMap(map, 'E');

            PathResult path = ComputeLowestScorePath(map, startLocation, goalLocation);

            long lowestScode = path.LowestScore;

            return lowestScode;
        }

        private static (int, int) FindInMap(char[][] map, char targetChar)
        {
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    if(map[row][col] == targetChar)
                    {
                        return (row, col);
                    }
                }
            }
            return (-1, -1);
        }

        private static PathResult ComputeLowestScorePath(char[][] map, (int,int) startLocation, (int,int) goalLocation)
        {
            PriorityQueue<(int cost, int row, int col, int rOffset, int cOffset), int> pQueue = new();
            HashSet<(int row, int col, int rOffset, int cOffset)> visited = [];

            int lowestScore = -1;
            (int sRow, int sCol) = startLocation;
            pQueue.Enqueue((0, sRow, sCol, 0, 1), 0);
            visited.Add((sRow, sCol, 0, 1));
            while (pQueue.Count > 0)
            {
                (int cCost, int cRow, int cCol, int crOffset, int ccOffset) = pQueue.Dequeue();
                visited.Add((cRow, cCol, crOffset, ccOffset));

                if((cRow, cCol) == goalLocation)
                {
                    lowestScore = cCost;
                    break;
                }


                foreach ((int nCost, int nRow, int nCol, int nrOffset, int ncOffset) in new(int, int, int, int, int)[]{
                    (cCost + 1, cRow + crOffset, cCol + ccOffset, crOffset, ccOffset),
                    (cCost + 1000, cRow, cCol, ccOffset, -crOffset),
                    (cCost + 1000, cRow, cCol, -ccOffset, crOffset),
                })
                {
                    char nValue = map[nRow][nCol];
                    if (nValue == '#') continue;

                    if (visited.Contains((nRow, nCol, nrOffset, ncOffset))) continue;

                    pQueue.Enqueue((nCost, nRow, nCol, nrOffset, ncOffset), nCost);
                }
            }

            return new PathResult { LowestScore = lowestScore };
        }

        public override long PartTwo(char[][] map)
        {
            throw new NotImplementedException();
        }
    }
}