using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapRoom : MonoBehaviour
{
    public GameObject Room;
    public GameObject North;
    public GameObject South;
    public GameObject East;
    public GameObject West;

    void Awake()
    {
        DisableAll();
    }

    public void SetRoomEnable(bool enable)
    {
        Room.SetActive(enable);
    }

    public void SetConnectionEnable(Direction dir, bool enable)
    {
        switch(dir)
        {
            case Direction.North:
                North.SetActive(enable);
                break;
            case Direction.East:
                East.SetActive(enable);
                break;
            case Direction.South:
                South.SetActive(enable);
                break;
            case Direction.West:
                West.SetActive(enable);
                break;
        }
    }

    public void ColorRoom(Color color)
    {
        Room.GetComponent<Image>().color = color;
    }

    public void DisableAll()
    {
        SetRoomEnable(false);
        DisableAllConnections();
    }

    public void DisableAllConnections()
    {
        foreach(var dir in DirectionUtility.GetDirections())
        {
            SetConnectionEnable(dir, false);
        }
    }
}
