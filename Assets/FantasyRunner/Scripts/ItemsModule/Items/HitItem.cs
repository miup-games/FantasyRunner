using System;
using System.Collections;
using UnityEngine;

public class HitItem : ItemUsageController 
{
    [SerializeField] public float attack = 0f;

    protected override void UseOverCharacter(CharacterController character)
    {
        if (attack == 0)
        {
            character.FullHit();    
        }
        else
        {
            character.Hit(attack);
        }
    }
}
