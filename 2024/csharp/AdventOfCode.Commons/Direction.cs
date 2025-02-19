﻿using System.Diagnostics;

namespace AdventOfCode.Commons
{
    public enum Direction
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest,
        None
    }

    public static class DirectionExtensions
    {
        public static Dictionary<Direction, int> DirectionToAngle = new()
        {
            { Direction.North, 0 },
            { Direction.NorthEast, 45 },
            { Direction.East, 90 },
            { Direction.SouthEast, 135 },
            { Direction.South, 180 },
            { Direction.SouthWest, 225 },
            { Direction.West, 270 },
            { Direction.NorthWest, 315 }
        };

        public static Direction Turn90(this Direction direction)
        {
            return direction.RotateOrtogonal(1);
        }

        public static Direction Turn180(this Direction direction)
        {
            return direction.RotateOrtogonal(2);
        }

        public static Direction TurnLeft(this Direction direction)
        {
            return direction.RotateOrtogonal(-1);
        }

        public static Direction ToDirection(this (int rowOffset, int colOffset) offset) => offset switch
        {
            (-1, 0) => Direction.North,
            (-1, 1) => Direction.NorthEast,
            (0, 1) => Direction.East,
            (1, 1) => Direction.SouthEast,
            (1, 0) => Direction.South,
            (1, -1) => Direction.SouthWest,
            (0, -1) => Direction.West,
            (-1, -1) => Direction.NorthWest,
            _ => throw new ArgumentException("Invalid offsets")
        };

        public static (int, int) ToOffsets(this Direction direction) => direction switch
        {
            Direction.North => (-1, 0),
            Direction.NorthEast => (-1, 1),
            Direction.East => (0, 1),
            Direction.SouthEast => (1, 1),
            Direction.South => (1, 0),
            Direction.SouthWest => (1, -1),
            Direction.West => (0, -1),
            Direction.NorthWest => (-1, -1),
            _ => throw new ArgumentException("Invalid direction")
        };

        public static Direction RotateOrtogonal(this Direction direction, int steps)
        {
            var directions = new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West };
            var index = Array.IndexOf(directions, direction);
            index = (index + steps) % directions.Length;
            if (index < 0)
            {
                index += directions.Length;
            }
            return directions[index];
        }

        public static int RowOffset(this Direction direction)
        {
            return direction switch
            {
                Direction.North => -1,
                Direction.South => 1,
                _ => 0,
            };
        }

        public static int ColOffset(this Direction direction)
        {
            return direction switch
            {
                Direction.East => 1,
                Direction.West => -1,
                _ => 0,
            };
        }

        public static bool IsPerpedicular(this Direction cDirection, Direction otherDirection)
        {
            if(cDirection == otherDirection) return false;

            if (
                (cDirection == Direction.North || cDirection != Direction.South)
                && (otherDirection == Direction.East || otherDirection == Direction.West)
            )
            {
                return true;
            }

            if(
                (cDirection == Direction.East || cDirection == Direction.West)
                && (otherDirection == Direction.North || otherDirection == Direction.South)
            )
            {
                return true;
            }

            return false;
        }

        public static bool IsOpposite(this Direction cDirection, Direction otherDirection)
        {
            if (cDirection == Direction.North && otherDirection == Direction.South) return true;
            if (cDirection == Direction.South && otherDirection == Direction.North) return true;
            if (cDirection == Direction.East && otherDirection == Direction.West) return true;
            if (cDirection == Direction.West && otherDirection == Direction.East) return true;

            if (cDirection == Direction.NorthEast && otherDirection == Direction.SouthWest) return true;
            if (cDirection == Direction.SouthWest && otherDirection == Direction.NorthEast) return true;
            if (cDirection == Direction.NorthWest && otherDirection == Direction.SouthEast) return true;
            if (cDirection == Direction.SouthEast && otherDirection == Direction.NorthWest) return true;

            return false;
        }
    }
}