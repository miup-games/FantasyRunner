using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : MonoBehaviour 
{
    [SerializeField] private ItemConstants.WeaponType weaponType;

    public ItemConstants.WeaponType WeaponType
    {
        get
        {
            return this.weaponType;
        }
    }
}
