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

    public bool TogglePhysics;
    private bool SimulatingPhysics = false;

    public List<DungeonRoom> Rooms;

    public void Update()
    {
        if(TogglePhysics)
        {
            TogglePhysics = false;
            SimulatingPhysics = !SimulatingPhysics;

            foreach (var room in Rooms)
            {
                room.GetComponent<Rigidbody2D>().simulated = SimulatingPhysics;
            }
        }
    }

    public void GenerateDungeon()
    {
        for(int i = 0; i < MaxRoomCount; i++)
        {
            var roomObj = Instantiate(RoomPrefab, DungeonTransform);
            roomObj.GetComponent<Rigidbody2D>().simulated = SimulatingPhysics;

            Vector2 location = UnityEngine.Random.insideUnitCircle * new Vector2(GenCircleRadius, GenCircleRadius);
            roomObj.transform.position = location;

            int width = Mathf.Max(MinSideLength, GaussianRandom(SideLengthMean, SideLengthStd));
            int height = Mathf.Max(MinSideLength, GaussianRandom(SideLengthMean, SideLengthStd));
            roomObj.transform.localScale = new Vector3(width, height);

            var roomScr = roomObj.GetComponent<DungeonRoom>();
            Rooms.Add(roomScr);
            roomScr.RoomId = i;

            if(width >= MainRoomThreshold && height >= MainRoomThreshold)
            {
                roomScr.IsMainRoom = true;
                roomObj.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                roomObj.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
    }

    private System.Random rand = new System.Random();

    private int GaussianRandom(int mean, float stdDev)
    {
        double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
        double randNormal = mean + stdDev * randStdNormal;

        float randFloat = (float) randNormal;

        Debug.Log(randFloat);

        return Mathf.RoundToInt(randFloat);
    }
}
