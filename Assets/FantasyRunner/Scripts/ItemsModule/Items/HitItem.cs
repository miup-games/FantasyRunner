using System;
using System.Collections;
using UnityEngine;

public class HitItem : ItemUsageController 
{
    private Hit _hit;

    public override void Initialize(Item item)
    {
        base.Initialize(item);
        this._hit = item.GetItemUsage<Hit>();
    }

    protected override void UseOverCharacter(CharacterController character)
    {
        base.UseOverCharacter(character);
        if (this._hit.HitPoints == 0)
        {
            character.FullHit();    
        }
        else
        {
            character.Hit(this._hit.HitPoints);
        }
    }
}
