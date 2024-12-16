namespace Advent24.Days
{
    public class Region
    {
        public char Plant { get; set; }
        public int Perimeter { get; set; }
        public int Area { get; set; }

        public override string ToString()
        {
            return $"Plant: {Plant}, Area: {Area}, Perimeter: {Perimeter}";
        }
    }
}