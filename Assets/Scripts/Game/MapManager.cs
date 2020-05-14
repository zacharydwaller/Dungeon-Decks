using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [HideInInspector]
    public Transform MapTransf;

    [HideInInspector]
    public GameManager GameManager;

    public GameObject RoomPrefab;

    private const int Columns = 7;
    private const int Rows = 5;

    void Awake()
    {
        MapTransf = GameObject.FindGameObjectWithTag("Map").transform;
        GameManager = GameManager.singleton;
    }

    public void DrawMap(DungeonFloor floor)
    {
        foreach(var mapRoom in MapTransf.GetComponentsInChildren<MapRoom>())
        {
            mapRoom.DisableAll();
        }

        foreach (var room in floor.Rooms)
        {
            int roomIndex = CoordToIndex(room.Coordinate);
            var mapRoom = MapTransf.GetChild(roomIndex).GetComponent<MapRoom>();
            DrawMapRoom(mapRoom, room);
        }
    }

    private void DrawMapRoom(MapRoom mapRoom, DungeonRoom dungeonRoom)
    {
        mapRoom.SetRoomEnable(true);
        
        foreach(var conn in dungeonRoom.Connections)
        {
            mapRoom.SetConnectionEnable(conn.Key, true);
        }

        if(dungeonRoom.Coordinate == GameManager.singleton.CurrentRoomCoord)
        {
            mapRoom.ColorRoom(Color.blue);
        }
        else
        {
            mapRoom.ColorRoom(Color.white);
        }
    }

    private int CoordToIndex(Vector2Int coord)
    {
        var rowCol = CoordToRowCol(coord);

        return (rowCol.y * Columns) + rowCol.x;
    }

    /// <summary>
    ///     Converts the positive/negative coordinates into a row/col format
    ///     X = Col, Y = Row
    /// </summary>
    /// <param name="coord"></param>
    /// <returns></returns>
    private Vector2Int CoordToRowCol(Vector2Int coord)
    {
        var rowCol = new Vector2Int();

        rowCol.x = coord.x + (Columns / 2);
        rowCol.y = -coord.y + (Rows / 2);

        return rowCol;
    }
}
