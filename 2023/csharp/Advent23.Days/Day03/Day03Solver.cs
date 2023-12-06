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
                    if (IsDigit(currentChar)){
                        if(buffer == string.Empty)
                        {
                            startIndex = colIndex;
                        }
                        buffer += currentChar;
                    }else if((IsDot(currentChar) || IsSymbol(currentChar)) && buffer != string.Empty)
                    {
                        (bool validPart, char symbol) = IsValidPartNumber(input, buffer, rowIndex, startIndex);
                        if (validPart)
                        {
                            var machinePart = new MachinePart { Number = int.Parse(buffer), Symbol = symbol };
                            validMachineParts.Add(machinePart);
                        }
                        buffer = string.Empty;
                        startIndex = -1;
                    }else if(IsDot(currentChar))
                    {
                        buffer = string.Empty;
                        startIndex = -1;
                    }

                    if(colIndex == input[rowIndex].Length - 1 && buffer != string.Empty)
                    {
                        (bool validPart, char symbol) = IsValidPartNumber(input, buffer, rowIndex, startIndex);
                        if (validPart)
                        {
                            var machinePart = new MachinePart { Number = int.Parse(buffer), Symbol = symbol };
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

        private (bool validPart, char symbol) IsValidPartNumber(string[] input, string buffer, int rowIndex, int startIndex)
        {
            var minRowIndex = rowIndex > 0 ? rowIndex - 1 : 0;
            var maxRowIndex = rowIndex < input.Length - 1 ? rowIndex + 1 : input.Length - 1;
            var maxColIndex = (startIndex+buffer.Length) < input[rowIndex].Length - 1 ? (startIndex + buffer.Length) : input[rowIndex].Length - 1;
            var minColIndex = startIndex > 0 ? startIndex - 1 : 0;

            if(IsSymbol(input[rowIndex][minColIndex]))
            {
                return (true, input[rowIndex][minColIndex]);
            }
            if(IsSymbol(input[rowIndex][maxColIndex]))
            {
                return (true, input[rowIndex][maxColIndex]);
            }

            int safeLength = ComputeSafeLength(buffer.Length, startIndex, minColIndex, maxColIndex);

            string topString = input[minRowIndex].Substring(minColIndex, safeLength);
            int symbolIndex = -1;
            for (int index = 0; index < topString.Length; index++)
            {
                char currentChar = topString[index];
                if (IsSymbol(currentChar))
                {
                    symbolIndex = index;
                    break;
                }
            }

            if (symbolIndex != -1) return (true, topString[symbolIndex]);

            string bottomString = input[maxRowIndex].Substring(minColIndex, safeLength);
            symbolIndex = -1;
            for (int index = 0; index < bottomString.Length; index++)
            {
                char currentChar = bottomString[index];
                if (IsSymbol(currentChar))
                {
                    symbolIndex = index;
                    break;
                }
            }
            if (symbolIndex != -1) return (true, bottomString[symbolIndex]);


            return (false, '.');
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
            throw new NotImplementedException();
        }
    }
}