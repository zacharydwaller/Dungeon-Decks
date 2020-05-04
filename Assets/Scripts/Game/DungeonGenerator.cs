using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public Transform DungeonTransform;

    public GameObject FloorPrefab;
    public GameObject WallPrefab;
    public GameObject DoorPrefab;

    public GameObject EnemyPrefab;
    public GameObject CardPrefab;

    public GameObject RoomPrefab;

    public Sprite[] FloorTiles;
    public Sprite[] WallTiles;
    public Sprite DoorTile;

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
            FinishSeparation();
        }
    }

    public void GenerateDungeon()
    {
        Time.timeScale = GenerationTimescale;
        GenerationStart = Time.time;

        Debug.Log($"Dungeon Generator Starting");

        for(int i = 0; i < MaxRoomCount; i++)
        {
            var roomObj = Instantiate(RoomPrefab, DungeonTransform);

            Vector2 location = UnityEngine.Random.insideUnitCircle * new Vector2(GenCircleRadius, GenCircleRadius);
            roomObj.transform.position = location;

            int width = Mathf.Max(MinSideLength, GaussianRandom(SideLengthMean, SideLengthStd));
            int height = Mathf.Max(MinSideLength, GaussianRandom(SideLengthMean, SideLengthStd));
            roomObj.transform.localScale = new Vector3(width, height);

            var roomScr = roomObj.GetComponent<DungeonRoom>();
            Rooms.Add(roomScr);
            roomScr.RoomId = i;

            if(width >= MainRoomThreshold || height >= MainRoomThreshold)
            {
                roomScr.IsMainRoom = true;
                roomScr.PlaceholderGraphic.color = Color.red;
            }
            else
            {
                roomScr.PlaceholderGraphic.color = Color.blue;
            }
        }
    }

    private void FinishSeparation()
    {
        FinishedSeparation = true;
        Time.timeScale = 1.0f;

        float elapsed = (Time.time - GenerationStart) / GenerationTimescale * 1000.0f;
        Debug.Log($"Dungeon Generator Finished in {elapsed} ms");

        foreach (var room in Rooms)
        {
            room.GetComponent<Rigidbody2D>().simulated = false;

            // Round position 
            Vector2 position = room.transform.position;
            float x = Mathf.Round(position.x);
            float y = Mathf.Round(position.y);

            room.transform.position = new Vector2(x, y);
        }
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
