using AdventOfCode.Commons;
using dotenv.net;
using FluentAssertions;
using Xunit;

namespace Advent24.Days
{
    public class Day09Test : TestEngine<Day09Solver, int[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 123;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day09Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "2333133121414131402"
                ],
                Result = 1928,
            },
            Examples = [
            ],
            Solution = EXPECTED_SOLUTION_PART_1,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = true,
            Example = new()
            {
                RawInput = [
                ],
                Result = 8,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };

        [Fact]
        public void PartOne_Example_01()
        {
            // Arrange
            Day09Solver day09Solver = new Day09Solver();
            var input = PartOne.Example.Input ?? day09Solver.ParseInput(["20202"]);
            var expectation = 0*1+6*1+9*2;
            // represents 001122

            // Act
            var result = day09Solver.PartOne(input);
            // Assert
            result.Should().Be(expectation);
        }
    }
}