using AdventOfCode.Commons;
using FluentAssertions;
using Xunit;

namespace Advent24.Days.Day15
{
    public class RobotTests
    {
        [Fact]
        public void Move_RobotDoesNotMoveIfHitingWall()
        {
            IEnumerable<string> input = [
                "########",
                "#..O.O.#",
                "##@.O..#",
                "#...O..#",
                "#.#.O..#",
                "#...O..#",
                "#......#",
                "########",
            ];
            char[][] map = input.ToCharArray();
            char[][] expectedMap = input.ToCharArray();

            Day15Solver.MoveRobot(map, (2, 2), Direction.West);

            map.IsTheSameAs(expectedMap).Should().BeTrue();
        }

        [Fact]
        public void Move_CanMoveToEmptySpace()
        {
            IEnumerable<string> input = [
                "########",
                "#..O.O.#",
                "##@.O..#",
                "#...O..#",
                "#.#.O..#",
                "#...O..#",
                "#......#",
                "########",
            ];
            IEnumerable<string> expected = [
                "########",
                "#.@O.O.#",
                "##..O..#",
                "#...O..#",
                "#.#.O..#",
                "#...O..#",
                "#......#",
                "########",
            ];
            char[][] map = input.ToCharArray();
            char[][] expectedMap = expected.ToCharArray();

            Day15Solver.MoveRobot(map, (2, 2), Direction.North);

            map.IsTheSameAs(expectedMap).Should().BeTrue();
        }

        [Fact]
        public void Move_WillNotMoveIfFacingBorder()
        {
            IEnumerable<string> input = [
                "########",
                "#.@O.O.#",
                "##..O..#",
                "#...O..#",
                "#.#.O..#",
                "#...O..#",
                "#......#",
                "########",
            ];
            char[][] map = input.ToCharArray();
            char[][] expectedMap = input.ToCharArray();

            Day15Solver.MoveRobot(map, (1, 2), Direction.North);

            map.IsTheSameAs(expectedMap).Should().BeTrue();
        }

        [Fact]
        public void Move_CanPushOneBoxToRight()
        {
            IEnumerable<string> input = [
                "########",
                "#.@O.O.#",
                "##..O..#",
                "#...O..#",
                "#.#.O..#",
                "#...O..#",
                "#......#",
                "########",
            ];
            IEnumerable<string> expected = [
                "########",
                "#..@OO.#",
                "##..O..#",
                "#...O..#",
                "#.#.O..#",
                "#...O..#",
                "#......#",
                "########",
            ];
            char[][] map = input.ToCharArray();
            char[][] expectedMap = expected.ToCharArray();

            Day15Solver.MoveRobot(map, (1, 2), Direction.East);

            map.IsTheSameAs(expectedMap).Should().BeTrue();
        }

        [Fact]
        public void Move_CanPushTwoBoxesToRight()
        {
            IEnumerable<string> input = [
                "########",
                "#..@OO.#",
                "##..O..#",
                "#...O..#",
                "#.#.O..#",
                "#...O..#",
                "#......#",
                "########",
            ];
            IEnumerable<string> expected = [
               "########",
                "#...@OO#",
                "##..O..#",
                "#...O..#",
                "#.#.O..#",
                "#...O..#",
                "#......#",
                "########",
            ];
            char[][] map = input.ToCharArray();
            char[][] expectedMap = expected.ToCharArray();

            Day15Solver.MoveRobot(map, (1, 3), Direction.East);

            map.IsTheSameAs(expectedMap).Should().BeTrue();
        }

        [Fact]
        public void Move_CanNotPushTwoBoxesToWall()
        {
            IEnumerable<string> input = [
                "########",
                "#...@OO#",
                "##..O..#",
                "#...O..#",
                "#.#.O..#",
                "#...O..#",
                "#......#",
                "########",
            ];
            IEnumerable<string> expected = [
               "########",
                "#...@OO#",
                "##..O..#",
                "#...O..#",
                "#.#.O..#",
                "#...O..#",
                "#......#",
                "########",
            ];
            char[][] map = input.ToCharArray();
            char[][] expectedMap = expected.ToCharArray();

            Day15Solver.MoveRobot(map, (1, 4), Direction.East);

            map.IsTheSameAs(expectedMap).Should().BeTrue();
        }

        [Fact]
        public void Move_CanPushTwoBoxesToBottom()
        {
            IEnumerable<string> input = [
                "########",
                "#...@OO#",
                "##..O..#",
                "#...O..#",
                "#.#.O..#",
                "#...O..#",
                "#......#",
                "########",
            ];
            IEnumerable<string> expected = [
               "########",
                "#....OO#",
                "##..@..#",
                "#...O..#",
                "#.#.O..#",
                "#...O..#",
                "#...O..#",
                "########",
            ];
            char[][] map = input.ToCharArray();
            char[][] expectedMap = expected.ToCharArray();

            Day15Solver.MoveRobot(map, (1, 4), Direction.South);

            map.IsTheSameAs(expectedMap).Should().BeTrue();
        }

        [Fact]
        public void Move_CanNotPushTwoBoxesToBottomWall()
        {
            IEnumerable<string> input = [
                "########",
                "#....OO#",
                "##..@..#",
                "#...O..#",
                "#.#.O..#",
                "#...O..#",
                "#...O..#",
                "########",
            ];
            IEnumerable<string> expected = [
                "########",
                "#....OO#",
                "##..@..#",
                "#...O..#",
                "#.#.O..#",
                "#...O..#",
                "#...O..#",
                "########",
            ];
            char[][] map = input.ToCharArray();
            char[][] expectedMap = expected.ToCharArray();

            Day15Solver.MoveRobot(map, (2, 4), Direction.South);

            map.IsTheSameAs(expectedMap).Should().BeTrue();
        }

        [Fact]
        public void Move_CanPushBoxToNearestWall()
        {
            IEnumerable<string> input = [
                "########",
                "#....OO#",
                "##.....#",
                "#.....O#",
                "#.#.O@.#",
                "#...O..#",
                "#...O..#",
                "########",
            ];
            IEnumerable<string> expected = [
                "########",
                "#....OO#",
                "##.....#",
                "#.....O#",
                "#.#O@..#",
                "#...O..#",
                "#...O..#",
                "########",
            ];
            char[][] map = input.ToCharArray();
            char[][] expectedMap = expected.ToCharArray();

            Day15Solver.MoveRobot(map, (4, 5), Direction.West);

            map.IsTheSameAs(expectedMap).Should().BeTrue();
        }
    }
}
