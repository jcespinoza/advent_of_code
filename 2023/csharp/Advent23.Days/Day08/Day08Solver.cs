using Advent23.Days.Day08;
using AdventOfCode.Commons;
using System.Reflection.Metadata;

namespace Advent23.Days
{
    public class Day08Solver : Solver<DesertMap, long>
    {


        public Day08Solver() : base(2023, 08) { }

        public override DesertMap ParseInput(IEnumerable<string> input)
            => DesertMap.Parse(input);

        public override long PartOne(DesertMap desertMap)
        {   const string START_KEY = "AAA";
            const string TARGET_KEY = "ZZZ";
            int steps = 0;
            var currentNode = desertMap.Nodes[START_KEY];
            
            for (int index = 0; index < desertMap.Directions.Length; index++)
            {
                var direction = desertMap.Directions[index];

                if(currentNode.Name == TARGET_KEY)
                {
                    break;
                }

                if(direction == Direction.Left)
                {
                    currentNode = desertMap.Nodes[currentNode.LeftName];
                }else
                {
                    currentNode = desertMap.Nodes[currentNode.RightName];
                }

                if(index == desertMap.Directions.Length - 1)
                {
                    index = -1;
                }
                steps++;
            }

            return steps;
        }

        public override long PartTwo(DesertMap desertMap)
        {
            var currentNodes = desertMap.Nodes.Where(n => n.Key.EndsWith('A')).Select(n => n.Value).ToArray();

            // There are cycles in the paths
            // Find the minimum steps required per node indvidually first
            var steps = currentNodes.Select(node => StepsUntilTarget(desertMap, node)).ToArray();

            // Find the lest common multiple for the minimum number of steps for all nodes
            var aggregatedSteps = steps.LeastCommonMultiple();

            return aggregatedSteps;
        }

        private long StepsUntilTarget(DesertMap desertMap, DesertNode startNode)
        {
            long steps = 0;
            var dirIndex = 0;
            var currentNode = startNode;

            while (!currentNode.Name.EndsWith('Z'))
            {
                var direction = desertMap.Directions[dirIndex];

                if (direction == Direction.Left)
                {
                    currentNode = desertMap.Nodes[currentNode.LeftName];
                }
                else
                {
                    currentNode = desertMap.Nodes[currentNode.RightName];
                }

                if (dirIndex == desertMap.Directions.Length - 1)
                {
                    dirIndex = 0;
                }
                else
                {
                    dirIndex++;
                }

                steps++;
            }

            return steps;
        }
    }
}