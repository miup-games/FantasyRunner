using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour 
{
    [SerializeField] private GameObject firstWeapon;
    [SerializeField] private SkinnedMeshRenderer secondWeapon;
    [SerializeField] private List<WeaponContainer> containers;

    public void AttachWeapon(WeaponItem weaponItem)
    {
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

    public void DetachWeapon()
    {
        firstWeapon.SetActive(true);
        secondWeapon.gameObject.SetActive(false);
    }
}
