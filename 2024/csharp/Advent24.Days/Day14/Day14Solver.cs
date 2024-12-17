using Advent24.Days.Day14;
using AdventOfCode.Commons;

namespace Advent24.Days
{
    public class Day14Solver : Solver<Robot[], long>
    {
        public Day14Solver() : base(2024, 14) { }

        public override Robot[] ParseInput(IEnumerable<string> input)
        => input.Select(Robot.Parse).ToArray();


        public override long PartOne(Robot[] robots)
        {
            int GRID_WIDTH = 101;
            int GRID_HEIGHT = 103;
            
            return CalculateSafetyFactor(robots, 100, GRID_WIDTH, GRID_HEIGHT);
        }

        public static long CalculateSafetyFactor(Robot[] robots, int time, int gridWidth, int gridHeight)
        {
            foreach (var robot in robots)
            {
                var (newPosX, newPosY) = Robot.CalculatePosition(robot, time, gridWidth, gridHeight);
                robot.PosX = newPosX;
                robot.PosY = newPosY;
            }
            long quadrant1 = 0;
            long quadrant2 = 0;
            long quadrant3 = 0;
            long quadrant4 = 0;
            int halfWidth = gridWidth / 2;
            int halfHeight = gridHeight / 2;
            for (int index = 0; index < robots.Length; index++)
            {
                int posX = robots[index].PosX;
                int posY = robots[index].PosY;

                if (posX == halfWidth || posY == halfHeight) continue;

                if (posX < halfWidth && posY < halfHeight)
                {
                    quadrant1++;
                }
                else if (posX > halfWidth && posY < halfHeight)
                {
                    quadrant2++;
                }
                else if (posX < halfWidth && posY > halfHeight)
                {
                    quadrant3++;
                }
                else if (posX > halfWidth && posY > halfHeight)
                {
                    quadrant4++;
                }
            }

            long safetyFactor = quadrant1 * quadrant2 * quadrant3 * quadrant4;

            return safetyFactor;
        }

        public override long PartTwo(Robot[] robots)
        {
            throw new NotImplementedException();
        }
    }
}