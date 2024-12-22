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
            PriorityQueue<(int cost, int row, int col, Direction dir), int> pQueue = new();
            HashSet<(int row, int col, Direction dir)> visited = [];

            int lowestScore = -1;
            (int sRow, int sCol) = startLocation;
            pQueue.Enqueue((0, sRow, sCol, Direction.East), 0);
            visited.Add((sRow, sCol,Direction.East));
            while (pQueue.Count > 0)
            {
                (int cCost, int cRow, int cCol, Direction cDir) = pQueue.Dequeue();
                visited.Add((cRow, cCol, cDir));

                (int crOffset, int ccOffset) = cDir.ToOffsets();
                if ((cRow, cCol) == goalLocation)
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

                    if (visited.Contains((nRow, nCol, (nrOffset, ncOffset).ToDirection() ))) continue;

                    pQueue.Enqueue((nCost, nRow, nCol, (nrOffset, ncOffset).ToDirection()), nCost);
                }
            }

            return new PathResult { LowestScore = lowestScore };
        }

        public override long PartTwo(char[][] map)
        {
            (int, int) startLocation = FindInMap(map, 'S');
            (int, int) goalLocation = FindInMap(map, 'E');

            PathResult path = ComputeBestSeats(map, startLocation, goalLocation);

            long lowestScode = path.BestSeats;

            return lowestScode;
        }

        private PathResult ComputeBestSeats(char[][] map, (int, int) startLocation, (int, int) goalLocation)
        {
            PriorityQueue<(int cost, int row, int col, Direction dir), int> pQueue = new();
            Dictionary<(int row, int col, Direction dir), int> lowestCost = [];
            Dictionary<(int row, int col, Direction dir), HashSet<(int, int, Direction)>> backtrack = [];
            Queue<(int, int, Direction)> endStates = [];
            int bestCost = int.MaxValue;

            (int sRow, int sCol) = startLocation;
            pQueue.Enqueue((0, sRow, sCol, Direction.East), 0);
            lowestCost.Add((sRow, sCol, Direction.East), 0);
            while (pQueue.Count > 0)
            {
                (int cCost, int cRow, int cCol, Direction cDir) = pQueue.Dequeue();
                bool costExist = lowestCost.TryGetValue((cRow, cCol, cDir), out int visitedCost);
                if (costExist && cCost > visitedCost) continue;

                (int crOffset, int ccOffset) = cDir.ToOffsets();
                if ((cRow, cCol) == goalLocation)
                {
                    if(cCost > bestCost) break;
                    
                    bestCost = cCost;
                    if (!endStates.Contains((cRow, cCol, cDir))){
                        endStates.Enqueue((cRow, cCol, cDir));
                    }
                }

                foreach ((int nCost, int nRow, int nCol, int nrOffset, int ncOffset) in new (int, int, int, int, int)[]{
                    (cCost + 1, cRow + crOffset, cCol + ccOffset, crOffset, ccOffset),
                    (cCost + 1000, cRow, cCol, ccOffset, -crOffset),
                    (cCost + 1000, cRow, cCol, -ccOffset, crOffset),
                })
                {

                    char nValue = map[nRow][nCol];
                    if (nValue == '#') continue;

                    Direction nDir = (nrOffset, ncOffset).ToDirection();

                    bool lowestExists = lowestCost.TryGetValue((nRow, nCol, nDir), out int nVisitedCost);
                    if (lowestExists && nCost > nVisitedCost) continue;
                    if(lowestExists && nCost < nVisitedCost)
                    {
                        backtrack.Add((nRow, nCol, nDir), []);
                        lowestCost[(nRow, nCol, nDir)] = nCost;
                    }
                    if (backtrack.ContainsKey((nRow, nCol, nDir)))
                    {
                        backtrack[(nRow, nCol, nDir)].Add((cRow, cCol, cDir));
                    }
                    else
                    {
                        backtrack.Add((nRow, nCol, nDir), []);
                    }
                    pQueue.Enqueue((nCost, nRow, nCol, nDir), nCost);
                }
            }

            HashSet<(int, int, Direction)> bestSeats = [];

            while (endStates.Count > 0)
            {
                (int, int, Direction) finalConfig = endStates.Dequeue();
                
                // Ignore starting position
                if(finalConfig == (sRow, sCol, Direction.East)) break;
                if(!backtrack.ContainsKey(finalConfig)) continue;
                foreach (var config in backtrack[finalConfig])
                {
                    if(bestSeats.Contains(config)) continue;

                    bestSeats.Add(config);
                    endStates.Enqueue(config);
                }
            }

            int bestSeatCount = bestSeats.Count;

            return new PathResult { BestSeats = bestSeatCount };
        }
    }
}