using System;
using UnityEngine;

public class HpItem : ItemUsageController 
{
    private Hp _hp;

    public override void Initialize(Item item)
    {
        base.Initialize(item);
        this._hp = item.GetItemUsage<Hp>();
    }

    protected override void UseOverCharacter(CharacterController character)
    {
        base.UseOverCharacter(character);
        if (this._hp.HpPoints == 0)
        {
            character.FullHeal();    
        }
        else
        {
            character.Heal(this._hp.HpPoints);
        }
    }
}
