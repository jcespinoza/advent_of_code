using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day09Solver : Solver<int[], long>
    {
        public Day09Solver() : base(2024, 09) { }

        public override int[] ParseInput(IEnumerable<string> input)
            => input.First().Select(c => c - '0').ToArray();


        public override long PartOne(int[] diskMap)
        {
            List<DiskBlock> blocks = BuildBlocks(diskMap);
            int[] sortedDisk = SortBlocks(blocks);
            long checksum = GetFileSytemChecksum(sortedDisk);

            return checksum;
        }

        private List<DiskBlock> BuildBlocks(int[] diskMap)
        {
            throw new NotImplementedException();
        }

        private int[] SortBlocks(List<DiskBlock> blocks)
        {
            throw new NotImplementedException();
        }

        private long GetFileSytemChecksum(int[] sortedDisk)
        {
            long totalSum = 0;
            for (int index = 0; index < sortedDisk.Length; index++)
            {
                long localSum = index * sortedDisk[index];
                totalSum += localSum;
            }
            return totalSum;
        }

        public override long PartTwo(int[] input)
        {
            throw new NotImplementedException();
        }
    }

    public record DiskBlock(int Size, int Index);
}