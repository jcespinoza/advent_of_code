using AdventOfCode.Commons;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent24.Days.CommonTests
{
    public class GridWalkerTests
    {
        int[][] testGrid = [
            [5, 2, 6, 9, 1, 1],
            [1, 8, 5, 7, 6, 1],
            [3, 3, 6, 4, 7, 9],
            [5, 5, 1, 4, 8, 7],
            [6, 7, 5, 1, 2, 1],
            [1, 8, 5, 4, 9, 6],
        ];
        // Given the following grid
        // 5 2 6 9 1 1 
        // 1 8 5 7 6 1 
        // 3 3 6 4 7 9 
        // 5 5 1 4 8 7 
        // 6 7 5 1 2 1 
        // 1 8 5 4 9 6 
        //
        // Four unit tests with different examples for each entry in the Direction enum for the grid above
        [Theory]
        [InlineData(Direction.North, 1, 1, 0, 1, true)]
        [InlineData(Direction.NorthEast, 1, 1, 0, 2, true)]
        [InlineData(Direction.East, 1, 1, 1, 2, true)]
        [InlineData(Direction.SouthEast, 1, 1, 2, 2, true)]
        [InlineData(Direction.South, 1, 1, 2, 1, true)]
        [InlineData(Direction.SouthWest, 1, 1, 2, 0, true)]
        [InlineData(Direction.West, 1, 1, 1, 0, true)]
        [InlineData(Direction.NorthWest, 1, 1, 0, 0, true)]

        [InlineData(Direction.North, 0, 1, 0, 1, false)]
        [InlineData(Direction.NorthEast, 0, 5, 0, 6, false)]
        [InlineData(Direction.East, 5, 5, 5, 6, false)]
        [InlineData(Direction.SouthEast, 5, 5, 6, 6, false)]
        [InlineData(Direction.South, 5, 5, 6, 5, false)]
        [InlineData(Direction.SouthWest, 5, 0, 6, 0, false)]
        [InlineData(Direction.West, 5, 0, 5, 0, false)]
        [InlineData(Direction.NorthWest, 0, 0, 0, 0, false)]

        public void WalkingTests(Direction direction, int startRow, int startCol, int expRow, int expCol, bool sholdSuceed)
        {
            // Act
            Result<(int row, int col), string> result = GridWalker<int>.Move(testGrid, direction, startRow, startCol);

            // Assert
            if (sholdSuceed)
            {
                result.IsSuccess.Should().BeTrue();
                result.Value.row.Should().Be(expRow);
                result.Value.col.Should().Be(expCol);
            }
            else
            {
                result.IsFailure.Should().BeTrue();
            }
        }

        [Theory]
        [InlineData(Direction.North, 1, 1, 2, 1, 0, false)]
        [InlineData(Direction.NorthEast, 2, 1, 2, 0, 3, true)]
        public void WalkingArbitraryStepsTests(Direction direction, int startRow, int startCol, int steps, int expRow, int expCol, bool shouldSucceed)
        {
            // Act
            Result<(int row, int col), string> result = GridWalker<int>.Move(testGrid, direction, startRow, startCol, steps);
            // Assert
            if (shouldSucceed)
            {
                result.IsSuccess.Should().BeTrue();
                result.Value.row.Should().Be(expRow);
                result.Value.col.Should().Be(expCol);
            }
            else
            {
                result.IsFailure.Should().BeTrue();
            }
        }
    }
}
