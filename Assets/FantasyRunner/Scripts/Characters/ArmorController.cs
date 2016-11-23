using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorController : AccesoryController 
{
    [SerializeField] private SkinnedMeshRenderer _armor;

    private Material _regularMaterial;

    private void Awake()
    {
        this._regularMaterial = this._armor.material;
    }

    public override void RemoveAccesory()
    {
        base.RemoveAccesory();
        this._armor.material = this._regularMaterial;
    }

    public override void AddAccesory(ItemUsageController accesoryItem, CharacterController character)
    {
        base.AddAccesory(accesoryItem, character);
        ArmorItem armorItem = accesoryItem as ArmorItem;
        this._armor.material = armorItem.ArmorMaterial;
    }
}
