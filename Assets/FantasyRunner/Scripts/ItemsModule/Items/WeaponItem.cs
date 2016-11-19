using System;
using UnityEngine;

public class WeaponItem : AccesoryItem 
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private MeshRenderer meshRender;
    [SerializeField] private ItemConstants.WeaponType weaponType;
    [SerializeField] private float _weaponValue = 4f;
    [SerializeField] private float _stageSpeedValue = 0.9f;

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

    protected override void AddAccesoryToCharacter()
    {
        this._character.AddWeapon(this);
        this._itemsController.AddWeapon(this);
    }

    protected override void SetBuff()
    {
        this.Buff.AddEffect(CharacterConstants.AttributeType.Attack, this._weaponValue, CharacterConstants.AttributeModifierType.Additive);
        this.Buff.AddEffect(CharacterConstants.AttributeType.BattleStageSpeed, this._stageSpeedValue, CharacterConstants.AttributeModifierType.Multiply);
    }

    protected override void RemoveAccesoryFromCharacter()
    {
        this._character.RemoveWeapon();
    }
}
