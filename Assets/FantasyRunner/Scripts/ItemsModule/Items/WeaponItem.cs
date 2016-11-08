using System;
using UnityEngine;

public class WeaponItem : ItemUsageController 
{
    [SerializeField] private float attack = 5f;
    [SerializeField] private float weaponDuration = 5f;
    [SerializeField] private Mesh mesh;
    [SerializeField] private MeshRenderer meshRender;
    [SerializeField] private ItemConstants.WeaponType weaponType;
    [SerializeField] private Sprite iconSprite;

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

    public float Attack
    {
        get
        {
            return this.attack;
        }
    }

    public float WeaponDuration
    {
        get
        {
            return this.weaponDuration;
        }
    }

    public ItemConstants.WeaponType WeaponType
    {
        get
        {
            return this.weaponType;
        }
    }

    public Sprite IconSprite
    {
        get
        {
            return this.iconSprite;
        }
    }

    protected override void UseOverCharacter(Character character)
    {
        character.AddWeapon(this);
        this._itemsController.AddWeapon(this);
    }
}
