
namespace Advent23.Days.Day09
{
    public record HistoryLine
    {
        public required long[] PastRecords { get; init; }
        public long Prediction { get; set; }
        public long BackPrediction { get; set; }

        public long[] GetDifferences()
        {
            long[] differences = new long[PastRecords.Length - 1];
            for (int index = 0; index < PastRecords.Length; index++)
            {
                if (index + 1 >= PastRecords.Length) break;
                differences[index] = PastRecords[index + 1] - PastRecords[index];
            }
            return differences;
        }

        public static HistoryLine Parse(string line)
        {
            var historicRecords = line
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            var historyLine = new HistoryLine
            {
                PastRecords = historicRecords
            };

            return historyLine;
        }

        public long ExtrapolateFuture()
        {
            long[] currentDifferences = GetDifferences();
            if (currentDifferences.All(d => d == 0))
            {
                return (Prediction = PastRecords.Last());
            }

            List<HistoryLine> intermediaryLines = [];
            int levels = 0;
            while (true) {
                levels++;
                var newLine = new HistoryLine { PastRecords = currentDifferences };
                intermediaryLines.Add(newLine);

                currentDifferences = newLine.GetDifferences();
                if (currentDifferences.All(d => d == 0))
                {
                    newLine.Prediction = newLine.PastRecords.Last();
                    break;
                }
            }
            if (levels == 1)
            {
                return (Prediction = PastRecords.Last() + intermediaryLines.First().Prediction);
            }

            for (int index = levels-1; index >= 0; index--)
            {
                if (index - 1 < 0) break;
                var aboveLine = intermediaryLines[index - 1];
                var lowLine = intermediaryLines[index];
                aboveLine.Prediction = aboveLine.PastRecords.Last() + lowLine.Prediction;
            }

            return (Prediction = PastRecords.Last() + intermediaryLines.First().Prediction);
        }

        public long ExtrapolatePast()
        {
            long[] currentDifferences = GetDifferences();
            if (currentDifferences.All(d => d == 0))
            {
                return (BackPrediction = PastRecords.First());
            }

            List<HistoryLine> intermediaryLines = [];
            int levels = 0;
            while (true) {
                levels++;
                var newLine = new HistoryLine { PastRecords = currentDifferences };
                intermediaryLines.Add(newLine);

                currentDifferences = newLine.GetDifferences();
                if (currentDifferences.All(d => d == 0))
                {
                    newLine.BackPrediction = newLine.PastRecords.First();
                    break;
                }
            }
            if (levels == 1)
            {
                return BackPrediction = PastRecords.First() - intermediaryLines.First().BackPrediction;
            }

            for (int index = levels-1; index >= 0; index--)
            {
                if (index - 1 < 0) break;
                var aboveLine = intermediaryLines[index - 1];
                var lowLine = intermediaryLines[index];
                aboveLine.BackPrediction = aboveLine.PastRecords.First() - lowLine.BackPrediction;
            }

            return BackPrediction = PastRecords.First() - intermediaryLines.First().BackPrediction;
        }
    }
}