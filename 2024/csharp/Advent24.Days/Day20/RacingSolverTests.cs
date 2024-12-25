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
    }
}
