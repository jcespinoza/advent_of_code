

namespace Advent23.Days.Day10
{
    public record struct Point(int Col, int Row)
    {
        public static Point Move(Point src, Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return new Point { Col = src.Col, Row = src.Row - 1 };
                case Direction.East:
                    return new Point { Col = src.Col + 1, Row = src.Row };
                case Direction.South:
                    return new Point { Col = src.Col, Row = src.Row + 1 };
                case Direction.West:
                    return new Point { Col = src.Col - 1, Row = src.Row };
                case Direction.None:
                default:
                    return new Point { Col = src.Col, Row = src.Row };
            }
        }  
        
        public void MoveSelf(Direction direction)
        {
            var newPoint = Move(this, direction);
            Col = newPoint.Col;
            Row = newPoint.Row;
        }
    }
}