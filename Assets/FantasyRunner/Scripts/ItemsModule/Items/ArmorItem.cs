using System;
using UnityEngine;

public class ArmorItem : AccesoryItem 
{
    [SerializeField] private Material _armorMaterial;
    [SerializeField] private float _armorValue = 0.9f;
    [SerializeField] private float _stageSpeedValue = 0.9f;

    public Material ArmorMaterial
    {
        get
        {
            return this._armorMaterial;
        }
    }

    protected override void AddAccesoryToCharacter()
    {
        this._character.AddArmor(this);
        this._itemsController.AddArmor(this);
    }

    protected override void SetBuff()
    {
        this.Buff.AddEffect(CharacterConstants.AttributeType.Defense, this._armorValue, CharacterConstants.AttributeModifierType.Multiply);
        this.Buff.AddEffect(CharacterConstants.AttributeType.BattleStageSpeed, this._stageSpeedValue, CharacterConstants.AttributeModifierType.Multiply);
    }

    protected override void RemoveAccesoryFromCharacter()
    {
        this._character.RemoveArmor();
    }
}
