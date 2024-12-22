using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Advent24.Days.Day17
{
    public class ComputerTests
    {
        [Fact]
        public void ProgramExample_01()
        {
            var computer = new Computer
            {
                RegC = 9,
                Program = [2, 6],
                Ready = true,
            };
            var strBuilder = new StringBuilder();
            Computer.SimulateProgram(computer, ref strBuilder);

            computer.RegB.Should().Be(1);
        }

        [Fact]
        public void ProgramExample_02()
        {
            var computer = new Computer
            {
                RegA = 10,
                Program = [5, 0, 5, 1, 5, 4],
                Ready = true,
            };
            var strBuilder = new StringBuilder();
            Computer.SimulateProgram(computer, ref strBuilder);
            var output = strBuilder.ToString();

            output.Should().Be("0,1,2");
        }

        [Fact]
        public void ProgramExample_03()
        {
            var computer = new Computer
            {
                RegA = 2024,
                Program = [0, 1, 5, 4, 3, 0],
                Ready = true,
            };
            var strBuilder = new StringBuilder();
            Computer.SimulateProgram(computer, ref strBuilder);
            var output = strBuilder.ToString();

            output.Should().Be("4,2,5,6,7,7,7,7,3,1,0");
            computer.RegA.Should().Be(0);
        }

        [Fact]
        public void ProgramExample_04()
        {
            var computer = new Computer
            {
                RegB = 29,
                Program = [1, 7],
                Ready = true,
            };
            var strBuilder = new StringBuilder();
            Computer.SimulateProgram(computer, ref strBuilder);

            computer.RegB.Should().Be(26);
        }

        [Fact]
        public void ProgramExample_05()
        {
            var computer = new Computer
            {
                RegB = 2024,
                RegC = 43690,
                Program = [4, 0],
                Ready = true,
            };
            var strBuilder = new StringBuilder();
            Computer.SimulateProgram(computer, ref strBuilder);
            
            computer.RegB.Should().Be(44354);
        }
    }
}
