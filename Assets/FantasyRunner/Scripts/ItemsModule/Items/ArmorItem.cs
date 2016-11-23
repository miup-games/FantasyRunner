using System;
using UnityEngine;

public class ArmorItem : ItemUsageController 
{
    [SerializeField] private Material _armorMaterial;

    public Material ArmorMaterial
    {
        get
        {
            return this._armorMaterial;
        }
    }

    protected override void AddBuffToCharacter(CharacterController character)
    {
        base.AddBuffToCharacter(character);
        character.AddArmor(this);
        this._itemsController.AddArmor(this);
    }

    protected override void RemoveBuffFromCharacter(CharacterController character)
    {
        base.RemoveBuffFromCharacter(character);
        character.RemoveArmor();
    }
}
