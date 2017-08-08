using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public enum StatType
    {
        Health, Armor
    }

    public Player owner;
    public StatType statType;

    private Slider slider;

    private int stat
    {
        get
        {
            if(statType == StatType.Health)
                return owner.health;
            else
                return owner.armor;
        }
    }

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if(owner == null)
        {
            owner = GameManager.singleton.player;

            if(owner) Init();
            else return;
        }

        int _stat = stat;

        if(slider.value != _stat)
        {
            if(_stat > slider.maxValue)
            {
                slider.maxValue = _stat;
            }

            slider.value = _stat;
        }
    }

    private void Init()
    {
        int _stat = stat;

        slider.minValue = 0;
        slider.maxValue = _stat;
        slider.value = _stat;
    }
}
