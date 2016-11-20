using System;
using UnityEngine;
using System.Collections.Generic;

public class SpeedItem : ItemUsageController 
{
    [SerializeField] public float speedFactor = 2f;
    [SerializeField] public float time = 1f;

    protected override void UseOverCharacter(CharacterController character)
    {
        Buff buff = new Buff(time);
        buff.AddEffect(CharacterConstants.AttributeType.Speed, speedFactor, CharacterConstants.AttributeModifierType.Additive);
        character.AddBuff(buff);
    }
}
