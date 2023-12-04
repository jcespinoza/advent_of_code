
namespace Advent23.Days
{
    public record GameInfo
    {
        public required int GameID { get; init; }
        public required CubeSet[] Sets { get; init; }

        public static GameInfo FromLine(string line)
        {
            var parts = line.Split(": ");
            var gameId = parts[0].Split(" ")[1];
            string[] setStrs = parts[1].Split(";");
            var cubeSets = new CubeSet[setStrs.Length];
            for (int index = 0; index < setStrs.Length; index++)
            {
                cubeSets[index] = ParseColorSet(setStrs[index]);
            }

            var gameInfo = new GameInfo
            {
                GameID = int.Parse(gameId),
                Sets = cubeSets
            };
            return gameInfo;
        }

        private static CubeSet ParseColorSet(string setStr)
        {
            var colorCounts = setStr.Trim().Split(",");
            int greenCount = 0, blueCount = 0, redCount = 0;
            foreach (var colorCount in colorCounts)
            {
                string[] countColorPair = colorCount.Trim().Split(" ");
                int count = int.Parse(countColorPair[0]);
                var colorName = countColorPair[1];
                if (colorName == "green") greenCount = count;
                if (colorName == "blue") blueCount = count;
                if (colorName == "red") redCount = count;
            }
            var cubeSet = new CubeSet { Blue = blueCount, Red = redCount, Green = greenCount };
            return cubeSet;
        }
    }
}