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
            List<int> blocks = BuildBlocks(diskMap);

            List<int> blankSpace = blocks
                                    .Select((element,index) => new { index, element })
                                    .Where(x => x.element == -1)
                                    .Select(x => x.index)
                                    .ToList();

            int[] sortedDisk = SortBlocks(blocks, blankSpace);
            var sortedStr = DataPrinter.Print(sortedDisk);
            long checksum = GetFileSytemChecksum(sortedDisk);

            return checksum;
        }

        private List<int> BuildBlocks(int[] diskMap)
        {
            List<int> blocks = [];
            int fileId = 0;
            for (int index = 0; index < diskMap.Length; index++)
            {
                int size = diskMap[index];
                if(index % 2 == 0)
                {
                    // Repeatedly add the file id to the blocks
                    foreach (int i in Enumerable.Range(0, size))
                    {
                        blocks.Add(fileId);
                    }
                    fileId++;
                }
                else
                {
                    // Repeatedly add the negative size to the blocks
                    foreach (int i in Enumerable.Range(0, size))
                    {
                        blocks.Add(-1);
                    }
                }
            }
            return blocks;
        }

        private int[] SortBlocks(List<int> blocks, List<int> blankIndexes)
        {
            var sortedDisk = blocks.ToList();
            foreach (var blankIndex in blankIndexes)
            {
                // Remove all the blank spaces at the end of the disk
                while (sortedDisk.Last() == -1)
                {
                    sortedDisk.RemoveAt(sortedDisk.Count - 1);
                }
                // Blank space is now beyond the end of the disk
                if (blankIndex >= sortedDisk.Count) break;

                // Move the last file to the current blank space
                int lastFile = sortedDisk.Last();
                sortedDisk.RemoveAt(sortedDisk.Count - 1);
                sortedDisk[blankIndex] = lastFile;
            }
            return [.. sortedDisk];
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

    public record DiskBlock(int Size, int Id);
}