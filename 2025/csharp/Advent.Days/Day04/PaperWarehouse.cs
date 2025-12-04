namespace Advent.Days
{
    public enum SlotContent
    {
        Empty,   // '.'
        Roll,    // '@'
        Removed, // 'x'
    }

    public class PaperWarehouse(List<List<SlotContent>> grid)
    {
        public List<List<SlotContent>> Grid { get; init; } = grid;

        public static PaperWarehouse FromLines(IEnumerable<string> lines)
        {
            var grid = lines.Select(line =>
                line.Select(c => c switch
                {
                    '.' => SlotContent.Empty,
                    '@' => SlotContent.Roll,
                    _ => throw new System.ArgumentException($"Invalid slot char: {c}")
                }).ToList()
            ).ToList();

            return new PaperWarehouse(grid);
        }

        public PaperWarehouse Clone()
        {
            var newGrid = Grid.Select(row => row.ToList()).ToList();
            return new PaperWarehouse(newGrid);
        }

        public bool IsAccessible(int row, int col)
        {
            var rows = Grid.Count;
            if (rows == 0) return false;
            var cols = Grid[0].Count;

            var directions = new (int dr, int dc)[]
            {
                (-1, -1), (-1, 0), (-1, 1),
                (0, -1), /*self*/ (0, 1),
                (1, -1), (1, 0), (1, 1)
            };

            int adjacentRolls = 0;

            foreach (var (dr, dc) in directions)
            {
                var nr = row + dr;
                var nc = col + dc;
                if (nr >= 0 && nr < rows && nc >= 0 && nc < cols)
                {
                    if (Grid[nr][nc] == SlotContent.Roll)
                    {
                        adjacentRolls++;
                    }
                }

                if (adjacentRolls >= 4)
                    return false;
            }

            return adjacentRolls < 4;
        }

        public long CountAccessibleRolls()
        {
            long count = 0;
            for (int r = 0; r < Grid.Count; r++)
            {
                for (int c = 0; c < Grid[0].Count; c++)
                {
                    if (Grid[r][c] == SlotContent.Roll && IsAccessible(r, c))
                        count++;
                }
            }

            return count;
        }

        public long CountRemovedRolls()
        {
            long removed = 0;
            foreach (var row in Grid)
            {
                foreach (var slot in row)
                {
                    if (slot == SlotContent.Removed) removed++;
                }
            }
            return removed;
        }

        public void RemoveAccessibleRollsQueue()
        {
            var rows = Grid.Count;
            if (rows == 0) return;
            var cols = Grid[0].Count;

            var enqueued = Enumerable.Range(0, rows).Select(_ => new bool[cols]).ToArray();
            var q = new System.Collections.Generic.Queue<(int r, int c)>();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (Grid[r][c] == SlotContent.Roll && IsAccessible(r, c))
                    {
                        q.Enqueue((r, c));
                        enqueued[r][c] = true;
                    }
                }
            }

            while (q.Count > 0)
            {
                var (r, c) = q.Dequeue();
                enqueued[r][c] = false;

                if (Grid[r][c] == SlotContent.Roll && IsAccessible(r, c))
                {
                    Grid[r][c] = SlotContent.Removed;

                    for (int dr = -1; dr <= 1; dr++)
                    {
                        for (int dc = -1; dc <= 1; dc++)
                        {
                            if (dr == 0 && dc == 0) continue;
                            var nr = r + dr;
                            var nc = c + dc;
                            if (nr >= 0 && nr < rows && nc >= 0 && nc < cols)
                            {
                                if (!enqueued[nr][nc] && Grid[nr][nc] == SlotContent.Roll)
                                {
                                    q.Enqueue((nr, nc));
                                    enqueued[nr][nc] = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
