using FluentAssertions;
using Xunit;

namespace Advent24.Days.Day20
{
    public class RacingSolverTests
    {
        Day20Solver _solver;
        readonly IEnumerable<string> _rawInput = [
                "###############",
                "#...#...#.....#",
                "#.#.#.#.#.###.#",
                "#S#...#.#.#...#",
                "#######.#.#.###",
                "#######.#.#...#",
                "#######.#.###.#",
                "###..E#...#...#",
                "###.#######.###",
                "#...###...#...#",
                "#.#####.#.###.#",
                "#.#...#.#.#...#",
                "#.#.#.#.#.#.###",
                "#...#...#...###",
                "###############",
                ];
        readonly char[][] _racetrack;

        public RacingSolverTests()
        {
            _solver = new();
            _racetrack = _solver.ParseInputOne(_rawInput);
        }

        [Fact]
        public void FindPathLength_Test01()
        {
            var path = Day20Solver.FindPath(_racetrack);
            var expectedLength = 85;

            path.Count.Should().Be(expectedLength);
        }

        [Fact]
        public void FindCheatsForPicosends_Test2_14()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 2, useExact: true);

            cheatsForPicoseconds.Should().Be(14);
        }

        [Fact]
        public void FindCheatsForPicosends_Test4_14()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 4, useExact: true);

            cheatsForPicoseconds.Should().Be(14);
        }

        [Fact]
        public void FindCheatsForPicosends_Test6_2()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 6, useExact: true);

            cheatsForPicoseconds.Should().Be(2);
        }

        [Fact]
        public void FindCheatsForPicosends_Test8_4()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 8, useExact: true);

            cheatsForPicoseconds.Should().Be(4);
        }

        [Fact]
        public void FindCheatsForPicosends_Test10_2()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 10, useExact: true);

            cheatsForPicoseconds.Should().Be(2);
        }

        [Fact]
        public void FindCheatsForPicosends_Test12_3()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 12, useExact: true);

            cheatsForPicoseconds.Should().Be(3);
        }

        [Fact]
        public void FindCheatsForPicosends_Test20_1()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 20, useExact: true);

            cheatsForPicoseconds.Should().Be(1);
        }

        [Fact]
        public void FindCheatsForPicosends_Test36_1()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 36, useExact: true);

            cheatsForPicoseconds.Should().Be(1);
        }

        [Fact]
        public void FindCheatsForPicosends_Test38_1()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 38, useExact: true);

            cheatsForPicoseconds.Should().Be(1);
        }

        [Fact]
        public void FindCheatsForPicosends_Test40_1()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 40, useExact: true);

            cheatsForPicoseconds.Should().Be(1);
        }

        [Fact]
        public void FindCheatsForPicosends_Test64_1()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 64, useExact: true);

            cheatsForPicoseconds.Should().Be(1);
        }

        [Theory]
        [InlineData(50, 32)]
        [InlineData(52, 31)]
        [InlineData(54, 29)]
        [InlineData(56, 39)]
        [InlineData(58, 25)]
        [InlineData(60, 23)]
        [InlineData(62, 20)]
        [InlineData(64, 19)]
        [InlineData(66, 12)]
        [InlineData(68, 14)]
        [InlineData(70, 12)]
        [InlineData(72, 22)]
        [InlineData(74, 4)]
        [InlineData(76, 3)]
        public void FindAdvancedCheatsForPicosends_Test(int targetTimeSaved, int expectedCheats)
        {
            var path = Day20Solver.FindPath(_racetrack);
            long cheatsForPicoseconds = Day20Solver.FindAdvancedCheatsForPicosends(_racetrack, path, targetTimeSaved, useExact: true);
            cheatsForPicoseconds.Should().Be(expectedCheats);
        }
    }
}
