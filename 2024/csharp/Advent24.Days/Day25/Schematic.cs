
namespace Advent24.Days
{
    public record Schematic
    {
        public SchematicType Type { get; set; }
        public int[] Pinout { get; set; } = [];

        public static Schematic Parse(List<string> input)
        {
            string firstLine = input.First();
            int[] pinout = new int[firstLine.Length];
            foreach (var line in input)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '#')
                    {
                        pinout[i] += 1;
                    }
                }
            }
            return new Schematic
            {
                Type = firstLine.StartsWith('#') ? SchematicType.Lock : SchematicType.Key,
                Pinout = pinout
            };
        }

        public static bool AreCompatible((int, int, int, int, int) keyPinout, (int, int, int, int, int) sLock, int maxHeight = 7)
        {
            if(keyPinout.Item1 + sLock.Item1 > maxHeight) return false;
            if (keyPinout.Item2 + sLock.Item2 > maxHeight) return false;
            if (keyPinout.Item3 + sLock.Item3 > maxHeight) return false;
            if (keyPinout.Item4 + sLock.Item4 > maxHeight) return false;
            if (keyPinout.Item5 + sLock.Item5 > maxHeight) return false;

            return true;
        }
    }
    public enum SchematicType
    {
        Lock,
        Key,
    }
}