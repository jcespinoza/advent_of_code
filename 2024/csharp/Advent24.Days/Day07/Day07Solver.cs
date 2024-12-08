using AdventOfCode.Commons;
using System.Diagnostics;

namespace Advent24.Days
{
    public class Day07Solver : Solver<Equation[], long>
    {
        public Day07Solver() : base(2024, 07) { }

        public override Equation[] ParseInput(IEnumerable<string> input)
            => input.Select(Equation.Parse).ToArray();


        public override long PartOne(Equation[] equations)
        {
            Dictionary<Equation, Operation[]> trueEquations = [];
            Dictionary<int, List<Operation[]>> permutatinoCache = [];
            Dictionary<(long, long), long> concatCache = [];
            Operation[] possibleOperations = [Operation.Add, Operation.Multiply];
            foreach (var eq in equations)
            {
                Result<Operation[], string> truthResult = MakeTrue(eq, possibleOperations, permutatinoCache, concatCache);
                if (truthResult.IsSuccess)
                {
                    trueEquations.Add(eq, truthResult.Value);
                }
            }

            long sumOfTestValues = trueEquations.Keys.Sum(eq => eq.TestValue);

            return sumOfTestValues;
        }

        private static Result<Operation[], string> MakeTrue(Equation eq, Operation[] possibleOperations, Dictionary<int, List<Operation[]>> permutatinoCache, Dictionary<(long, long), long> concatCache)
        {
            var operationSlots = eq.Operands.Length - 1;

            List<Operation[]> permuttedOperations = Permute(possibleOperations, operationSlots, permutatinoCache);

            foreach (var operations in permuttedOperations)
            {
                if (IsEquationTrue(eq, operations, concatCache))
                {
                    return Result<Operation[], string>.Success(operations);
                }
            }

            return Result<Operation[], string>.Failure("No operations found");
        }

        private static bool IsEquationTrue(Equation eq, Operation[] operations, Dictionary<(long, long), long> concatCache)
        {
            Debug.Assert(eq.Operands.Length == operations.Length + 1, "Invalid operations permutation");

            long result = eq.Operands[0];
            for (int i = 0; i < operations.Length; i++)
            {
                result = operations[i] switch
                {
                    Operation.Add => result + eq.Operands[i + 1],
                    Operation.Multiply => result * eq.Operands[i + 1],
                    Operation.Concatenation => ConcatenateDigits(result, eq.Operands[i + 1], concatCache),
                    _ => throw new InvalidOperationException("Invalid operation")
                };
            }

            return result == eq.TestValue;
        }

        private static long ConcatenateDigits(long left, long right, Dictionary<(long,long), long> concatCache)
        {
            if(concatCache.TryGetValue((left,right), out long cached)){
                return cached;
            }

            string digitsLeft = left.ToString();
            string digitsRight = right.ToString();
            var result = long.Parse(digitsLeft + digitsRight);

            concatCache.Add((left,right), result);

            return result;
        }

        private static List<Operation[]> Permute(Operation[] possibleOperations, int operationSlots, Dictionary<int, List<Operation[]>> permutationCache)
        {
            if (permutationCache.ContainsKey(operationSlots))
            {
                return permutationCache[operationSlots];
            }

            List<Operation[]> result = [];
            foreach (Operation op in possibleOperations)
            {
                if (operationSlots == 1)
                {
                    result.Add([op]);
                }
                else
                {
                    var subPermutations = Permute(possibleOperations, operationSlots - 1, permutationCache);
                    foreach (var subPermutation in subPermutations)
                    {
                        result.Add([.. subPermutation, op]);
                    }
                }
            }

           return result;
        }

        public override long PartTwo(Equation[] equations)
        {
            Dictionary<Equation, Operation[]> trueEquations = [];
            Dictionary<int, List<Operation[]>> permutatinoCache = [];
            Dictionary<(long, long), long> concatCache = [];
            Operation[] possibleOperations = [Operation.Add, Operation.Multiply, Operation.Concatenation];
            foreach (var eq in equations)
            {
                Result<Operation[], string> truthResult = MakeTrue(eq, possibleOperations, permutatinoCache, concatCache);
                if (truthResult.IsSuccess)
                {
                    trueEquations.Add(eq, truthResult.Value);
                }
            }

            long sumOfTestValues = trueEquations.Keys.Sum(eq => eq.TestValue);

            return sumOfTestValues;
        }
    }
}