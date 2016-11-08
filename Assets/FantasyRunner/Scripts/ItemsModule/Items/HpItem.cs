using System;
using UnityEngine;

public class HpItem : ItemUsageController 
{
    [SerializeField] public float hp = 0f;

    protected override void UseOverCharacter(Character character)
    {
        if (hp == 0)
        {
            character.FullHeal();    
        }
        else
        {
            character.Heal(hp);
        }
    }
}
