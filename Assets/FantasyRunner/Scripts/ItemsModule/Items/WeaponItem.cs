using System;
using UnityEngine;

public class WeaponItem : AccesoryItem 
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private MeshRenderer meshRender;
    [SerializeField] private ItemConstants.WeaponType weaponType;

    public Mesh WeaponMesh
    {
        get
        {
            return this.mesh;
        }
    }

    public Material WeaponMaterial
    {
        get
        {
            return this.meshRender.material;
        }
    }

    public ItemConstants.WeaponType WeaponType
    {
        get
        {
            return this.weaponType;
        }
    }

    protected override void UseOverCharacter(Character character)
    {
        character.AddWeapon(this);
        this._itemsController.AddWeapon(this);
    }
}
