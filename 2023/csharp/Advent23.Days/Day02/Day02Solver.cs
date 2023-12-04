using AdventOfCode.Commons;

namespace Advent23.Days
{
    public class Day02Solver : Solver<GameInfo[], long>
    {
        private CubeSet MaxSet = new()
        {
            Red = 12,
            Green = 13,
            Blue = 14,
        };

        public Day02Solver() : base(2023, 02) { }

        public override GameInfo[] ParseInput(IEnumerable<string> input)
            => input.Select(GameInfo.FromLine).ToArray();


        public override long PartOne(GameInfo[] input)
        {
            var possibleGames = 0;
            foreach (var game in input)
            {
                if (IsGamePossible(game, MaxSet))
                {
                    possibleGames += game.GameID;
                }
            }

            return possibleGames;
        }

        public override long PartTwo(GameInfo[] input)
        {
            return input.Select(game => GetMinimumPower(game)).Sum();
        }

        private long GetMinimumPower(GameInfo game)
        {
            CubeSet minimumSet = GetMinimumSet(game);
            return minimumSet.Red * minimumSet.Blue * minimumSet.Green;
        }

        private CubeSet GetMinimumSet(GameInfo game)
        {
            var maxRed = game.Sets.Max(s => s.Red);
            var maxGreen = game.Sets.Max(s => s.Green);
            var maxBlue = game.Sets.Max(s => s.Blue);
            var minimumSet = new CubeSet {
                Red = maxRed <= 0 ? 1 : maxRed,
                Green = maxGreen<= 0 ? 1: maxGreen,
                Blue = maxBlue <= 0 ? 1: maxBlue,
            };
            return minimumSet;
        }

        private bool IsGamePossible(GameInfo game, CubeSet maxSet)
        {
            foreach (var set in game.Sets)
            {
                if(!IsSetPossible(set, maxSet))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsSetPossible(CubeSet set, CubeSet maxSet)
        {
            return set.Red <= maxSet.Red
                && set.Green <= maxSet.Green
                && set.Blue <= maxSet.Blue;
        }
    }
}