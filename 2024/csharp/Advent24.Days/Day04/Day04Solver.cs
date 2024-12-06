using AdventOfCode.Commons;
using FluentAssertions.Data;

namespace Advent24.Days
{
    public class Day04Solver : Solver<char[][], long>
    {
        public Day04Solver() : base(2024, 04) { }

        public override char[][] ParseInput(IEnumerable<string> input)
            => input.Select(l => l.ToCharArray()).ToArray();


        public override long PartOne(char[][] inputGrid)
        {
            int wordCount = FindWordCount(inputGrid, "XMAS");

            return wordCount;
        }

        private int FindWordCount(char[][] inputGrid, string word)
        {
            int successCounts = 0;
            for (int row = 0; row < inputGrid.Length; row++)
            {
                for (int col = 0; col < inputGrid[row].Length; col++)
                {
                    if (inputGrid[row][col] == word[0])
                    {
                        int countFromCell = FindWordCountInAllDirections(inputGrid, row, col, word);
                        successCounts += countFromCell;
                    }
                }
            }

            return successCounts;
        }

        private int FindWordCountInAllDirections(char[][] inputGrid, int row, int col, string word)
        {
            throw new NotImplementedException();
        }

        public override long PartTwo(char[][] input)
        {
            throw new NotImplementedException();
        }
    }
}