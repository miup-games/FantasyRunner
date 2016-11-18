using System;
using UnityEngine;

public class ArmorItem : AccesoryItem 
{
    [SerializeField] private Material _armorMaterial;

    public Material ArmorMaterial
    {
        get
        {
            return this._armorMaterial;
        }
    }

    protected override void UseOverCharacter(Character character)
    {
        character.AddArmor(this);
        this._itemsController.AddArmor(this);
    }
}
