using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public int RoomsMean;
    public int RoomsStd;

    public List<DungeonFloor> Floors;
    public DungeonFloor CurrentFloor { get; private set; }
    public int Level { get; private set; }

    private System.Random Rand = new System.Random();

    public DungeonManager()
    {
        Floors = new List<DungeonFloor>();
        CurrentFloor = null;
        Level = 0;
    }

    public DungeonFloor GenerateFloor()
    {
        Level = Floors.Count + 1;

        var floor = new DungeonFloor(Level);
        Floors.Add(floor);

        if (CurrentFloor == null) CurrentFloor = floor;

        var numRooms = Rand.NextGaussian(RoomsMean, RoomsStd);

        Debug.Log($"Generating Floor {Level} with {numRooms} rooms.");

        return floor.GenerateFloor(numRooms);
    }

    public DungeonFloor IncrementFloor()
    {
        Level++;

        if(Level >= Floors.Count)
        {
            CurrentFloor = GenerateFloor();
        }
        else
        {
            CurrentFloor = Floors[Level];
        }

        return CurrentFloor;
    }

    public DungeonFloor DecrementFloor()
    {
        Level--;

        if(Level <= 0)
        {
            Level = 1;
        }

        CurrentFloor = Floors[Level];
        return CurrentFloor;
    }

    private void OnDrawGizmos()
    {
        if (CurrentFloor != null)
        {
            foreach(var room in CurrentFloor.Rooms)
            {
                DrawRoom(room);
            }
        }
    }

    private void DrawRoom(DungeonRoom room)
    {
        var position = new Vector3(room.Coordinate.x, room.Coordinate.y);

        Gizmos.color = new Color(66 / 255f, 135 / 255f, 245 / 255f);
        Gizmos.DrawCube(position, Vector3.one * 0.8f);

        foreach (var connection in room.Connections)
        {
            DrawConnection(room, CurrentFloor.GetRoomById(connection.Value));
        }
    }

    private void DrawConnection(DungeonRoom roomA, DungeonRoom roomB)
    {
        var positionA = new Vector3(roomA.Coordinate.x, roomA.Coordinate.y);
        var positionB = new Vector3(roomB.Coordinate.x, roomB.Coordinate.y);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(positionA, positionB);
    }
}
