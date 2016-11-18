using System;
using UnityEngine;

public abstract class AccesoryItem : ItemUsageController 
{
    [SerializeField] private float accesoryValue = 5f;
    [SerializeField] private float accesoryDuration = 5f;
    [SerializeField] private Sprite iconSprite;

    public float AccesoryValue
    {
        get
        {
            return this.accesoryValue;
        }
    }

    public float AccesoryDuration
    {
        get
        {
            return this.accesoryDuration;
        }
    }

    public Sprite IconSprite
    {
        get
        {
            return this.iconSprite;
        }
    }
}
