using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : AccesoryController 
{
    [SerializeField] private GameObject firstWeapon;
    [SerializeField] private SkinnedMeshRenderer secondWeapon;
    [SerializeField] private List<WeaponContainer> containers;

    public override void RemoveAccesory()
    {
        base.RemoveAccesory();
        firstWeapon.SetActive(true);
        secondWeapon.gameObject.SetActive(false);
    }

    public override void AddAccesory(ItemUsageController accesoryItem, CharacterController character)
    {
        base.AddAccesory(accesoryItem, character);

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
