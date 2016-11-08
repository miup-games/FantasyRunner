using System;
using UnityEngine;

public class SpeedItem : ItemUsageController 
{
    [SerializeField] public float speedFactor = 2f;
    [SerializeField] public float time = 1f;

    protected override void UseOverCharacter(Character character)
    {
        character.SetSpeedFactor(speedFactor, time);
    }
}
