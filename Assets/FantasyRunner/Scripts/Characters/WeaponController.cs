using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : AccesoryController 
{
    [SerializeField] private GameObject firstWeapon;
    [SerializeField] private SkinnedMeshRenderer secondWeapon;
    [SerializeField] private List<WeaponContainer> containers;

    protected override CharacterConstants.AttributeType AccesoryType
    { 
        get { return CharacterConstants.AttributeType.Attack; }
    }

    protected override CharacterConstants.AttributeModifierType AccesoryModifierType
    { 
        get { return CharacterConstants.AttributeModifierType.Additive; }
    }
    protected override float BaseAccesoryValue
    { 
        get { return 0; }
    }

    protected override void RemoveAccesory()
    {
        firstWeapon.SetActive(true);
        secondWeapon.gameObject.SetActive(false);
    }

    protected override void AddAccesory(AccesoryItem accesoryItem)
    {
        WeaponItem weaponItem = accesoryItem as WeaponItem;

        firstWeapon.SetActive(false);
        secondWeapon.gameObject.SetActive(true);

        for(int i = 0; i < this.containers.Count; i++)
        {
            if (this.containers[i].WeaponType == weaponItem.WeaponType)
            {
                secondWeapon.sharedMesh = weaponItem.WeaponMesh;
                secondWeapon.material = weaponItem.WeaponMaterial;
                secondWeapon.rootBone = this.containers[i].transform;
                break;
            }
        }
    }
}
