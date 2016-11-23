using System;
using UnityEngine;

public class WeaponItem : ItemUsageController 
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

    protected override void AddBuffToCharacter(CharacterController character)
    {
        base.AddBuffToCharacter(character);
        character.AddWeapon(this);
        this._itemsController.AddWeapon(this);
    }

    protected override void RemoveBuffFromCharacter(CharacterController character)
    {
        base.RemoveBuffFromCharacter(character);
        character.RemoveWeapon();
    }
}
