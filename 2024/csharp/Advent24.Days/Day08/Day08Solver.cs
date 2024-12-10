using AdventOfCode.Commons;
using System.Collections.Generic;

namespace Advent24.Days
{
    public class Day08Solver : Solver<char[][], long>
    {
        public Day08Solver() : base(2024, 08) { }

        public override char[][] ParseInput(IEnumerable<string> input)
            => input.Select(l => l.ToCharArray()).ToArray();


        public override long PartOne(char[][] map)
        {
            List<Antenna> antennas = FindAntennas(map);
            Dictionary<char, List<Antenna>> mapOfAntennas = antennas
                                                .GroupBy(a => a.Frequency)
                                                .ToDictionary(a => a.Key, a => a.ToList());
            
            HashSet<(int, int)> allAntinodes = FindAntinodesInMap(map, mapOfAntennas);

            var uniqueLocations = allAntinodes.OrderBy(a => a.Item1).ThenBy(a => a.Item2).Select(a => (a.Item1, a.Item2)).ToHashSet();

            return uniqueLocations.Count;
        }

        private HashSet<(int, int)> FindAntinodesInMap(char[][] map, Dictionary<char, List<Antenna>> mapOfAntennas)
        {
            HashSet<(int, int)> allAntinodes = [];
            foreach (var group in mapOfAntennas.Values)
            {
                for(int indexA = 0; indexA < group.Count; indexA++)
                {
                    for (int indexB = indexA + 1; indexB < group.Count; indexB++)
                    {
                        // Compare one antenna with rest of antennas
                        Antenna antennaA = group[indexA];
                        Antenna antennaB = group[indexB];
                        (int aRow, int aCol) = (antennaA.Row, antennaA.Col);
                        (int bRow, int bCol) = (antennaB.Row, antennaB.Col);

                        // Antinode in away from first antenna
                        (int antiRow1, int antiCol1) = (2 * aRow - bRow, 2 * aCol - bCol);
                        // Antinode in away from second antenna
                        (int antiRow2, int antiCol2) = (2 * bRow - aRow, 2 * bCol - aCol);

                        if (GridWalker<char>.IsPointInGrid(map, antiRow1, antiCol1))
                        {
                            allAntinodes.Add((antiRow1, antiCol1));
                        }

                        if (GridWalker<char>.IsPointInGrid(map, antiRow2, antiCol2))
                        {
                            allAntinodes.Add((antiRow2, antiCol2));
                        }
                    }
                }
            }

            return allAntinodes;
        }

        private HashSet<(int, int)> FindAntinodesInMapAnywhere(char[][] map, Dictionary<char, List<Antenna>> mapOfAntennas)
        {
            HashSet<(int, int)> allAntinodes = [];
            foreach (var group in mapOfAntennas.Values)
            {
                foreach(var antennaA in group)
                {
                    foreach(var antennaB in group)
                    {
                        // Make sure we are not comparing one antenna with itself
                        if (antennaA == antennaB) continue;

                        (int aRow, int aCol) = (antennaA.Row, antennaA.Col);
                        (int bRow, int bCol) = (antennaB.Row, antennaB.Col);

                        (int diffRow, int diffCol) = (bRow - aRow, bCol - aCol);

                        int antiRow = aRow;
                        int antiCol = aCol;
                        while (GridWalker<char>.IsPointInGrid(map, antiRow, antiCol))
                        {
                            allAntinodes.Add((antiRow, antiCol));
                            antiRow += diffRow;
                            antiCol += diffCol;
                        }
                    }
                }
            }

            return allAntinodes;
        }

        private static List<Antenna> FindAntennas(char[][] map)
        {
            List<Antenna> antennas = new();
            for (int rIndex = 0; rIndex < map.Length; rIndex++)
            {
                for (int cIndex = 0; cIndex < map[rIndex].Length; cIndex++)
                {
                    char cellContent = map[rIndex][cIndex];
                    if (char.IsDigit(cellContent) || char.IsLetter(cellContent))
                    {
                        Antenna antenna = new(cellContent, rIndex, cIndex);
                        antennas.Add(antenna);
                    }
                }
            }

            return antennas;
        }

        public override long PartTwo(char[][] map)
        {
            List<Antenna> antennas = FindAntennas(map);
            Dictionary<char, List<Antenna>> mapOfAntennas = antennas
                                                .GroupBy(a => a.Frequency)
                                                .ToDictionary(a => a.Key, a => a.ToList());

            HashSet<(int, int)> allAntinodes = FindAntinodesInMapAnywhere(map, mapOfAntennas);

            var uniqueLocations = allAntinodes.OrderBy(a => a.Item1).ThenBy(a => a.Item2).Select(a => (a.Item1, a.Item2)).ToHashSet();

            return uniqueLocations.Count;
        }
    }
}