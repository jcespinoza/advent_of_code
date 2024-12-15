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

        [Fact]
        public void CountStones_Test1()
        {
            long[] stones = [0, 1, 10, 99, 999];
            long newStonesCount = PlutonianPebbles.CountStones(stones, 0);
            Assert.Equal(5, newStonesCount);
        }

        [Fact]
        public void CountStones_Test2()
        {
            long[] stones = [0, 1, 10, 99, 999];
            long newStonesCount = PlutonianPebbles.CountStones(stones, 1);
            Assert.Equal(7, newStonesCount);
        }

        [Fact]
        public void CountStones_Test_1blink()
        {
            long[] stones = [125, 17];
            long newStonesCount = PlutonianPebbles.CountStones(stones, 1);
            Assert.Equal(3, newStonesCount);
        }        

        [Fact]
        public void CountStones_Test_2blinks()
        {
            long[] stones = [125, 17];
            long newStonesCount = PlutonianPebbles.CountStones(stones, 2);
            Assert.Equal(4, newStonesCount);
        }

        [Fact]
        public void CountStones_Test_3blinks()
        {
            long[] stones = [125, 17];
            long newStonesCount = PlutonianPebbles.CountStones(stones, 3);
            Assert.Equal(5, newStonesCount);
        }

        [Fact]
        public void CountStones_Test_4blinks()
        {
            long[] stones = [125, 17];
            long newStonesCount = PlutonianPebbles.CountStones(stones, 4);
            Assert.Equal(9, newStonesCount);
        }
        

        [Fact]
        public void CountStones_Test_5blinks()
        {
            long[] stones = [125, 17];
            long newStonesCount = PlutonianPebbles.CountStones(stones, 5);
            Assert.Equal(13, newStonesCount);
        }

        [Fact]
        public void CountStones_Test_6blinks()
        {
            long[] stones = [125, 17];
            long newStonesCount = PlutonianPebbles.CountStones(stones, 6);
            Assert.Equal(22, newStonesCount);
        }

        [Fact]
        public void CountStones_Test_25blinks()
        {
            long[] stones = [125, 17];
            long newStonesCount = PlutonianPebbles.CountStones(stones, 25);
            Assert.Equal(55312, newStonesCount);
        }
    }
}
