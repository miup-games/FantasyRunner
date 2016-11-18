using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorController : AccesoryController 
{
    [SerializeField] private SkinnedMeshRenderer _armor;

    private Material _regularMaterial;

    protected override CharacterConstants.AttributeType AccesoryType
    { 
        get { return CharacterConstants.AttributeType.Defense; }
    }

    private void Awake()
    {
        this._regularMaterial = this._armor.material;
    }

    protected override void RemoveAccesory()
    {
        this._armor.material = this._regularMaterial;
    }

    protected override void AddAccesory(AccesoryItem accesoryItem)
    {
        ArmorItem armorItem = accesoryItem as ArmorItem;
        this._armor.material = armorItem.ArmorMaterial;
    }
}
