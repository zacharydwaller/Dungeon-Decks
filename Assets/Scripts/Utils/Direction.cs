using System;
using System.Collections.Generic;
using System.Linq;

public enum Direction
{
    North, East, South, West
}

public static class DirectionUtility
{
    public static Direction GetRandom()
    {
        int max = NumDirections();
        int num = new Random().Next(0, max);

        return (Direction) num;
    }

    public static Direction GetClockwise(this Direction dir)
    {
        return (Direction) (((int) dir + 1) % NumDirections());
    }

    public static Direction GetOpposite(this Direction dir)
    {
        return dir.GetClockwise().GetClockwise();
    }

    public static int NumDirections()
    {
        return GetDirections().Count;
    }

    public static List<Direction> GetDirections()
    {
        return Enum.GetValues(typeof(Direction)).Cast<Direction>().ToList();
    }

    public static UnityEngine.Vector2Int ToVector(this Direction dir)
    {
        switch(dir)
        {
            case Direction.North:
                return new UnityEngine.Vector2Int(0, 1);
            case Direction.East:
                return new UnityEngine.Vector2Int(1, 0);
            case Direction.South:
                return new UnityEngine.Vector2Int(0, -1);
            case Direction.West:
                return new UnityEngine.Vector2Int(-1, 0);
        }

        return new UnityEngine.Vector2Int(0, 0);
    }
}