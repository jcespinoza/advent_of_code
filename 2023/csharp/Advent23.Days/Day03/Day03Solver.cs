using Advent23.Days.Day03;
using AdventOfCode.Commons;

namespace Advent23.Days
{
    public class Day03Solver : Solver<string[], long>
    {
        public Day03Solver() : base(2023, 03) { }

        public override string[] ParseInput(IEnumerable<string> input)
            => input.ToArray();


        public override long PartOne(string[] input)
        {
            List<MachinePart> parts = GetParts(input);
            return parts.Sum(p => p.Number);
        }

        private List<MachinePart> GetParts(string[] input)
        {
            var validMachineParts = new List<MachinePart>();
            for (int rowIndex = 0; rowIndex < input.Length; rowIndex++)
            {
                int startIndex = -1;
                string buffer = string.Empty;
                for (int colIndex = 0; colIndex < input[rowIndex].Length; colIndex++)
                {
                    char currentChar = input[rowIndex][colIndex];
                    if (IsDigit(currentChar))
                    {
                        if (buffer == string.Empty)
                        {
                            startIndex = colIndex;
                        }
                        buffer += currentChar;
                    }
                    else if ((IsDot(currentChar) || IsSymbol(currentChar)) && buffer != string.Empty)
                    {
                        var result = IsValidPartNumber(input, buffer, rowIndex, startIndex);
                        if (result.ValidPart)
                        {
                            var machinePart = new MachinePart { 
                                Number = int.Parse(buffer), 
                                Symbol = result.Symbol, 
                                SymbolRow = result.SymbolRow, 
                                SymbolCol = result.SymbolCol
                            };
                            validMachineParts.Add(machinePart);
                        }
                        buffer = string.Empty;
                        startIndex = -1;
                    }
                    else if (IsDot(currentChar))
                    {
                        buffer = string.Empty;
                        startIndex = -1;
                    }

                    if (colIndex == input[rowIndex].Length - 1 && buffer != string.Empty)
                    {
                        var result = IsValidPartNumber(input, buffer, rowIndex, startIndex);
                        if (result.ValidPart)
                        {
                            var machinePart = new MachinePart
                            {
                                Number = int.Parse(buffer),
                                Symbol = result.Symbol,
                                SymbolRow = result.SymbolRow,
                                SymbolCol = result.SymbolCol
                            };
                            validMachineParts.Add(machinePart);
                        }
                    }
                }
            }

            return validMachineParts;
        }

        private bool IsDigit(char c) => char.IsDigit(c);
        private bool IsDot(char c) => c == '.';
        private bool IsSymbol(char c) => !IsDigit(c) && !IsDot(c);

        private (bool ValidPart, char Symbol, int SymbolRow, int SymbolCol) IsValidPartNumber(
            string[] input, string buffer, int rowIndex, int startIndex
        )
        {
            var minRowIndex = rowIndex > 0 ? rowIndex - 1 : 0;
            var maxRowIndex = rowIndex < input.Length - 1 ? rowIndex + 1 : input.Length - 1;
            var maxColIndex = (startIndex + buffer.Length) < input[rowIndex].Length - 1
                ? (startIndex + buffer.Length) 
                : input[rowIndex].Length - 1;
            var minColIndex = startIndex > 0 ? startIndex - 1 : 0;

            if (IsSymbol(input[rowIndex][minColIndex]))
            {
                return (true, input[rowIndex][minColIndex], rowIndex, minColIndex);
            }
            if (IsSymbol(input[rowIndex][maxColIndex]))
            {
                return (true, input[rowIndex][maxColIndex], rowIndex, maxColIndex);
            }

            int safeLength = ComputeSafeLength(buffer.Length, startIndex, minColIndex, maxColIndex);

            string topString = input[minRowIndex].Substring(minColIndex, safeLength);
            int symbolIndex = -1;
            for (int index = 0; index < topString.Length; index++)
            {
                char currentChar = topString[index];
                if (IsSymbol(currentChar))
                {
                    symbolIndex = minColIndex + index;
                    break;
                }
            }

            if (symbolIndex != -1) return (true, input[minRowIndex][symbolIndex], minRowIndex, symbolIndex);

            string bottomString = input[maxRowIndex].Substring(minColIndex, safeLength);
            symbolIndex = -1;
            for (int index = 0; index < bottomString.Length; index++)
            {
                char currentChar = bottomString[index];
                if (IsSymbol(currentChar))
                {
                    symbolIndex = minColIndex + index;
                    break;
                }
            }
            if (symbolIndex != -1) return (true, input[maxRowIndex][symbolIndex], maxRowIndex, symbolIndex);


            return (false, '.', -1, -1);
        }

        private int ComputeSafeLength(int bufferLength, int startIndex, int minColIndex, int maxColIndex)
        {
            int safeLength = bufferLength;

            // Are there characters on the left side
            if (minColIndex < startIndex) safeLength++;

            // Are there characters on the right side
            if (maxColIndex >= (startIndex + bufferLength)) safeLength++;

            return safeLength;
        }

        public override long PartTwo(string[] input)
        {
            List<MachinePart> parts = GetParts(input);
            var onlyGearSymbols = parts.Where(s => s.Symbol == '*');
            var groupedGears = parts.GroupBy(s => new { s.SymbolRow, s.SymbolCol });
            var groupsWithTwoParts = groupedGears.Where(g => g.Count() == 2);
            int finalSum = 0;
            foreach (var item in groupsWithTwoParts)
            {
                var product = 1;
                foreach (var part in item)
                {
                    product *= part.Number;
                }
                finalSum += product;
            }

            return finalSum;
        }
    }
}