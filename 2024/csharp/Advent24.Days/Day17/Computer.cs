namespace Advent24.Days
{
    public record Computer
    {
        public enum Instruction
        {
            ADV,
            BXL,
            BST,
            JNZ,
            BXC,
            OUT,
            BDV,
            CDV
        }
        public bool Ready { get; set; }
        public int ProgramPointer { get; set; }
        public int RegA { get; set; }
        public int RegB { get; set; }
        public int RegC { get; set; }
        public int[] Program { get; set; } = [];

        public void MovePointer(int offset)
        {
            if (ProgramPointer + offset < 0 || ProgramPointer + offset >= Program.Length)
            {
                Ready = false;
                return;
            }
            ProgramPointer += offset;
        }

        public void JumpTo(int position)
        {
            if(position < 0 || position >= Program.Length)
            {
                Ready = false;
                return;
            }
            ProgramPointer = position;
        }

        public int ReadNext()
        {
            if (ProgramPointer >= Program.Length)
            {
                Ready = false;
                return -1;
            }
            int instruction = Program[ProgramPointer];
            MovePointer(1);
            return instruction;
        }
    }
}