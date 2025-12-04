using AdventOfCode.Commons;

namespace Advent.Days
{
    public class Day01Solver : Solver<Rotation[], long>
    {
        public Day01Solver() : base(AocConstants.Year, 01) { }

        public override Rotation[] ParseInput(IEnumerable<string> input)
            => [.. input
            .Select(line =>
            {
                DirectionLR DirectionLR = line[0] == 'L' ? DirectionLR.Left : DirectionLR.Right;
                int distance = int.Parse(line[1..]);
                return new Rotation { DirectionLR = DirectionLR, distance = distance };
            })];


        public override long PartOne(Rotation[] rotations)
        {
            // Initially the arrow is pointing to the value 50
            // We need to find how many times the arrow points to 0 after all rotations
            int arrowPosition = 50;
            int zeroCount = 0;

            // The dial has 100 positions (0 to 99)
            const int dialSize = 100;
            foreach (var rotation in rotations)
            {
                int dist = rotation.distance;
                arrowPosition = rotation.DirectionLR switch
                {
                    DirectionLR.Left => (arrowPosition - rotation.distance + dialSize) % dialSize,
                    DirectionLR.Right => (arrowPosition + rotation.distance) % dialSize,
                    _ => arrowPosition
                };

                // Check if the arrow points to 0 and add it to the count
                if (arrowPosition == 0)
                {
                    zeroCount++;
                }
            }

            return zeroCount;
        }

        public override long PartTwo(Rotation[] rotations)
        {
            // For this part instead of counting how many times the arrow points to 0,
            // we need to find the number of times the arrow would point to zero in its path to the new position
            int arrowPosition = 50;
            int zeroCount = 0;

            // The dial has 100 positions (0 to 99)
            const int dialSize = 100;
            foreach (var rotation in rotations)
            {
                // Count zeros in the path by finding out how many times the distance wraps around the dial
                if (rotation.distance >= dialSize)
                {
                    zeroCount += rotation.distance / dialSize;
                }

                // Create a rotation for the remaining distance
                var newRotation = new Rotation
                {
                    DirectionLR = rotation.DirectionLR,
                    distance = rotation.distance % dialSize
                };
                // Simulate the remaining distance to see if it causes a zero
                if ( CausesPaintingToZero(newRotation, arrowPosition) )
                {
                    zeroCount++;
                }

                // Move the arrow to the new position
                arrowPosition = rotation.DirectionLR switch
                {
                    DirectionLR.Left => (arrowPosition - rotation.distance + dialSize) % dialSize,
                    DirectionLR.Right => (arrowPosition + rotation.distance) % dialSize,
                    _ => arrowPosition
                };
            }

            return zeroCount;
        }

        /// <summary>
        /// Given a rotation definition and a starting position start_pos,
        /// determine if the rotation causes the arrow to point to zero at any point during its movement
        /// </summary>
        /// <param name="rotation"></param>
        /// <param name="startingPosition"></param>
        /// <returns></returns>
        private static bool CausesPaintingToZero(Rotation rotation, int startingPosition)
        {
            int arrow_position = startingPosition;
            int sign = rotation.DirectionLR == DirectionLR.Left ? -1 : 1;

            int distance = rotation.distance;

            // iterate cyclically over the dial according to the rotation distance
            // Decrease or increase the arrow position according to the rotation direction
            // Stop if we reach zero returning true
            for (int i = 0; i < distance; i++)
            {
                arrow_position = (arrow_position + sign + 100) % 100; // ensure cyclic behavior
                if (arrow_position == 0)
                {
                    return true;
                }
            }

            // If we complete the iterations it means we never pointed to zero
            return false;
        }
    }

    public struct Rotation
    {
        public DirectionLR DirectionLR;
        public int distance;
    }

    public enum DirectionLR {
        Left,
        Right
    }
}