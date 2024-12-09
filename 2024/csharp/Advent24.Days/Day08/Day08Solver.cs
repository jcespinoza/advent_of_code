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
            Dictionary<(int,int), List<(Antenna, int)>> distancesToAntennas = FindDistances(map, antennas);
            HashSet<(int, int, char)> allAntinodes = FindAntinodesInMap(distancesToAntennas);

            var uniqueLocations = allAntinodes.Select(a => (a.Item1, a.Item2)).ToHashSet();

            return uniqueLocations.Count;
        }

        private static HashSet<(int, int, char)> FindAntinodesInMap(Dictionary<(int, int), List<(Antenna, int)>> distancesToAntennas)
        {
            HashSet<(int, int, char)> antinodeLocations = [];
            foreach(var distEntry in distancesToAntennas)
            {
                (int row, int col) = distEntry.Key;
                List<char> antinodeFrequencies = FindAntinodes(row, col, distEntry.Value);

                var locations = antinodeFrequencies.Select( f =>
                {
                    return (row, col, f);
                }).ToList();

                locations.ForEach( l => antinodeLocations.Add(l));
            }

            return antinodeLocations;
        }

        private static List<char> FindAntinodes(int srcRow, int srcCol, List<(Antenna, int)> distanceMap)
        {
            HashSet<char> frequencies = [];
            foreach (var firstPair in distanceMap)
            {
                (Antenna firstAntenna, int firstDistance) = firstPair;
                (int firsRow, int firstCol) = (firstAntenna.Row, firstAntenna.Col);
                foreach (var secondPair in distanceMap)
                {
                    (Antenna secAntenna, int secDistance) = secondPair;
                    (int secRow, int secCol) = (secAntenna.Row, secAntenna.Col);

                    // Skip if we're comparing the same antenna.
                    if (firstAntenna == secAntenna) continue;
                    // Skip if the frequencies of the antennas are different
                    if (secAntenna.Frequency != firstAntenna.Frequency) continue;
                    // Skip if an of the frequency the current point is 

                    bool distanceRatioIsHalf = secDistance/firstDistance == 2 || firstDistance/secDistance == 2;
                    bool colinearityConditon = GridWalker<char>.ArePointsColinear((srcRow, srcCol), (secRow, secCol), (firsRow, firstCol), firstDistance,secDistance);
                    if (distanceRatioIsHalf && colinearityConditon)
                    {
                        frequencies.Add(firstAntenna.Frequency);
                    }
                }

            }

            return [.. frequencies];
        }

        private static Dictionary<(int, int), List<(Antenna, int)>> FindDistances(char[][] map, List<Antenna> antennas)
        {
            Dictionary<(int, int), List<(Antenna, int)>> result = [];

            for (int rIndex = 0; rIndex < map.Length; rIndex++)
            {
                for (int cIndex = 0; cIndex < map[rIndex].Length; cIndex++)
                {
                    List<(Antenna, int)> distancesToAntennas = [];
                    foreach (var antenna in antennas)
                    {
                        int distanceToAntenna = GridWalker<char>.FindDistance((rIndex, cIndex), (antenna.Row, antenna.Col));
                        if (distanceToAntenna == 0) continue; // An antenna is located here, don't count this distance
                        distancesToAntennas.Add((antenna, distanceToAntenna));
                    }
                    result[(rIndex, cIndex)] = distancesToAntennas;
                }
            }

            return result;
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
            throw new NotImplementedException();
        }
    }
}