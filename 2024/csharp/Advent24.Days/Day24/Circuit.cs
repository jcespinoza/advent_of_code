namespace Advent24.Days
{
    public record Circuit
    {
        public List<Wire> Input { get; set; } = [];
        public List<Gate> Gates { get; set; } = [];
    }
    public record Wire
    {
        public string Name { get; set; } = string.Empty;
        public bool Value { get; set; }
    }

    public record Gate
    {
        public string Target { get; set; } = string.Empty;
        public BoolOperation Operation { get; set; }
        public string InputA { get; set; } = string.Empty;
        public string InputB { get; set; } = string.Empty;
    }
    public enum BoolOperation
    {
        AND,
        OR,
        XOR,
    }
}