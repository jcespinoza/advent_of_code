using Advent24.Days.Day02;
using FluentAssertions;
using Xunit;

namespace Advent24.Days
{
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