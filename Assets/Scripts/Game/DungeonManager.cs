using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [HideInInspector]
    public Transform BoardTransform;

    [HideInInspector]
    public MapManager MapManager;

    public int RoomsMean;
    public int RoomsStd;

    public const int MaxColumns = 7;
    public const int MaxRows = 5;

    public List<DungeonFloor> Floors;

    public int Level { get; private set; }

    //public GameObject WallPrefab;
    //public GameObject FloorPrefab;
    //public GameObject DoorPrefab;

    //public Sprite[] WallSprites;
    //public Sprite[] FloorSprites;
    //public Sprite DoorSprite;

    private System.Random Rand = new System.Random();

    public void Awake()
    {
        BoardTransform = GameObject.FindGameObjectWithTag("Board").transform;
        MapManager = GetComponent<MapManager>();
    }

    public DungeonManager()
    {
        Floors = new List<DungeonFloor>();
        Level = 0;
    }

    public DungeonFloor GetCurrentFloor()
    {
        if(Level > Floors.Count || Level <= 0)
        {
            return null;
        }

        return Floors[Level - 1];
    }

    #region DungeonGeneration

    public DungeonFloor GenerateFloor()
    {
        var level = Level + 1;

        var floor = new DungeonFloor(level);
        Floors.Add(floor);

        var numRooms = Rand.NextGaussian(RoomsMean, RoomsStd);

        Debug.Log($"Generating Floor {level} with {numRooms} rooms.");

        return floor.GenerateFloor(numRooms);
    }

    public DungeonFloor AdvanceFloor()
    {
        if(Level + 1 >= Floors.Count)
        {
            GenerateFloor();
        }

        Level++;

        var currentFloor = GetCurrentFloor();
        currentFloor.DrawRoomAtCoord(new Vector2Int(0, 0));
        MapManager.DrawMap(currentFloor);

        return currentFloor;
    }

    public DungeonFloor DecrementFloor()
    {
        if(Level <= 1)
        {
            Level = 1;
        }
        else
        {
            Level--;
        }

        return GetCurrentFloor();
    }

    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        if (GetCurrentFloor() != null)
        {
            foreach(var room in GetCurrentFloor().Rooms)
            {
                GizmosDrawRoom(room);
            }
        }
    }

    private void GizmosDrawRoom(DungeonRoom room)
    {
        var position = new Vector3(room.Coordinate.x, room.Coordinate.y);

        Gizmos.color = new Color(66 / 255f, 135 / 255f, 245 / 255f);
        Gizmos.DrawCube(position, Vector3.one * 0.8f);

        foreach (var connection in room.Connections)
        {
            GizmosDrawConnection(room, GetCurrentFloor().GetRoomById(connection.Value));
        }
    }

    private void GizmosDrawConnection(DungeonRoom roomA, DungeonRoom roomB)
    {
        var positionA = new Vector3(roomA.Coordinate.x, roomA.Coordinate.y);
        var positionB = new Vector3(roomB.Coordinate.x, roomB.Coordinate.y);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(positionA, positionB);
    }

    #endregion

}
