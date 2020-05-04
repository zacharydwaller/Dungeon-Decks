using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonRoom : MonoBehaviour
{
    public int RoomId;
    public bool IsMainRoom = false;

    public SpriteRenderer PlaceholderGraphic;

    public void Awake()
    {
        PlaceholderGraphic = GetComponentInChildren<SpriteRenderer>();
    }
}
