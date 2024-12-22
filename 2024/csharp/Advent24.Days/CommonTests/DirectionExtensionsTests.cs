using Xunit;
using FluentAssertions;

namespace AdventOfCode.Commons.Tests
{
    public class DirectionExtensionsTests
    {
        [Theory]
        [InlineData(Direction.North, Direction.East)]
        [InlineData(Direction.East, Direction.South)]
        [InlineData(Direction.South, Direction.West)]
        [InlineData(Direction.West, Direction.North)]
        public void Turn90Test(Direction original, Direction expected)
        {
            // Act
            var result = original.Turn90();

            // Assert
            result.Should().Be(expected);
        }


        [Theory]
        [InlineData(Direction.North, Direction.South)]
        [InlineData(Direction.East, Direction.West)]
        [InlineData(Direction.West, Direction.East)]
        [InlineData(Direction.South, Direction.North)]
        public void Turn180Test(Direction original, Direction expected)
        {
            // Act
            var result = original.Turn180();

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(Direction.North, Direction.West)]
        [InlineData(Direction.West, Direction.South)]
        [InlineData(Direction.South, Direction.East)]
        [InlineData(Direction.East, Direction.North)]
        public void TurnLeftTest(Direction original, Direction expected)
        {
            // Act
            var result = original.TurnLeft();
            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-1, 0, Direction.North)]
        [InlineData(-1, 1, Direction.NorthEast)]
        [InlineData(0, 1, Direction.East)]
        [InlineData(1, 1, Direction.SouthEast)]
        [InlineData(1, 0, Direction.South)]
        [InlineData(1, -1, Direction.SouthWest)]
        [InlineData(0, -1, Direction.West)]
        [InlineData(-1, -1, Direction.NorthWest)]
        public void ToDirectionTest(int rowOffet, int colOffset, Direction expected)
        {
            // Act
            var result = (rowOffet, colOffset).ToDirection();
            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(Direction.North, -1, 0)]
        [InlineData(Direction.NorthEast, -1, 1)]
        [InlineData(Direction.East, 0, 1)]
        [InlineData(Direction.SouthEast, 1, 1)]
        [InlineData(Direction.South, 1, 0)]
        [InlineData(Direction.SouthWest, 1, -1)]
        [InlineData(Direction.West, 0, -1)]
        [InlineData(Direction.NorthWest, -1, -1)]
        public void ToOffsetsTest(Direction direction, int rowOffset, int colOffset)
        {
            // Act
            var result = direction.ToOffsets();
            // Assert
            result.Should().Be((rowOffset, colOffset));
        }

        [Theory]
        [InlineData(Direction.North, 1, Direction.East)]
        [InlineData(Direction.East, 1, Direction.South)]
        [InlineData(Direction.South, 1, Direction.West)]
        [InlineData(Direction.West, 1, Direction.North)]
        public void RotateOrtogonalTest(Direction original, int steps, Direction expected)
        {
            // Act
            var result = original.RotateOrtogonal(steps);
            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(Direction.North, -1)]
        [InlineData(Direction.South, 1)]
        [InlineData(Direction.East, 0)]
        [InlineData(Direction.West, 0)]
        public void RowOffsetTest(Direction direction, int expectedOffset)
        {
            // Act
            var result = direction.RowOffset();
            // Assert
            result.Should().Be(expectedOffset);
        }

        [Theory]
        [InlineData(Direction.North, 0)]
        [InlineData(Direction.South, 0)]
        [InlineData(Direction.East, 1)]
        [InlineData(Direction.West, -1)]
        public void ColOffsetTest(Direction original, int expectedOffset)
        {
            // Act
            var result = original.ColOffset();
            // Assert
            result.Should().Be(expectedOffset);
        }

        [Theory]
        [InlineData(Direction.North, Direction.South, false)]
        [InlineData(Direction.North, Direction.North, false)]
        [InlineData(Direction.North, Direction.East, true)]
        [InlineData(Direction.North, Direction.West, true)]
        [InlineData(Direction.East, Direction.North, true)]
        [InlineData(Direction.West, Direction.South, true)]

        public void IsPerpedicularTest(Direction first, Direction second, bool expectation)
        {
            // Act
            var result = first.IsPerpedicular(second);
            // Assert
            result.Should().Be(expectation);
        }

        [Theory]
        [InlineData(Direction.North, Direction.South, true)]
        [InlineData(Direction.North, Direction.North, false)]
        [InlineData(Direction.North, Direction.East, false)]
        [InlineData(Direction.North, Direction.West, false)]
        [InlineData(Direction.East, Direction.North, false)]
        [InlineData(Direction.West, Direction.East, true)]

        public void IsOppositeTest(Direction first, Direction second, bool expectation)
        {
            // Act
            var result = first.IsOpposite(second);
            // Assert
            result.Should().Be(expectation);
        }
    }
}