using Advent24.Days.Day02;
using AdventOfCode.Commons;
using dotenv.net;
using FluentAssertions;
using Xunit;

namespace Advent24.Days
{
    public class Day02Test : TestEngine<Day02Solver, Report[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 631;
        private const long EXPECTED_SOLUTION_PART_2 = 665;

        public Day02Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "7 6 4 2 1",
                    "1 2 7 8 9",
                    "9 7 6 2 1",
                    "1 3 2 4 5",
                    "8 6 4 4 1",
                    "1 3 6 7 9",
                ],
                Result = 2,
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
                    "7 6 4 2 1",
                    "1 2 7 8 9",
                    "9 7 6 2 1",
                    "1 3 2 4 5",
                    "8 6 4 4 1",
                    "1 3 6 7 9",
                ],
                Result = 4,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }


    public class ReportTests
    {
        [Fact]
        public void IsSafe_ShouldReturnTrueWhenThereAreTwoLevelsOrLess()
        {
            // Arrange
            Report report = new()
            {
                Levels =
                [
                    1,
                ]
            };

            // Act
            bool result = report.IsSafe();

            // Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public void IsSafe_ShouldReturnTrueWhenAllLevelsAreIncreasing()
        {
            // Arrange
            Report report = new()
            {
                Levels =
                [
                    1,
                    2,
                    3,
                    4,
                    5,
                ]
            };

            // Act
            bool result = report.IsSafe();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsSafe_ShouldReturnTrueWhenAllLevelsAreDecreasing()
        {
            // Arrange
            Report report = new()
            {
                Levels =
                [
                    5,
                    4,
                    3,
                    2,
                    1,
                ]
            };

            // Act
            bool result = report.IsSafe();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsSafe_ShouldReturnTrueWhenMaximumDifferenceIsLowerThanThree()
        {
            // Arrange
            Report report = new()
            {
                Levels =
                [
                    1,
                    4,
                    7,
                    8,
                    9,
                ]
            };

            // Act
            bool result = report.IsSafe();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsSafe_ShouldReturnFalseWhenMaximumDifferenceIsHigherThanThree()
        {
            // Arrange
            Report report = new()
            {
                Levels =
                [
                    1,
                    2,
                    7,
                    8,
                    9,
                ]
            };

            // Act
            bool result = report.IsSafe();

            // Assert
            result.Should().BeFalse();
        }

        
        [Fact]
        public void IsSafe_ShouldReturnFalseWhenThereIsAnIncreaseAndDecrease()
        {
            // Arrange
            Report report = new()
            {
                Levels =
                [
                    1,
                    3,
                    2,
                    4,
                    5,
                ]
            };

            // Act
            bool result = report.IsSafe();

            // Assert
            result.Should().BeFalse();
        }
        
        [Fact]
        public void IsSafe_ShouldReturnFalseWhenThereIsAnAdjacentPairWithNoDifference()
        {
            // Arrange
            Report report = new()
            {
                Levels =
                [
                    1,
                    1,
                    2,
                    4,
                    5,
                ]
            };

            // Act
            bool result = report.IsSafe();

            // Assert
            result.Should().BeFalse();
        }


    }
}