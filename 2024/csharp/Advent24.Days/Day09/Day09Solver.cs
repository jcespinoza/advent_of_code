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

        public override long PartTwo(int[] diskMap)
        {
            FileSystem fileSystem = BuildFileSystem(diskMap);

            CompactFileSystem(fileSystem);

            long checksum = GetFileSytemChecksum(fileSystem);

            return checksum;
        }

        private static void CompactFileSystem(FileSystem fileSystem)
        {
            Dictionary<int, DiskBlock> files = fileSystem.Files;
            int fileId = files.Count;
            while (fileId > 0)
            {
                fileId--;
                DiskBlock file = files[fileId];
                List<DiskBlock> blanks = fileSystem.BlankSpace;
                for (int index = 0; index < blanks.Count; index++)
                {
                    var blank = blanks[index];
                    if (blank.Position >= file.Position)
                    {
                        int countToTake = blanks.Count - index - 1;
                        blanks = blanks
                                                    .Take(countToTake)
                                                    .ToList();
                        break;
                    }
                    if (file.Size <= blank.Size)
                    {
                        files[fileId] = new(blank.Position, file.Size);
                        if (file.Size == blank.Size)
                        {
                            blanks.RemoveAt(index);
                        }
                        else
                        {
                            blanks[index] = new(blank.Position + file.Size, blank.Size - file.Size);
                        }
                        break;
                    }
                }
            }
        }

        private long GetFileSytemChecksum(FileSystem fileSystem)
        {
            long totalSum = 0;
            foreach (var entry in fileSystem.Files)
            {
                DiskBlock file = entry.Value;
                foreach (var index in Enumerable.Range(file.Position, file.Size))
                {
                    totalSum += entry.Key * index;
                }
            }

            return totalSum;
        }

        private FileSystem BuildFileSystem(int[] diskMap)
        {
            Dictionary<int, DiskBlock> files = [];
            List<DiskBlock> blankSpace = [];
            int fileId = 0;
            int position = 0;

            for (int index = 0; index < diskMap.Length; index++)
            {
                int size = diskMap[index];
                if (index % 2 == 0)
                {
                    if (size == 0) throw new Exception("Size can not be zero");
                    files.Add(fileId, new(position, size));
                    fileId++;
                }
                else
                {
                    if (size > 0)
                    {
                        blankSpace.Add(new(position, size));
                    }
                }
                position += size;
            }

            return new FileSystem
            {
                Disk = diskMap,
                Files = files,
                BlankSpace = blankSpace
            };
        }
    }

    public record DiskBlock(int Position, int Size)
    {
        public override string ToString()
        {
            return $"({Position} {Size})";
        }
    };
    public class FileSystem{
        public int[] Disk { get; set; }
        public Dictionary<int, DiskBlock> Files { get; set; }
        public List<DiskBlock> BlankSpace { get; set; }
    }
}