using Advent23.Days.Day07;
using AdventOfCode.Commons;
using dotenv.net;

namespace Advent23.Days
{
    public class Day07Test : TestEngine<Day07Solver, BidHand[], long>
    {
        private const long EXPECTED_SOLUTION_PART_1 = 253_933_213;
        private const long EXPECTED_SOLUTION_PART_2 = 123;

        public Day07Test()
        {
            DotEnv.Load();
        }

        public override Puzzle PartOne => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "32T3K 765",
                    "T55J5 684",
                    "KK677 28",
                    "KTJJT 220",
                    "QQQJA 483",
                ],
                Input = [
                    new()
                    {
                        Hand = "MLVMY",
                        BidAmt = 765,
                        HandType = HandType.OnePair
                    },
                    new()
                    {
                        Hand = "VOOWO",
                        BidAmt = 684,
                        HandType = HandType.ThreeOfKind
                    },
                    new()
                    {
                        Hand = "YYPRR",
                        BidAmt = 28,
                        HandType = HandType.TwoPair
                    },
                    new()
                    {
                        Hand = "YVWWV",
                        BidAmt = 220,
                        HandType = HandType.TwoPair
                    },
                    new()
                    {
                        Hand = "XXXWZ",
                        BidAmt = 483,
                        HandType = HandType.ThreeOfKind
                    },
                ],
                Result = 6440,
            },
            Examples = [
                new()
                {
                    RawInput = [
                        "AAAAA 1",
                        "AA8AA 1",
                        "23332 1",
                        "TTT98 1",
                        "23432 1",
                        "A23A4 1",
                        "23456 1",
                    ],
                    Input = [
                        new()
                        {
                            Hand = "ZZZZZ",
                            BidAmt = 1,
                            HandType = HandType.FiveOfKind,
                        },
                        new()
                        {
                            Hand = "ZZSZZ",
                            BidAmt = 1,
                            HandType = HandType.FourOfKind,
                        },
                        new()
                        {
                            Hand = "LMMML",
                            BidAmt = 1,
                            HandType = HandType.FullHouse,
                        },
                        new()
                        {
                            Hand = "VVVUS",
                            BidAmt = 1,
                            HandType = HandType.ThreeOfKind,
                        },
                        new()
                        {
                            Hand = "LMNML",
                            BidAmt = 1,
                            HandType = HandType.TwoPair,
                        },
                        new()
                        {
                            Hand = "ZLMZN",
                            BidAmt = 1,
                            HandType = HandType.OnePair,
                        },
                        new()
                        {
                            Hand = "LMNOP",
                            BidAmt = 1,
                            HandType = HandType.HighCard,
                        },
                    ],
                    Result= 28,
                },
            ],
            Solution = EXPECTED_SOLUTION_PART_1,
        };

        public override Puzzle PartTwo => new()
        {
            ShouldSkipTests = false,
            Example = new()
            {
                RawInput = [
                    "32T3K 765",
                    "T55J5 684",
                    "KK677 28",
                    "KTJJT 220",
                    "QQQJA 483",
                ],
                Result = 5905,
            },
            Solution = EXPECTED_SOLUTION_PART_2,
        };
    }
}