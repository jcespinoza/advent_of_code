
namespace Advent23.Days.Day14
{
    public record Dish
    {
        public required Rock[][] Rocks { get; init; }

        public static Dish ParseFromLines(string[] input)
        {
            Rock[][] rocks = new Rock[input.Length][];
            int[] emptySlots = new int[input[0].Length];
            for (int row = 0; row < input.Length; row++)
            {
                rocks[row] = new Rock[input[row].Length];

                for (int col = 0; col < input[row].Length; col++)
                {
                    var cSymbol = input[row][col];
                    if(cSymbol == '.')
                    {
                        emptySlots[col]++;
                    }else if (cSymbol == '#')
                    {
                        emptySlots[col] = 0;
                    }
                    var type = Rock.ParseType(input[row][col]);
                    rocks[row][col] = new Rock { 
                        Row = row, 
                        Col = col, 
                        Type = type, 
                        EmptyAbove = emptySlots[col]
                    };
                }
            }

            var dish = new Dish { 
                Rocks = rocks    
            };

            return dish;
        }
    }
}