namespace Advent24.Days
{
    public static class PrintingOrderChecker
    {
        public static IEnumerable<int[]> FindCorrectUpdates(List<int[]> updates, List<OrderingRule> rules)
        {
            List<int[]> correctUpdates = [];
            foreach (var update in updates)
            {
                bool isCorrect = DoesUpdateFollowRules(update, rules);
                if (isCorrect)
                {
                   yield return update;
                }
            }
        }
        
        public static IEnumerable<int[]> FindIncorrectUpdates(List<int[]> updates, List<OrderingRule> rules)
        {
            List<int[]> correctUpdates = [];
            foreach (var update in updates)
            {
                bool isCorrect = DoesUpdateFollowRules(update, rules);
                if (!isCorrect)
                {
                   yield return update;
                }
            }
        }

        public static bool DoesUpdateFollowRules(int[] update, List<OrderingRule> rules)
        {
            foreach (var rule in rules)
            {
                bool passesTest = DoesUpdateFollowRule(update, rule);
                if(!passesTest)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool DoesUpdateFollowRule(int[] update, OrderingRule rule)
        {
            int indexX = Array.IndexOf(update, rule.PageX);
            int indexY = Array.IndexOf(update, rule.PageY);

            // if either index is -1, the rule does not apply
            if (indexX == -1 || indexY == -1)
            {
                return true;
            }

            return indexX < indexY;
        }
    }
}