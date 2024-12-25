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
            var expectedLength = 84;

            path.Count.Should().Be(expectedLength);
        }

        [Fact]
        public void FindCheatsForPicosends_Test2_14()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 2);

            cheatsForPicoseconds.Should().BeGreaterThanOrEqualTo(14);
        }

        [Fact]
        public void FindCheatsForPicosends_Test4_14()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 4);

            cheatsForPicoseconds.Should().BeGreaterThanOrEqualTo(14);
        }

        [Fact]
        public void FindCheatsForPicosends_Test20_1()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 20);

            cheatsForPicoseconds.Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public void FindCheatsForPicosends_Test36_1()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 36);

            cheatsForPicoseconds.Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public void FindCheatsForPicosends_Test38_1()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 38);

            cheatsForPicoseconds.Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public void FindCheatsForPicosends_Test40_1()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 40);

            cheatsForPicoseconds.Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public void FindCheatsForPicosends_Test64_1()
        {
            var path = Day20Solver.FindPath(_racetrack);

            long cheatsForPicoseconds = Day20Solver.FindCheatsForPicosends(_racetrack, path, targetTimeSaved: 64);

            cheatsForPicoseconds.Should().BeGreaterThanOrEqualTo(1);
        }
    }
}
