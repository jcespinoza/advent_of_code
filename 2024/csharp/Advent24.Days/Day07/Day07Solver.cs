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
            foreach (var eq in equations)
            {
                Result<Operation[], string> truthResult = MakeTrue(eq, permutatinoCache);
                if (truthResult.IsSuccess)
                {
                    trueEquations.Add(eq, truthResult.Value);
                }
            }

            long sumOfTestValues = trueEquations.Keys.Sum(eq => eq.TestValue);

            return sumOfTestValues;
        }

        private static Result<Operation[], string> MakeTrue(Equation eq, Dictionary<int, List<Operation[]>> permutatinoCache)
        {
            var operationSlots = eq.Operands.Length - 1;
            Operation[] possibleOperations = [Operation.Add, Operation.Multiply];

            List<Operation[]> permuttedOperations = Permute(possibleOperations, operationSlots, permutatinoCache);

            foreach (var operations in permuttedOperations)
            {
                if (IsEquationTrue(eq, operations))
                {
                    return Result<Operation[], string>.Success(operations);
                }
            }

            return Result<Operation[], string>.Failure("No operations found");
        }

        private static bool IsEquationTrue(Equation eq, Operation[] operations)
        {
            Debug.Assert(eq.Operands.Length == operations.Length + 1, "Invalid operations permutation");

            long result = eq.Operands[0];
            for (int i = 0; i < operations.Length; i++)
            {
                result = operations[i] switch
                {
                    Operation.Add => result + eq.Operands[i + 1],
                    Operation.Multiply => result * eq.Operands[i + 1],
                    _ => throw new InvalidOperationException("Invalid operation")
                };
            }

            return result == eq.TestValue;
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

        public override long PartTwo(Equation[] input)
        {
            throw new NotImplementedException();
        }
    }
}