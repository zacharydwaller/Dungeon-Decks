using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonRoom
{
    public int RoomId;
    public bool IsMainRoom = false;

    public Transform ObjectTransform;
    public SpriteRenderer PlaceholderGraphic;

    public DungeonRoom(int id, Transform transform)
    {
        RoomId = id;

        ObjectTransform = transform;
        PlaceholderGraphic = ObjectTransform.GetComponentInChildren<SpriteRenderer>();
    }

    public void PlaceTiles(Tile floorTile)
    {
        var tilemap = GameObject.Find("TilemapBase").GetComponent<Tilemap>();

        var position = ObjectTransform.position;
        var scale = ObjectTransform.localScale;

        var startPos = new Vector3(position.x - (scale.x / 2), position.y - (scale.y / 2), 0);
        var endPos = new Vector3(position.x + (scale.x / 2), position.y + (scale.y / 2), 0);

        tilemap.BoxFill(floorTile, startPos, endPos);
    }

    public List<DungeonRoom> FindConnections(List<DungeonRoom> rooms)
    {
        var connectedRooms = FindRoomsInDirection(rooms, new Vector2(1, 0));
        connectedRooms.AddRange(FindRoomsInDirection(rooms, new Vector2(0, 1)));

        return connectedRooms;
    }

    private List<DungeonRoom> FindRoomsInDirection(List<DungeonRoom> rooms, Vector2 direction)
    {
        Vector2 start = ObjectTransform.position;

        var thisCollider = ObjectTransform.GetComponent<Collider2D>();
        //thisCollider.enabled = false;

        var rayHits = Physics2D.RaycastAll(start, direction);
        thisCollider.enabled = true;

        var transformIds = rayHits.Select(h => h.transform.GetInstanceID());
        var connectedRooms = rooms.Where(r => transformIds.Contains(r.ObjectTransform.GetInstanceID())).ToList();

        return connectedRooms;
    }
}
