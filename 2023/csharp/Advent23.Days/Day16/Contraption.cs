
namespace Advent23.Days.Day16
{
    public record Contraption
    {
        public required Tile[][] Grid { get; init; }
        public static Contraption Parse(string[] lines)
        {
            var grid = new Tile[lines.Length][];

            for (int row = 0; row < lines.Length; row++)
            {
                string cLine = lines[row];
                var cols = new Tile[cLine.Length];

                for (int col = 0; col < cLine.Length; col++)
                {
                    var cChar = cLine[col];
                    TileType type = Tile.ParseType(cChar);
                    Tile tile = new()
                    {
                        Type = type,
                        Col = col,
                        Row = row
                    };

                    cols[col] = tile;
                }

                grid[row] = cols;
            }

            var contraption = new Contraption
            {
                Grid = grid
            };

            return contraption;
        }

        public void BeamWalk(Step? target)
        {
            if (target == null) return;

            if(target.Row < 0 || target.Col < 0
                || target.Row >= Grid.Length
                || target.Col >= Grid[target.Row].Length)
            {
                return;
            }

            Tile tile = Grid[target.Row][target.Col];

            Direction towards = target.Towards;
            if (tile.IsPathEnergized(towards))
            {
                return;
            }

            tile.Energize(towards);

            var nextSteps = tile.GetNextStep(towards);

            BeamWalk(nextSteps.Item1);
            BeamWalk(nextSteps.Item2);
        }

        public long GetEnergizedBeams()
        {
            return Grid.SelectMany(r => r).Count(t => t.IsEnergized);
        }

        public Contraption Duplicate()
        {
            var rows = new Tile[Grid.Length][];
            for (int row = 0; row < Grid.Length; row++)
            {
                Tile[] sRow = Grid[row];
                Tile[] col = sRow.Select(t => new Tile
                {
                    Col = t.Col,
                    Row = t.Row,
                    Type = t.Type,
                }).ToArray();
                rows[row] = col;
            }

            return new() { Grid = rows };
        }
    }

    public record Step
    {
        public required Direction Towards { get; init; }
        public required int Row { get; init; }
        public required int Col { get; init; }
    }
}