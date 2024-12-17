namespace Advent24.Days.Day14
{
    public record Robot
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int VelX { get; set; }
        public int VelY { get; set; }

        /// <summary>
        /// Parses a string of the form "p=0,4 v=3,-3" into a Robot object
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Robot Parse(string text)
        {
            var parts = text.Split(' ');
            var pos = parts[0].Split('=')[1].Split(',');
            var vel = parts[1].Split('=')[1].Split(',');
            return new Robot
            {
                PosX = int.Parse(pos[0]),
                PosY = int.Parse(pos[1]),
                VelX = int.Parse(vel[0]),
                VelY = int.Parse(vel[1])
            };
        }

        public static (int, int) CalculatePosition(Robot robot, int time, int gridWidth, int gridHeight)
        {
            var initX = robot.PosX;
            var initY = robot.PosY;
            var velX = robot.VelX;
            var velY = robot.VelY;

            int deltaX = time * velX;
            int totalX = (initX + deltaX);
            int newPosX = Mod(totalX, gridWidth);

            int deltaY = time * velY;
            int totalY = (initY + deltaY);
            int newPosY = Mod(totalY, gridHeight);

            return (newPosX, newPosY);
        }
        public static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }
    }
}
