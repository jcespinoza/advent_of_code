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

        public static int[] CorrectUpdate(int[] originalUpdate, List<OrderingRule> applicableRules)
        {
            // build a graph out of the applicable rules
            Dictionary<int, HashSet<int>> rulesDict = ConvertToDictionary(applicableRules);

            var inwardRuleCounts = originalUpdate.ToDictionary(p => p, p => 0);
            var graph = new Dictionary<int, HashSet<int>>();

            foreach (var page in originalUpdate)
            {
                graph[page] = new HashSet<int>();
            }

            foreach (var rule in rulesDict)
            {
                int left = rule.Key;

                foreach (var right in rule.Value)
                {
                    if (!graph[left].Contains(right))
                    {
                        graph[left].Add(right);
                        inwardRuleCounts[right]++;
                    }
                }
            }

            var sortedItems = new List<int>();
            // Initialize the Queue with items that have no incoming edges
            var queue = new Queue<int>(originalUpdate.Where(p => inwardRuleCounts[p] == 0));

            // Process each item in the queue by looking at its outgoing rules
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                sortedItems.Add(current);

                foreach (var neighborPage in graph[current])
                {
                    // Trim the edge from the graph
                    inwardRuleCounts[neighborPage]--;

                    // If the neighbor has no more incoming edges, add it to the queue
                    if (inwardRuleCounts[neighborPage] == 0)
                    {
                        queue.Enqueue(neighborPage);
                    }
                }

            }

            return sortedItems.Count == originalUpdate.Length ? [.. sortedItems] : originalUpdate;
        }

        private static Dictionary<int, HashSet<int>> ConvertToDictionary(List<OrderingRule> applicableRules)
        {
            Dictionary<int, HashSet<int>> result = new();
            foreach (var item in applicableRules)
            {
                if(result.TryGetValue(item.PageX, out var set))
                {
                    set.Add(item.PageY);
                }
                else
                {
                    result[item.PageX] = new HashSet<int> { item.PageY };
                }
            }

            return result;
        }
    }
}