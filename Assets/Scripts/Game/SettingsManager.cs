using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public StatType primaryStat = StatType.Strength;
    public StatType offStat = StatType.Strength;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static SettingsManager GetScript
    {
        get { return GameObject.FindGameObjectWithTag("SettingsManager").GetComponent<SettingsManager>(); }
    }
}
