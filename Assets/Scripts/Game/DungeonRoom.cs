using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom
{
    public int Id;
    public Vector2Int Coordinate;
    public Dictionary<Direction, int> Connections;

    public bool FullyConnected { get => Connections.Count == DirectionUtility.NumDirections(); }

    public DungeonRoom(int id, Vector2Int coord)
    {
        Id = id;
        Coordinate = coord;
        Connections = new Dictionary<Direction, int>();
    }

    public void AddConnection(Direction dir, int roomId)
    {
        try
        {
            Connections.Add(dir, roomId);
        }
        catch (Exception /*e*/) { }
    }
}
