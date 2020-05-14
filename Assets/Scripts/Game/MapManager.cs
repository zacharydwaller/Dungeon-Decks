using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [HideInInspector]
    public Transform MapTransf;

    [HideInInspector]
    public GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        MapTransf = GameObject.FindGameObjectWithTag("Map").transform;
        GameManager = GameManager.singleton;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
