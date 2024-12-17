using Advent24.Days.Day14;
using AdventOfCode.Commons;
using dotenv.net;
using FluentAssertions;
using Xunit;

namespace Advent24.Days
{
    public class Day14Test : TestEngine<Day14Solver, Robot[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 230436441L;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day14Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "p=0,4 v=3,-3",
                    "p=6,3 v=-1,-3",
                    "p=10,3 v=-1,2",
                    "p=2,0 v=2,-1",
                    "p=0,0 v=1,3",
                    "p=3,0 v=-2,-2",
                    "p=7,6 v=-1,-3",
                    "p=3,0 v=-1,-2",
                    "p=9,3 v=2,3",
                    "p=7,3 v=-1,2",
                    "p=2,4 v=2,-3",
                    "p=9,5 v=-3,-3",
                ],
                Result = 21,
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
        public void PartOne_SimpleExampleTest()
        {
            // Arrange
            IEnumerable<string> input = [
                    "p=0,4 v=3,-3",
                    "p=6,3 v=-1,-3",
                    "p=10,3 v=-1,2",
                    "p=2,0 v=2,-1",
                    "p=0,0 v=1,3",
                    "p=3,0 v=-2,-2",
                    "p=7,6 v=-1,-3",
                    "p=3,0 v=-1,-2",
                    "p=9,3 v=2,3",
                    "p=7,3 v=-1,2",
                    "p=2,4 v=2,-3",
                    "p=9,5 v=-3,-3",
                ];
            int expectedSafetyFactor = 12;
            Robot[] robots = new Day14Solver().ParseInput(input);
            // Act
            var result = Day14Solver.CalculateSafetyFactor(robots, 100, 11, 7);
            // Assert
            result.Should().Be(expectedSafetyFactor);
        }
    }
}