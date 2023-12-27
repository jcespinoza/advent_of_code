using Advent23.Days.Day14;
using AdventOfCode.Commons;

namespace Advent23.Days
{
    public class Day14Solver : Solver<Dish, long>
    {
        public Day14Solver() : base(2023, 14) { }

        public override Dish ParseInput(IEnumerable<string> input)
            => Dish.ParseFromLines(input.ToArray());


        public override long PartOne(Dish dish)
        {
            long totalLoad = 0;
            foreach (var row in dish.Rocks)
            {
                long rowLoad = 0;
                var spheres = row.Where(c => c.Type == SlotType.Sphere);
                foreach(var cRock in spheres)
                {
                    var rockLoad = dish.Rocks.Length - (cRock.Row) + cRock.EmptyAbove;
                    rowLoad += rockLoad;
                }
                totalLoad += rowLoad;
            }

            return totalLoad;
        }

        public override long PartTwo(Dish input)
        {
            throw new NotImplementedException();
        }
    }
}