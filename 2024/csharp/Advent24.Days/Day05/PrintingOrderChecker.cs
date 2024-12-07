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

        public static IEnumerable<int[]> CorrectUpdates(List<int[]> incorrectUpdates, List<OrderingRule> rules)
        {
            foreach (var update in incorrectUpdates)
            {
                List<OrderingRule> applicableRules = FilterApplicableRules(update, rules);
                int[] correctedUpdate = CorrectUpdate(update, applicableRules);
                yield return correctedUpdate;
            }
        }

        public static List<OrderingRule> FilterApplicableRules(int[] update, List<OrderingRule> rules)
        {
            List<OrderingRule> applicableRules = [];
            foreach (var rule in rules)
            {
                if (update.Contains(rule.PageX) && update.Contains(rule.PageY))
                {
                    applicableRules.Add(rule);
                }
            }
            return applicableRules;
        }

        public static int[] CorrectUpdate(int[] update, List<OrderingRule> applicableRules)
        {
            // copy the update to avoid modifying the original
            int[] correctedUpdate = new int[update.Length];
            update.CopyTo(correctedUpdate, 0);

            foreach (var rule in applicableRules)
            {
                ApplyOrderingRule(correctedUpdate, rule);
            }

            return correctedUpdate;
        }

        public static void ApplyOrderingRule(int[] correctedUpdate, OrderingRule rule)
        {
            int indexX = Array.IndexOf(correctedUpdate, rule.PageX);
            int indexY = Array.IndexOf(correctedUpdate, rule.PageY);

            if (indexX < indexY)
            {
                return;
            }

            if (indexX > indexY)
            {
                SwapElements(correctedUpdate, indexX, indexY);
            }
            else
            {
                // if the elements are already in the correct order, move the element at indexY to the end
                SwapElements(correctedUpdate, indexY, correctedUpdate.Length - 1);
            }
        }

        private static void SwapElements(int[] correctedUpdate, int indexX, int indexY)
        {
            int temp = correctedUpdate[indexX];
            correctedUpdate[indexX] = correctedUpdate[indexY];
            correctedUpdate[indexY] = temp;
        }
    }
}