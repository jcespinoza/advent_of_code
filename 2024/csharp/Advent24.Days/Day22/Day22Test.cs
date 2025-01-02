using AdventOfCode.Commons;
using dotenv.net;
using Xunit;

namespace Advent24.Days
{
    public class Day22Test : TestEngine<Day22Solver, int[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 19822877190L;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day22Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "1",
                    "10",
                    "100",
                    "2024",
                ],
                Result = 37327623,
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

        [Theory]
        [InlineData(123, 1, 15887950)]
        [InlineData(123, 2, 16495136)]
        [InlineData(123, 3, 527345)]
        [InlineData(123, 4, 704524)]
        [InlineData(123, 5, 1553684)]
        [InlineData(123, 6, 12683156)]
        [InlineData(123, 7, 11100544)]
        [InlineData(123, 8, 12249484)]
        [InlineData(123, 9, 7753432)]
        [InlineData(123, 10, 5908254)]
        public void FindNthSecretNumber_Test(int initial, int step, long expected)
        {
            // Arrange
            // Act
            long result = Day22Solver.FindNthSecretNumber(initial, step);
            // Assert
            Assert.Equal(expected, result);
        }
    }
}