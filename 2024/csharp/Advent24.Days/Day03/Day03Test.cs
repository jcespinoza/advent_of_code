using Advent24.Days.Day03;
using AdventOfCode.Commons;
using dotenv.net;
using FluentAssertions;
using Xunit;

namespace Advent24.Days
{
    public class Day03Test : TestEngine<Day03Solver, Instruction[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 165225049L;
        private const long EXPECTED_SOLUTION_PART_2 = 108830766L;

        public Day03Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"
                ],
                Result = 161,
            },
            Examples = [
            ],
            Solution = EXPECTED_SOLUTION_PART_1,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"
                ],
                Result = 48,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };

        [SkippableFact]
        public void ParseInputTest_FullLine()
        {
            // Arrange
            string input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
            // Act
            var result = new OperationParser(input).ReadOperations();
            // Assert
            result.Should().BeEquivalentTo(
            [
                new MulOperation(2, 4),
                new MulOperation(5, 5),
                new MulOperation(11, 8),
                new MulOperation(8, 5),
            ]);
        }

        [SkippableFact]
        public void ParseInputTest_SingleOperationInWholeString()
        {
            // Arrange
            string input = "mul(2,4)";
            // Act
            var result = new OperationParser(input).ReadOperations();
            // Assert
            result.Should().BeEquivalentTo(
            [
                new MulOperation(2, 4),
            ]);
        }

        [SkippableFact]
        public void ParseInputTest_FullLineWithConditionals()
        {
            // Arrange
            string input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))\r\n";
            // Act
            List<Instruction> result = new OperationParser(input).ReadInstructions();
            // Assert
            result.Should().BeEquivalentTo(
            new List<Instruction> {
                new MulOperation(2, 4),
                new DontInstruction(),
                new MulOperation(5, 5),
                new MulOperation(11, 8),
                new DoInstruction(),
                new MulOperation(8, 5),
            });
        }
    }
}