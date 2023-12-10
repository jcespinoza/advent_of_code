using Advent23.Days.Day08;
using AdventOfCode.Commons;

namespace Advent23.Days
{
    public class Day08Solver : Solver<DesertMap, long>
    {
        private const string START_KEY = "AAA";
        private const string TARGET_KEY = "ZZZ";

        public Day08Solver() : base(2023, 08) { }

        public override DesertMap ParseInput(IEnumerable<string> input)
            => DesertMap.Parse(input);

        public override long PartOne(DesertMap dessertMap)
        {
            int steps = 0;
            var currentNode = dessertMap.Nodes[START_KEY];
            
            for (int index = 0; index < dessertMap.Directions.Length; index++)
            {
                var direction = dessertMap.Directions[index];

                if(currentNode.Name == TARGET_KEY)
                {
                    break;
                }

                if(direction == Direction.Left)
                {
                    currentNode = dessertMap.Nodes[currentNode.LeftName];
                }else
                {
                    currentNode = dessertMap.Nodes[currentNode.RightName];
                }

                if(index == dessertMap.Directions.Length - 1)
                {
                    index = -1;
                }
                steps++;
            }

            return steps;
        }

        public override long PartTwo(DesertMap input)
        {
            throw new NotImplementedException();
        }
    }
}