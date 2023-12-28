
namespace Advent23.Days.Day16
{
    public record Tile
    {
        public required int Col { get; init; }
        public required int Row { get; init; }
        public required TileType Type { get; init; }

        public bool WestEnergized { get; set; }
        public bool EastEnergized { get; set; }
        public bool SouthEnergized { get; set; }
        public bool NorthEnergized { get; set; }

        public bool IsEnergized => NorthEnergized
                || SouthEnergized
                || EastEnergized
                || WestEnergized;

        public static TileType ParseType(char cChar)
        {
            return cChar switch
            {
                '/' => TileType.NESWMirror,
                '\\' => TileType.NWSEMirror,

                '|' => TileType.NSSplitter,
                '-' => TileType.WESplitter,

                _ => TileType.Blank
            };
        }

        public Tuple<Step?, Step?> GetNextStep(Direction incDirection)
        {
            Direction[] newDirs = GetNewDirections(incDirection);

            Step? step1 = null;
            if (newDirs.Length > 0)
            {
                step1 = new()
                {
                    Towards = newDirs[0],
                    Col = Col + GetColOffset(newDirs[0]),
                    Row = Row + GetRowOffset(newDirs[0]),
                };
            }

            Step? step2 = null;
            if(newDirs.Length > 1)
            {
                step2 = new()
                {
                    Towards = newDirs[1],
                    Col = Col + GetColOffset(newDirs[1]),
                    Row = Row + GetRowOffset(newDirs[1]),
                };
            }

            return Tuple.Create(step1, step2);
        }

        private Direction[] GetNewDirections(Direction incDirection)
        {
            return Type switch
            {
                TileType.Blank => [incDirection],
                TileType.NWSEMirror => SwitchOnMirror(Type, incDirection),
                TileType.NESWMirror => SwitchOnMirror(Type, incDirection),
                TileType.WESplitter => ForkOnSplitter(Type, incDirection),
                TileType.NSSplitter => ForkOnSplitter(Type, incDirection),
                _ => throw new NotImplementedException("Invalid direction"),
            };
        }

        private Direction[] ForkOnSplitter(TileType type, Direction incDirection)
        {
            if(Type == TileType.NSSplitter)
            {
                switch (incDirection)
                {
                    case Direction.North:
                        return [incDirection];
                    case Direction.East:
                        return [Direction.North, Direction.South];
                    case Direction.South:
                        return [incDirection];
                    case Direction.West:
                        return [Direction.North, Direction.South];
                    default:
                        throw new NotImplementedException("Invalid direction");
                }
            }
            else //WESplitter
            {
                switch (incDirection)
                {
                    case Direction.North:
                        return [Direction.West, Direction.East];
                    case Direction.East:
                        return [incDirection];
                    case Direction.South:
                        return [Direction.West, Direction.East];
                    case Direction.West:
                        return [incDirection];
                    default:
                        throw new NotImplementedException("Invalid direction");
                }
            }
        }

        private Direction[] SwitchOnMirror(TileType type, Direction incDirection)
        {
            if(type == TileType.NWSEMirror)
            {
                return incDirection switch
                {
                    Direction.North => [Direction.West],
                    Direction.East => [Direction.South],
                    Direction.South => [Direction.East],
                    Direction.West => [Direction.North],
                    _ => throw new NotImplementedException("Invalid direction"),
                };
            }
            else // NESWMirror
            {
                return incDirection switch
                {
                    Direction.North => [Direction.East],
                    Direction.East => [Direction.North],
                    Direction.South => [Direction.West],
                    Direction.West => [Direction.South],
                    _ => throw new NotImplementedException("Invalid direction"),
                };
            }
        }

        private int GetRowOffset(Direction incDirection)
        {
            return incDirection switch
            {
                Direction.North => -1,
                Direction.East => 0,
                Direction.South => +1,
                Direction.West => 0,
                _ => throw new NotImplementedException("Invalid direction"),
            };
        }
        private int GetColOffset(Direction incDirection)
        {
            return incDirection switch
            {
                Direction.North => 0,
                Direction.East => 1,
                Direction.South => 0,
                Direction.West => -1,
                _ => throw new NotImplementedException("Invalid direction"),
            };
        }

        public void Energize(Direction towards)
        {
            switch (towards)
            {
                case Direction.North:
                    NorthEnergized = true;
                    break;
                case Direction.East:
                    EastEnergized = true;
                    break;
                case Direction.South:
                    SouthEnergized = true;
                    break;
                case Direction.West:
                    WestEnergized = true;
                    break;
                default:
                    throw new NotImplementedException("Invalid direction");
            }
        }

        public bool IsPathEnergized(Direction towards)
        {
            return towards switch
            {
                Direction.North => NorthEnergized,
                Direction.East => EastEnergized,
                Direction.South => SouthEnergized,
                Direction.West => WestEnergized,
                _ => throw new NotImplementedException("Invalid direction"),
            };
        }
    }

    public enum TileType
    {
        Blank,
        NWSEMirror,
        NESWMirror,
        WESplitter,
        NSSplitter
    }
}