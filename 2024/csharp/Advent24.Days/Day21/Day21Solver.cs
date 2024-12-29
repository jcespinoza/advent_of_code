using AdventOfCode.Commons;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Advent24.Days
{
    public class Day21Solver : Solver<string[], long>
    {
        public Day21Solver() : base(2024, 21) { }

        public override string[] ParseInput(IEnumerable<string> input)
            => input.ToArray();

        public static char[][] NumPadMap = [
            ['1', '2', '3'],
            ['4', '5', '6'],
            ['7', '8', '9'],
            ['\0', '0', 'A'],
            ];

        public static char[][] DPadMap = [
            ['\0','^','A'],
            ['<','v','>'],
        ];

        public static Dictionary<char, (int,int)> NumButtons = new()
        {
            { '7', (0, 0) },
            { '4', (1, 0) },
            { '1', (2, 0) },
            { '8', (0, 1) },
            { '5', (1, 1) },
            { '2', (2, 1) },
            { '9', (0, 2) },
            { '6', (1, 2) },
            { '3', (2, 2) },
            { '0', (3, 1) },
            { 'A', (3, 2) },
        };

        public static Dictionary<(int, int), char> NumButtonsReverse = NumButtons.ToDictionary(x => x.Value, x => x.Key);

        public static Dictionary<char, (int,int)> DButtons = new()
        {
            { '^', (0, 1) },
            { 'A', (0, 2) },
            { '<', (1, 0) },
            { 'v', (1, 1) },
            { '>', (1, 2) },
        };

        public static Dictionary<(int, int), char> DButtonsReverse = DButtons.ToDictionary(x => x.Value, x => x.Key);

        public override long PartOne(string[] passcodes)
        {
            long totalComplexity = 0;
            Dictionary < ((int, int), (int, int)), List < List<char> >> allNumPaths = FindAllPaths(NumButtons, NumPadMap);
            Dictionary<((int, int), (int, int)), List<List<char>>> allDirPaths = FindAllPaths(DButtons, DPadMap);

            foreach (var code in passcodes)
            {
                var complexity = FindComplexity(code, DButtons, allDirPaths, NumButtons, allNumPaths);
                totalComplexity += complexity;
            }

            return totalComplexity;
        }

        public static long FindComplexity(
            string code,
            Dictionary<char, (int, int)> dirButtons, Dictionary<((int, int), (int, int)), List<List<char>>> dirPaths,
            Dictionary<char, (int, int)> numButtons, Dictionary<((int, int), (int, int)), List<List<char>>> numPaths
            )
        {
            List<string> sequences = FindSequenceForRobots(code, 3, dirButtons, dirPaths, numButtons, numPaths);

            var numericPart = int.Parse(code[..^1]); // strip the last character

            int shortestLength = sequences.Min(s => s.Length);
            var complexity = shortestLength * numericPart;;
            return complexity;
        }

        public static List<string> FindSequenceForRobots(string code,
            int robotCount,
            Dictionary<char, (int, int)> dirButtons, Dictionary<((int, int), (int, int)), List<List<char>>> dirPaths,
            Dictionary<char, (int, int)> numButtons, Dictionary<((int, int), (int, int)), List<List<char>>> numPaths
        )
        {
            List<string> sequences = FindKeyPadSequences(code, numButtons, numPaths);
            List<string> nextLevel = [..sequences];
            for (int round = 0; round < robotCount-1; round++)
            {
                List<string> possibleNext = [];
                foreach (var sequence in nextLevel)
                {
                    possibleNext = [.. possibleNext, ..FindKeyPadSequences(sequence, dirButtons, dirPaths)];
                }
                int minLentgh = possibleNext.Min(s => s.Length);
                nextLevel = possibleNext.Where(s => s.Length == minLentgh).ToList();
            }
            return nextLevel;
        }

        private static List<string> FindKeyPadSequences(string code, Dictionary<char,(int,int)> buttons, Dictionary<((int, int), (int, int)), List<List<char>>> keyPaths)
        {
            var pairs = Enumerable.Zip(['A', .. code], code.ToList());
            var options = pairs.Select(p =>
            {
                (char sChar, char eChar) = p;
                var start = buttons[sChar];
                var end = buttons[eChar];
                return keyPaths[(start, end)]
                    .Select(p => string.Join(string.Empty, p));
            }).ToList();

            var product = options.CartesianProduct()
                .Select(p => string.Join(string.Empty, p))
                .ToList();
            
            return product;
        }

        public static Dictionary<((int, int), (int, int)), List<List<char>>> FindAllPaths(Dictionary<char, (int,int)> buttons, char[][] buttonsMap)
        {
            Dictionary<((int, int), (int, int)), List<List<char>>> sequences = [];
            foreach (var start in buttons.Values)
            {
                foreach (var end in buttons.Values)
                {
                    if (start == end)
                    {
                        sequences[(start, end)] = [['A']];
                    }
                    List<List<char>> sequence = FindPossiblePaths(start, end, buttonsMap);
                    sequences[(start, end)] = sequence;
                }
            }

            return sequences;
        }

        public static List<List<char>> FindPossiblePaths((int, int) start, (int, int) end, char[][] keypadMap)
        {
            if (start == end) return [['A']];

            List<List<char>> possibilities = [];
            Queue<((int,int), List<char>)> queue = [];
            queue.Enqueue((start, []));
            int optimal = int.MaxValue;
            while (queue.Count > 0)
            {
                ((int cRow, int cCol), List<char> path) = queue.Dequeue();
                foreach((int nRow,int nCol, char nMove) in new (int, int, char)[] { 
                    (cRow - 1, cCol, '^'), 
                    (cRow + 1, cCol, 'v'),
                    (cRow, cCol - 1, '<'), 
                    (cRow, cCol + 1, '>') }
                )
                {
                    if (nRow < 0 || nRow >= keypadMap.Length || nCol < 0 || nCol >= keypadMap[0].Length)
                    {
                        continue;
                    }

                    if (keypadMap[nRow][nCol] == '\0')
                    {
                        continue;
                    }

                    if ((nRow,nCol) == end)
                    {
                        if (optimal < (path.Count + 1)) return possibilities;
                        
                        optimal = path.Count + 1;
                        possibilities.Add([..path, nMove, 'A']);
                        
                    }
                    else
                    {
                        List<char> nextPath = [.. path, nMove];
                        queue.Enqueue((((int, int))(nRow, nCol), nextPath));
                    }
                }
            }

            return possibilities;
        }

        public override long PartTwo(string[] passcodes)
        {
            throw new NotImplementedException();
        }

        public static List<char> FindNumPadPressesFromButton(char v1, char v2)
        {
            throw new NotImplementedException();
        }
    }
}