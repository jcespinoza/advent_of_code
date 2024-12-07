using FluentAssertions;
using Xunit;

namespace Advent24.Days
{
    public class PrintingOrderCheckerTests
    {
        private readonly List<OrderingRule> AllRules = [
            new(47,53),
            new(97,13),
            new(97,61),
            new(97,47),
            new(75,29),
            new(61,13),
            new(75,53),
            new(29,13),
            new(97,29),
            new(53,29),
            new(61,53),
            new(97,53),
            new(61,29),
            new(47,13),
            new(75,47),
            new(97,75),
            new(47,61),
            new(75,61),
            new(47,29),
            new(75,13),
            new(53,13),
        ];

        [Fact]
        public void FindApplicableRules_ReturnsCorrectRules()
        {
            // Arrange
            int[] update = [75, 97, 47, 61, 53];
            List<OrderingRule> expectedRules = [
                new(47,53),
                new(97,61),
                new(97,47),
                new(75,53),
                new(61,53),
                new(97,53),
                new(75,47),
                new(97,75),
                new(47,61),
                new(75,61),
            ];
            // Act
            var applicableRules = PrintingOrderChecker.FilterApplicableRules(update, AllRules);
            // Assert
            applicableRules.Should().ContainInOrder(expectedRules);
        }

        [Fact]
        public void ApplyOrderingRule_ReturnsOriginalUpdateIfOrderIsAlreadyCorrect()
        {
            // Arrange
            int[] update = [75, 97, 47, 61, 53];
            OrderingRule rule = new(47, 53);
            int[] expectedOrder = [75, 97, 47, 61, 53];
            // Act
            PrintingOrderChecker.ApplyOrderingRule(update, rule);
            // Assert
            update.Should().ContainInOrder(expectedOrder);
        }

        [Fact]
        public void CorrectUpdate_ReturnsCorrectUpdate_1()
        {
            // Arrange
            int[] incorrectUpdate = [75, 97, 47, 61, 53];
            int[] expectedCorrection = [97, 75, 47, 61, 53];
            List<OrderingRule> rules = [
                new(47,53),
                new(97,61),
                new(97,47),
                new(75,53),
                new(61,53),
                new(97,53),
                new(75,47),
                new(97,75),
                new(47,61),
                new(75,61),
            ];

            // Act
            var correctedUpdate = PrintingOrderChecker.CorrectUpdate(incorrectUpdate, rules);

            // Assert
            correctedUpdate.Should().ContainInOrder(expectedCorrection);
        }
        
        [Fact]
        public void CorrectUpdate_ReturnsCorrectUpdate_2()
        {
            // Arrange
            int[] incorrectUpdate = [97, 13, 75, 29, 47];
            int[] expectedCorrection = [97, 75, 47, 29, 13];
            List<OrderingRule> rules = [
                new(97,13),
                new(97,47),
                new(75,29),
                new(29,13),
                new(97,29),
                new(47,13),
                new(75,47),
                new(97,75),
                new(47,29),
                new(75,13),
            ];

            // Act
            var correctedUpdate = PrintingOrderChecker.CorrectUpdate(incorrectUpdate, rules);

            // Assert
            correctedUpdate.Should().ContainInOrder(expectedCorrection);
        }

        [Fact]
        public void CorrectUpdates_ReturnsCorrectUpdates()
        {
            // Arrange
            List<int[]> incorrectUpdates = [
                [75,97,47,61,53],
                [61,13,29],
                [97,13,75,29,47],
            ];
            List<int[]> expectedCorrections = [
                [97,75,47,61,53],
                [61,29,13],
                [97,75,47,29,13],
            ];
            // Act
            var correctedUpdates = PrintingOrderChecker.CorrectUpdates(incorrectUpdates, AllRules).ToList();

            // Assert
            for (int index = 0; index < correctedUpdates.Count; index++)
            {
                int[] corrected = correctedUpdates[index];
                int[] expected = expectedCorrections[index];
                corrected.Should().ContainInOrder(expected);
            }
        }

        [Fact]
        public void CorrectUpdates_ReturnsCorrectUpdates_2()
        {
            // Arrange
            List<int[]> incorrectUpdates = [
                [97,13,75,29,47],
            ];
            List<int[]> expectedCorrections = [
                [97,75,47,29,13],
            ];
            // Act
            var correctedUpdates = PrintingOrderChecker.CorrectUpdates(incorrectUpdates, AllRules).ToList();
            
            // Assert
            for (int index = 0; index < correctedUpdates.Count; index++)
            {
                int[] corrected = correctedUpdates[index];
                int[] expected = expectedCorrections[index];
                corrected.Should().ContainInOrder(expected);
            }
        }
    }
}