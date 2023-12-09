


namespace Advent23.Days.Day07
{
    public record BidHand
    {
        private static readonly int HAND_TYPES = 5;
        public static readonly Dictionary<char, char> CARD_NAMES = new() {
            { 'A', 'Z' },
            { 'K', 'Y' },
            { 'Q', 'X' },
            { 'J', 'W' },
            { 'T', 'V' },
            { '9', 'U' },
            { '8', 'S' },
            { '7', 'R' },
            { '6', 'P' },
            { '5', 'O' },
            { '4', 'N' },
            { '3', 'M' },
            { '2', 'L' },
        };

        public required string Hand { get; set; }
        public required int BidAmt { get; init; }
        public required HandType HandType { get; set; }
        public int IntHandType => (int)HandType;

        public static BidHand Parse(string line)
        {
            var parts = line.Split(' ',StringSplitOptions.RemoveEmptyEntries);

            string handStr = RenameCards(parts[0].Trim());
            int bidAmount = int.Parse(parts[1].Trim());
            HandType type = ComputeHandType(handStr);
            return new()
            {
                Hand = handStr,
                BidAmt = bidAmount,
                HandType = type
            };
        }

        public static HandType ComputeHandType(string handStr)
        {
            var groups = handStr.GroupBy(s => s);
            
            var distinctLabels = handStr.Distinct().Count();

            if (distinctLabels == 1) return HandType.FiveOfKind;
            if (distinctLabels == 5) return HandType.HighCard;

            if(groups.Any(g => g.Count() == 3) && groups.Any(g => g.Count() == 2)){
                return HandType.FullHouse;
            }
            if(groups.Any(g => g.Count() == 3))
            {
                return HandType.ThreeOfKind;
            }
            if(groups.Any(g => g.Count() == 4))
            {
                return HandType.FourOfKind;
            }
            if(groups.Count(g => g.Count() == 2) == 2)
            {
                return HandType.TwoPair;
            }

            return HandType.OnePair;
        }

        private static string RenameCards(string line)
        {
            for (int index = 0; index < line.Length; index++)
            {
                if (CARD_NAMES.TryGetValue(line[index], out char value))
                {
                    line = line.Replace(line[index], value);
                }
            }
            return line;
        }


        /*
        AAAAJ
        AAAJJ
        AAAJQ
        AAJQQ
        AA
         */

        public static string ComputeBestHand(string handStr)
        {
            char jokenSymbol = CARD_NAMES['J'];
            if (!handStr.Contains(jokenSymbol)) return handStr;

            var nonJokerLabels = handStr.Where(c => c != jokenSymbol).ToList();
            if (!nonJokerLabels.Any())
            {
                return handStr.Replace(jokenSymbol, CARD_NAMES['2']);
            }

            var groups = nonJokerLabels.GroupBy(s => s).OrderByDescending(s => s.Count()).ThenByDescending(s => s.Key);

            var firstGroup = groups.First().ToList();
            if (firstGroup.Count() > 1)
            {
                var newHandStr = handStr.Replace(jokenSymbol, firstGroup.First());
                return newHandStr;
            }
            return handStr;
        }
    }
}