using FluentAssertions;
using Xunit;

namespace Advent24.Days.Day14
{
    /// <summary>
    /// Unit tests for the Robot class
    /// </summary>
    public class RobotTests
    {
        [Theory]
        [InlineData(2, 4, 2, -3, 1, 11, 7, 4, 1)]
        [InlineData(2, 4, 2, -3, 2, 11, 7, 6, 5)]
        [InlineData(2, 4, 2, -3, 3, 11, 7, 8, 2)]
        [InlineData(2, 4, 2, -3, 4, 11, 7, 10, 6)]
        [InlineData(2, 4, 2, -3, 5, 11, 7, 1, 3)]
        public void CalculatePosition_WarpTests(int posX, int posY, int velX, int velY, int time, int gridWith, int gridHeight, int expectedX, int expectedY) {
            Robot robot = new() { VelY = velY, VelX = velX, PosY = posY, PosX = posX };
            (int, int) newPosition = Robot.CalculatePosition(robot, time, gridWith, gridHeight);

            newPosition.Should().Be((expectedX, expectedY));
        }
    }
}
