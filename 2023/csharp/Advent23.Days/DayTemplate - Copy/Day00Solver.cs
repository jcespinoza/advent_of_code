using Advent23.Days.Day15;
using AdventOfCode.Commons;
using Xunit.Sdk;

namespace Advent23.Days
{
    public class Day15Solver : Solver<string, long>
    {
        public Day15Solver() : base(2023, 15) { }

        public override string ParseInput(IEnumerable<string> input)
            => input.First();

        public override long PartOne(string input)
        {
            var stepStrs = input.Trim().Split(',', StringSplitOptions.TrimEntries);

            var totalStepSum = 0;
            for (int stepIdx = 0; stepIdx < stepStrs.Length; stepIdx++)
            {
                var newHash = HashSequence(stepStrs[stepIdx]);
                totalStepSum += newHash;
            }

            return totalStepSum;
        }

        private int HashSequence(string sequence)
        {
            int currentHash = 0;
            for (int index = 0; index < sequence.Length; index++)
            {
                var cChar = sequence[index];
                currentHash += cChar;
                currentHash *= 17;
                currentHash %= 256;
            }

            return currentHash;
        }

        public override long PartTwo(string input)
        {
            Dictionary<int, List<Lens>> boxes = [];
            var stepStrs = input.Trim().Split(',', StringSplitOptions.TrimEntries);

            foreach (string step in stepStrs)
            {
                ProcessStep(step, boxes);
            }
            
            var totalFocusingPower = 0;
            foreach (var box in boxes.Where(b => b.Value.Count > 0))
            {
                var boxNumber = box.Key + 1;
                var slotNumber = 1;
                foreach (var lens in box.Value)
                {
                    var focusingPower = boxNumber * slotNumber * lens.FocalLength;
                    totalFocusingPower += focusingPower;

                    slotNumber++;
                }
            }

            return totalFocusingPower;
        }

        private void ProcessStep(string stepStr, Dictionary<int, List<Lens>> boxes)
        {
            var type = char.IsDigit(stepStr.Last()) ? StepType.Replace : StepType.Remove;
            var label = type == StepType.Remove
                ? stepStr[..^1]
                : stepStr[..^2];

            var targetBox = HashSequence(label);

            if (type == StepType.Remove && boxes.ContainsKey(targetBox)) {
                var lensIndex = boxes[targetBox].FindIndex(l => l.Label == label);

                if (lensIndex == -1) return;

                boxes[targetBox].RemoveAt(lensIndex);
                return;
            }

            // Scenario: it's a Remove but there's no box with this number
            if (type == StepType.Remove) return;

            if (!boxes.ContainsKey(targetBox))
            {
                boxes[targetBox] = [
                    new Lens { 
                        Label = label, 
                        FocalLength = stepStr.Last()  - '0'
                    }
                ];
                return;
            }

            var lens = boxes[targetBox].FirstOrDefault(targetBox => targetBox.Label == label);
            if (lens != null)
            {
                lens.FocalLength = stepStr.Last() - '0';
            }
            else
            {
                boxes[targetBox].Add(
                    new Lens
                    {
                        Label = label,
                        FocalLength = stepStr.Last() - '0'
                    });
            }

            return;
        }
    }
}