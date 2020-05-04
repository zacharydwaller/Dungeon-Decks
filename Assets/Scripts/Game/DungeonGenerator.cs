using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject FloorPrefab;
    public GameObject WallPrefab;
    public GameObject DoorPrefab;

    public GameObject EnemyPrefab;
    public GameObject CardPrefab;

    public GameObject RoomPrefab;

    public Tile[] FloorTiles;

    public int MaxRoomCount;
    public int MinSideLength;
    public float MaxSideLength;

    public int SideLengthMean;
    public float SideLengthStd;

    public float GenCircleRadius;
    public int MainRoomThreshold;

    public float GenerationTimescale;
    public float GenerationDuration;

    private float GenerationStart;
    private bool FinishedSeparation = false;

    public List<DungeonRoom> Rooms;

    public void Update()
    {
        if(!FinishedSeparation && Time.time > GenerationStart + (GenerationDuration * Time.timeScale))
        {
            LockLocations();
            BuildRooms();
        }
    }

    public void GenerateDungeon()
    {
        Rooms = new List<DungeonRoom>();

        Time.timeScale = GenerationTimescale;
        GenerationStart = Time.time;

        Debug.Log($"Dungeon Generator Starting");

        for(int i = 0; i < MaxRoomCount; i++)
        {
            var roomObj = Instantiate(RoomPrefab, transform);

            Vector2 location = UnityEngine.Random.insideUnitCircle * new Vector2(GenCircleRadius, GenCircleRadius);
            roomObj.transform.position = location;

            int width = Mathf.Max(MinSideLength, GaussianRandom(SideLengthMean, SideLengthStd));
            int height = Mathf.Max(MinSideLength, GaussianRandom(SideLengthMean, SideLengthStd));
            roomObj.transform.localScale = new Vector3(width, height);

            var room = new DungeonRoom(i, roomObj.transform);
            Rooms.Add(room);

            if(width >= MainRoomThreshold && height >= MainRoomThreshold)
            {
                room.IsMainRoom = true;
                room.PlaceholderGraphic.color = Color.red;
            }
            else
            {
                room.PlaceholderGraphic.color = Color.blue;
            }
        }
    }

    private void LockLocations()
    {
        FinishedSeparation = true;
        Time.timeScale = 1.0f;

        float elapsed = (Time.time - GenerationStart) / GenerationTimescale * 1000.0f;
        Debug.Log($"Dungeon Generator Finished in {elapsed} ms");

        foreach (var room in Rooms)
        {
            //room.ObjectTransform.GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    private void BuildRooms()
    {
        var tilemap = GameObject.Find("TilemapBase").GetComponent<Tilemap>();
        var mainRooms = GetMainRooms();

        // Build connection rooms
        foreach(var room in mainRooms)
        {
            // Place main room tiles
            room.PlaceTiles(FloorTiles[0]);

            // Place connecting lines
            var connections = room.FindConnections(Rooms);
            foreach(var connection in connections)
            {
                // Fill connecting rooms
                if(!connection.IsMainRoom)
                {
                    connection.PlaceTiles(FloorTiles[1]);
                }
            }

        }
    }

    private List<DungeonRoom> GetMainRooms()
    {
        return Rooms.Where(r => r.IsMainRoom).ToList();
    }

    private List<DungeonRoom> GetSideRooms()
    {
        return Rooms.Where(r => !r.IsMainRoom).ToList();
    }

    private System.Random rand = new System.Random();

    /// <summary>
    ///     Box-Muller Transform fast Gaussian distribution
    ///     Implemented by Jarrett Meyer
    /// </summary>
    /// <param name="mean"></param>
    /// <param name="stdDev"></param>
    /// <returns></returns>
    private int GaussianRandom(int mean, float stdDev)
    {
        double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
        double randNormal = mean + stdDev * randStdNormal;

        float randFloat = (float) randNormal;

        return Mathf.RoundToInt(randFloat);
    }
}
