using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day15Solver : SteppedSolver<Warehouse, Warehouse, long, long>
    {
        public Day15Solver() : base(2024, 15) { }

        public override Warehouse ParseInputOne(IEnumerable<string> input)
        {
            var lines = input.ToList();
            var map = lines.TakeWhile(x => x.StartsWith('#')).Select(x => x.ToCharArray()).ToArray();
            List<Direction> moves = lines.SkipWhile(x => x.StartsWith('#'))
                .Skip(1)
                .TakeWhile(x => !string.IsNullOrWhiteSpace(x))
                .SelectMany(x => x.ToCharArray())
                .Select(x =>
                {
                    if(x == '^') return Direction.North;
                    if (x == 'v') return Direction.South;
                    if (x == '>') return Direction.East;
                    if (x == '<') return Direction.West;

                    throw new InvalidOperationException();
                })
                .ToList();
            return new() { Map = map, Moves = moves };
        }

        public override Warehouse ParseInputTwo(IEnumerable<string> input)
        {
            var lines = input.ToList();
            var map = lines
                .TakeWhile(x => x.StartsWith('#'))
                .Select(
                    str => str.Replace("#", "##")
                    .Replace(".", "..")
                    .Replace("@", "@.")
                    .Replace("O", "[]")
                )
                .Select(x => x.ToCharArray())
                .ToArray();
            List<Direction> moves = lines
                .SkipWhile(x => x.StartsWith('#'))
                .Skip(1)
                .TakeWhile(x => !string.IsNullOrWhiteSpace(x))
                .SelectMany(x => x.ToCharArray())
                .Select(x =>
                {
                    if (x == '^') return Direction.North;
                    if (x == 'v') return Direction.South;
                    if (x == '>') return Direction.East;
                    if (x == '<') return Direction.West;

                    throw new InvalidOperationException();
                })
                .ToList();
            return new() { Map = map, Moves = moves };
        }

        public override long PartOne(Warehouse warehouse)
        {
            (int,int) robotLocation = FindRobotInMap(warehouse.Map);
            foreach (var move in warehouse.Moves)
            {
                robotLocation = MoveRobot(warehouse.Map, robotLocation, move);
            }
            HashSet<(int, int)> boxes = FindBoxes(warehouse.Map, 'O');

            long sumOfGPS = 0;
            foreach (var box in boxes)
            {
                (int distanceFromTop, int distanceFromLeft) = box;
                int localSum = 100 * distanceFromTop + distanceFromLeft;
                sumOfGPS += localSum;
            }

            return sumOfGPS;
        }

        private static (int, int) FindRobotInMap(char[][] map)
        {
            // The robot is denoted by the '@' character
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    char cellValue = map[row][col];
                    if (cellValue == '@')
                    {
                        return (row, col);
                    }
                }
            }
            return (-1, -1);
        }

        public static (int,int) MoveRobot(char[][] map, (int,int) robotLocation, Direction direction)
        {
            (int rbRow, int rbCol) = robotLocation;
            List<(int, int)> pushTargets = [];
            (int rowOffset, int colOffset) = direction switch
            {
                Direction.North => (-1, 0),
                Direction.South => (1, 0),
                Direction.East => (0, 1),
                Direction.West => (0, -1),
                _ => (0, 0),
            };
            (int cRow, int cCol) = (rbRow, rbCol);
            bool canPush = true;
            while (true)
            {
                cRow+= rowOffset;
                cCol += colOffset;
                char cellValue = map[cRow][cCol];
                if (cellValue == '#')
                {
                    canPush = false;
                    break;
                }
                if (cellValue == 'O')
                {
                    pushTargets.Add((cRow, cCol));
                }
                if(cellValue == '.')
                {
                    break;
                }
            }
            if (!canPush) return robotLocation;

            map[rbRow][rbCol] = '.';
            map[rbRow + rowOffset][rbCol + colOffset] = '@';

            foreach (var target in pushTargets)
            {
                (int tRow, int tCol) = (target.Item1 + rowOffset, target.Item2 + colOffset);
                map[tRow][tCol] = 'O';
            }            

            return (rbRow + rowOffset, rbCol + colOffset);
        }

        private static HashSet<(int, int)> FindBoxes(char[][] map, char indicator)
        {
            HashSet<(int, int)> boxes = [];
            //A box is every cell that has an 'O' in it
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    if(map[row][col] == indicator)
                        boxes.Add((row, col));
                }
            }
            return boxes;
        }

        public override long PartTwo(Warehouse warehouse)
        {
            (int, int) robotLocation = FindRobotInMap(warehouse.Map);

            foreach (var move in warehouse.Moves)
            {
                robotLocation = MoveWideRobot(warehouse.Map, robotLocation, move);
            }
            HashSet<(int, int)> boxes = FindBoxes(warehouse.Map, '[');

            long sumOfGPS = 0;
            foreach (var box in boxes)
            {
                (int distanceFromTop, int distanceFromLeft) = box;
                int localSum = 100 * distanceFromTop + distanceFromLeft;
                sumOfGPS += localSum;
            }

            return sumOfGPS;
        }

        private static (int, int) MoveWideRobot(char[][] map, (int, int) robotLocation, Direction direction)
        {
            (int rbRow, int rbCol) = robotLocation;
            List<(int, int)> pushTargets = [];
            (int rowOffset, int colOffset) = direction switch
            {
                Direction.North => (-1, 0),
                Direction.South => (1, 0),
                Direction.East => (0, 1),
                Direction.West => (0, -1),
                _ => (0, 0),
            };
            pushTargets.Add((rbRow, rbCol));
            bool canPush = true;
            for (int index = 0; index < pushTargets.Count; index++)
            {
                (int cRow, int cCol) = pushTargets[index];
                int nRow = cRow + rowOffset;
                int nCol = cCol + colOffset;

                if (pushTargets.Contains((nRow, nCol)))
                {
                    continue;
                }

                char cellValue = map[nRow][nCol];
                if (cellValue == '#')
                {
                    canPush = false;
                    break;
                }
                if (cellValue == '[')
                {
                    pushTargets.Add((nRow, nCol));
                    pushTargets.Add((nRow, nCol + 1));
                }
                if (cellValue == ']')
                {
                    pushTargets.Add((nRow, nCol));
                    pushTargets.Add((nRow, nCol - 1));
                }
            }
            if (!canPush) return robotLocation;

            char[][] mapDup = DuplicateMap(map);

            map[rbRow][rbCol] = '.';
            map[rbRow + rowOffset][rbCol + colOffset] = '@';

            for (int index = 1; index < pushTargets.Count; index++)
            {
                (int bRow, int bCol) = pushTargets[index];
                map[bRow][bCol] = '.';
            }

            for (int index = 1; index < pushTargets.Count; index++)
            {
                (int bRow, int bCol) = pushTargets[index];
                map[bRow + rowOffset][bCol + colOffset] = mapDup[bRow][bCol];
            }

            return (rbRow + rowOffset, rbCol + colOffset);
        }

        private static char[][] DuplicateMap(char[][] map)
        {
            char[][] newMap = new char[map.Length][];
            for (int i = 0; i < map.Length; i++)
            {
                newMap[i] = new char[map[i].Length];
                for (int j = 0; j < map[i].Length; j++)
                {
                    newMap[i][j] = map[i][j];
                }
            }
            return newMap;
        }
    }
}