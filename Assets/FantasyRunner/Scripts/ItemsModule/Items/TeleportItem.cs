using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportItem : ItemUsageController 
{
    [SerializeField] public Transform point1;
    [SerializeField] public Transform point2;

    protected override void UseOverCharacter(Character character)
    {
        float characterPositionX = character.transform.position.x;
        float distance1 = Mathf.Abs(point1.position.x - characterPositionX);
        float distance2 = Mathf.Abs(point2.position.x - characterPositionX);

        if (distance1 > distance2)
        {
            character.Move(point1.position.x);
        }
        else
        {
            character.Move(point2.position.x);
        }
    }
}
