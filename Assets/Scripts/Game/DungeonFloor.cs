using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonFloor
{
    public int Level;

    public List<DungeonRoom> Rooms;

    private System.Random Rand = new System.Random();

    private int RoomWidthMean = 15;
    private int RoomWidthStd = 3;

    private int RoomHeightMean = 11;
    private int RoomHeightStd = 2;

    private const int MaxXCoord = 3;
    private const int MaxYCoord = 2;

    public DungeonFloor(int level)
    {
        Level = level;
        Rooms = new List<DungeonRoom>();
    }

    public void DrawRoomAtCoord(Vector2Int coord)
    {
        var room = GetRoomAtCoordinate(coord);
        room.DrawRoom();
    }

    #region DungeonGeneration

    public DungeonRoom AddRoom(DungeonRoom room)
    {
        Rooms.Add(room);
        return room;
    }

    public DungeonRoom GetRoomInDirection(DungeonRoom room, Direction dir)
    {
        var newCoord = room.Coordinate + dir.ToVector();

        return GetRoomAtCoordinate(newCoord);
    }

    public DungeonRoom GetRoomAtCoordinate(Vector2Int coord)
    {
        return Rooms.FirstOrDefault(r => r.Coordinate == coord);
    }

    public DungeonRoom GetRoomById(int id)
    {
        return Rooms.FirstOrDefault(r => r.Id == id);
    }

    public DungeonFloor GenerateFloor(int numRooms)
    {
        int id = 0;

        while(Rooms.Count < numRooms)
        {
            if(id == 0)
            {
               AddStartingRoom();
            }
            else
            {
                AddRandomRoom(id);
            }

            id++;
        }

        int extraConnections = Mathf.RoundToInt(numRooms * 0.15f);
        Debug.Log($"DungeonGenerator: Adding {extraConnections} extra connections.");
        for(int i = 0; i < extraConnections; i++)
        {
            AddRandomConnection();
        }

        return this;
    }

    public DungeonRoom AddStartingRoom()
    {
        var width = Rand.NextGaussian(RoomWidthMean, RoomWidthStd);
        var height = Rand.NextGaussian(RoomHeightMean, RoomHeightStd);

        var room = new DungeonRoom(0, Level, new Vector2Int(0, 0), width, height);
        Rooms.Add(room);

        return room;
    }

    public DungeonRoom AddRandomRoom(int id)
    {
        if (Rooms.Count == 0) return AddStartingRoom();

        DungeonRoom parentRoom = GetRandomParentRoom();
        var dir = DirectionUtility.GetRandom();

        // Get a coordinate that isn't taken up by an existing room
        DungeonRoom roomInDirection;
        do
        {
            dir = dir.GetClockwise();
            roomInDirection = GetRoomInDirection(parentRoom, dir);
        } while (roomInDirection != null);

        // Create new room, connect newRoom to parentRoom
        var coord = parentRoom.Coordinate + dir.ToVector();
        var width = Rand.NextGaussian(RoomWidthMean, RoomWidthStd);
        var height = Rand.NextGaussian(RoomHeightMean, RoomHeightStd);
        var newRoom = new DungeonRoom(id, Level, coord, width, height);

        parentRoom.AddConnection(dir, id);
        newRoom.AddConnection(dir.GetOpposite(), parentRoom.Id);

        Rooms.Add(newRoom);

        return newRoom;
    }

    public void AddRandomConnection()
    {
        bool connectionMade = false;

        do
        { 
            var room = GetRandomRoom();
            var dir = DirectionUtility.GetRandom();

            for(int i = 0; i < 4; i++)
            {
                var secondRoom = GetRoomInDirection(room, dir);
                if (secondRoom != null && !room.Connections.ContainsKey(dir))
                {
                    room.AddConnection(dir, secondRoom.Id);
                    secondRoom.AddConnection(dir.GetOpposite(), room.Id);

                    connectionMade = true;
                }
                else
                {
                    dir = dir.GetClockwise();
                }
            }
            
        } while(!connectionMade);
    }

    /// <summary>
    ///     Gets a random room capable of creating a new connected room
    ///     Require room to not be surrounded by other rooms
    /// </summary>
    /// <returns></returns>
    public DungeonRoom GetRandomParentRoom()
    {
        var room = GetRandomRoom();

        // If room is surrounded, pick another room
        while(IsRoomSurounded(room))
        {
            room = GetRandomRoom();
        }

        return room;
    }

    public DungeonRoom GetRandomRoom()
    {
        if (Rooms.Count == 0) return null;

        int id = Rand.Next(0, Rooms.Count - 1);
        var room = Rooms[id];

        return room;
    }

    public bool IsRoomSurounded(DungeonRoom room)
    {
        bool surroundedByRooms = true;

        foreach (var dir in DirectionUtility.GetDirections())
        {
            if (GetRoomInDirection(room, dir) == null)
            {
                surroundedByRooms = false;
            }
        }

        bool onEdge = false;
        var coord = room.Coordinate;
        if(Mathf.Abs(coord.x) >= MaxXCoord
            || Mathf.Abs(coord.y) >= MaxYCoord)
        {
            onEdge = true;
        }

        return surroundedByRooms || onEdge;
    }

    #endregion
}