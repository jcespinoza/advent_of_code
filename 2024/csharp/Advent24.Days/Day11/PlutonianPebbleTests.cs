using Xunit;

namespace Advent24.Days.Day11
{
    public class PlutonianPebbleTests
    {
        [Fact]
        public void CalculateState_Test1()
        {
            long[] stones = [0, 1, 10, 99, 999];
            List<long> newStones = PlutonianPebbles.CalculateState(stones, 1);
            Assert.Equal(7, newStones.Count);
        }

        [Fact]
        public void CalculateState_Test2()
        {
            long[] stones = [125, 17];
            List<long> newStones = PlutonianPebbles.CalculateState(stones, 6);
            Assert.Equal(22, newStones.Count);
        }

        [Fact]
        public void CalculateState_Test3()
        {
            long[] stones = [125, 17];
            List<long> newStones = PlutonianPebbles.CalculateState(stones, 25);
            Assert.Equal(55312, newStones.Count);
        }
    }
}
