using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent24.Days.Day21
{
    public class KeypadTests
    {

        Day21Solver _solver;
        readonly Dictionary<((int, int), (int, int)), List<List<char>>> allNumPaths;
        readonly Dictionary<((int, int), (int, int)), List<List<char>>> allDirPaths;

        public KeypadTests()
        {
            _solver = new();

            allNumPaths = Day21Solver.FindAllPaths(Day21Solver.NumButtons, Day21Solver.NumPadMap);
            allDirPaths = Day21Solver.FindAllPaths(Day21Solver.DButtons, Day21Solver.DPadMap);
        }

        [Fact]
        public void FindPathFromButton_A_A()
        {
            (int, int) start = Day21Solver.NumButtons['A'];
            (int, int) end = Day21Solver.NumButtons['A'];
            List<List<char>> possibilities = Day21Solver.FindPossiblePaths(start, end, Day21Solver.NumPadMap);
            possibilities.Count.Should().Be(1);
        }

        [Theory]
        [InlineData('3', '8', new string[] { "^^<A", "^<^A", "<^^A" })]
        [InlineData('0', '3', new string[] { "^>A", ">^A" })]
        [InlineData('A', '9', new string[] { "^^^A" })]
        public void FindPathFromButton_X_To_Y(char cStart, char cEnd, string[] possiblePaths)
        {
            (int,int) start = Day21Solver.NumButtons[cStart];
            (int, int) end = Day21Solver.NumButtons[cEnd];
            List<List<char>> possibilities = Day21Solver.FindPossiblePaths(start, end, Day21Solver.NumPadMap);
            List<string> pathStrings = possibilities
                .Select(p => string.Join("", p))
                .ToList();

            pathStrings.Should().BeEquivalentTo(possiblePaths);
        }

        [Fact]
        public void FindSequenceForRobots_1_029A()
        {
            string passCode = "029A";
            List<string> paths = Day21Solver.FindSequenceForRobots(passCode, 1, Day21Solver.DButtons, allDirPaths, Day21Solver.NumButtons, allNumPaths);

            paths.Contains("<A^A>^^AvvvA").Should().BeTrue();
        }

        [Fact]
        public void FindSequenceForRobots_2_029A()
        {
            string passCode = "029A";
            List<string> paths = Day21Solver.FindSequenceForRobots(passCode, 2, Day21Solver.DButtons, allDirPaths, Day21Solver.NumButtons, allNumPaths);

            paths.Contains("v<<A>>^A<A>AvA<^AA>A<vAAA>^A").Should().BeTrue();
        }

        [Fact]
        public void FindSequenceForRobots_3_029A()
        {
            string passCode = "029A";
            List<string> paths = Day21Solver.FindSequenceForRobots(passCode, 3, Day21Solver.DButtons, allDirPaths, Day21Solver.NumButtons, allNumPaths);

            paths.Contains("v<<A>>^A<A>AvA<^AA>A<vAAA>^A").Should().BeFalse();
            paths.Contains("<vA<AA>>^AvAA<^A>A<v<A>>^AvA^A<vA>^A<v<A>^A>AAvA^A<v<A>A>^AAAvA<^A>A").Should().BeTrue();
        }

        [Theory]
        [InlineData("029A", 68*29)]
        [InlineData("980A", 60*980)]
        [InlineData("179A", 68*179)]
        [InlineData("456A", 64*456)]
        [InlineData("379A", 64*379)]
        public void FindComplexity(string code, long expectedComplexity)
        {
            long complexity = Day21Solver.FindComplexity(code, Day21Solver.DButtons, allDirPaths, Day21Solver.NumButtons, allNumPaths);

            complexity.Should().Be(expectedComplexity);
        }
    }
}
