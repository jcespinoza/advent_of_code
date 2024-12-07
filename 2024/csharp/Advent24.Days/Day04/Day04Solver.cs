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

        public override long PartTwo(char[][] input)
        {
            int xwordCount= FindXWordCount(input, "MAS");

            return xwordCount;
        }

        private int FindWordCount(char[][] inputGrid, string word)
        {
            int successCount = 0;
            for (int row = 0; row < inputGrid.Length; row++)
            {
                for (int col = 0; col < inputGrid[row].Length; col++)
                {
                    if (inputGrid[row][col] == word[0])
                    {
                        int countFromCell = FindWordCountInAllDirections(inputGrid, row, col, word);
                        successCount += countFromCell;
                    }
                }
            }

            return successCount;
        }

        private int FindWordCountInAllDirections(char[][] inputGrid, int startRow, int startCol, string word)
        {
            int countOfWords = 0;
            foreach (var direction in AllDirections)
            {
                bool isWordInDirection = IsWordInDirection(inputGrid, startRow, startCol, word, direction);

                if(isWordInDirection) countOfWords++;
            }

            return countOfWords;
        }

        private bool IsWordInDirection(char[][] inputGrid, int startRow, int startCol, string word, Direction direction)
        {
            if (word[0] != inputGrid[startRow][startCol])
            {
                // the first character does not match
                return false;
            }

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

                char cellChar = inputGrid[newRow][newCol];
                char wordChar = word[index];
                bool charactersMatch = cellChar == wordChar;
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

        private int FindXWordCount(char[][] input, string word)
        {
            if(word.Length % 2 == 0)
            {
                throw new ArgumentException("Word length must be odd in order to form an X");
            }

            int successCount = 0;
            int halfLength = word.Length / 2;
            char middleChar = word[halfLength];

            for (int row = 0; row < input.Length; row++)
            {
                for (int col = 0; col < input[row].Length; col++)
                {
                    char currentChar = input[row][col];
                    if (currentChar != middleChar) continue;
                    
                    var nWest = GridWalker<char>.Move(input, Direction.NorthWest, row, col, halfLength);
                    var sWest = GridWalker<char>.Move(input, Direction.SouthWest, row, col, halfLength);
                    var nEast = GridWalker<char>.Move(input, Direction.NorthEast, row, col, halfLength);
                    var sEast = GridWalker<char>.Move(input, Direction.SouthEast, row, col, halfLength);

                    if (nWest.IsFailure || sWest.IsFailure || nEast.IsFailure || sEast.IsFailure)
                    {
                        // this cell can not be the center of the X. skip
                        continue;
                    }

                    (int nwRow, int nwCol) = nWest.Value;
                    (int swRow, int swCol) = sWest.Value;
                    (int neRow, int neCol) = nEast.Value;
                    (int seRow, int seCol) = sEast.Value;

                    // Find the word from all four corners
                    bool containsWordFromNW = IsWordInDirection(input, nwRow, nwCol, word, Direction.SouthEast);
                    bool containsWordFromSW = IsWordInDirection(input, swRow, swCol, word, Direction.NorthEast);
                    bool containsWordFromNE = IsWordInDirection(input, neRow, neCol, word, Direction.SouthWest);
                    bool containsWordFromSE = IsWordInDirection(input, seRow, seCol, word, Direction.NorthWest);

                    // Consolidate pairs from the four cardinal directions
                    bool hasXFromWest = containsWordFromNW && containsWordFromSW;
                    bool hasXFromEast = containsWordFromNE && containsWordFromSE;
                    bool hasXFromNorth = containsWordFromNE && containsWordFromNW;
                    bool hasXFromSouth = containsWordFromSE && containsWordFromSW;

                    // If there is any X, increment the count
                    bool formsXword = hasXFromWest || hasXFromEast || hasXFromNorth || hasXFromSouth;
                    if (formsXword)
                    {
                        successCount++;
                    }                    
                }
            }

            return successCount;
        }
    }
}