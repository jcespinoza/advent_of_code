using Advent23.Days.Day08;
using AdventOfCode.Commons;

namespace Advent23.Days
{
    public class Day08Solver : Solver<DesertMap, long>
    {
        public Day08Solver() : base(2023, 08) { }

        public override DesertMap ParseInput(IEnumerable<string> input)
            => DesertMap.Parse(input);

        public override long PartOne(DesertMap dessertMap)
        {
            int steps = 0;
            var currentNode = dessertMap.Nodes["AAA"];
            
            for (int index = 0; index < dessertMap.Directions.Length; index++)
            {
                var direction = dessertMap.Directions[index];

                if(currentNode.Name == "ZZZ")
                {
                    break;
                }

                if(direction == 'L')
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