using AdventOfCode.Commons;
using FluentAssertions.Data;

namespace Advent24.Days
{
    public class Day04Solver : Solver<char[][], long>
    {
        public Day04Solver() : base(2024, 04) { }
        public Direction[] AllDirections = [
            Direction.North,
            Direction.NorthEast,
            Direction.East,
            Direction.SouthEast,
            Direction.South,
            Direction.SouthWest,
            Direction.West,
            Direction.NorthWest,
        ];

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

        private int FindWordCountInAllDirections(char[][] inputGrid, int startRow, int startCol, string word)
        {
            int countOfWords = 0;
            foreach (var direction in AllDirections)
            {
                bool isWordInDirection = FindWordCountInDirection(inputGrid, startRow, startCol, word, direction);

                if(isWordInDirection) countOfWords++;
            }

            return countOfWords;
        }

        private bool FindWordCountInDirection(char[][] inputGrid, int startRow, int startCol, string word, Direction direction)
        {
            (int currentRow, int currentCol) = (startRow, startCol);
            for (int index = 1; index < word.Length; index++)
            {
                var result = GridWalker<char>.Move(inputGrid, direction, currentRow, currentCol);
                if(result.IsFailure)
                {
                    // no more cells in that directions
                    return false;
                }

                (int newRow, int newCol) = result.Value;

                bool charactersMatch = inputGrid[newRow][newCol] == word[index];
                if (!charactersMatch)
                {
                    // character at that position does not match the expected value
                    return false;
                }

                // set currentRow, currentCol for next iteration
                (currentRow, currentCol) = (newRow, newCol);
            }

            // We were able to match the whole word
            return true;
        }

        public override long PartTwo(char[][] input)
        {
            throw new NotImplementedException();
        }
    }
}