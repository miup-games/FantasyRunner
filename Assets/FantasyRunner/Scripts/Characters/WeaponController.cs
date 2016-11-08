using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour 
{
    [SerializeField] private GameObject firstWeapon;
    [SerializeField] private SkinnedMeshRenderer secondWeapon;
    [SerializeField] private List<WeaponContainer> containers;

    private Buff _currentBuff;
    private BuffManager _buffManager;
    private Coroutine removeWeaponCoroutine;

    private void StopRemoveWeaponCoroutine()
    {
        if (this.removeWeaponCoroutine != null)
        {
            StopCoroutine(this.removeWeaponCoroutine);
            this.removeWeaponCoroutine = null;
        }
    }

    private IEnumerator RemoveWeaponAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        this.DetachWeapon(true);
    }

    private void ModifyBuff(float attack)
    {
        this._currentBuff.ModifyEffectValue(CharacterConstants.AttributeType.Attack, attack);
        this._buffManager.UpdateBuffs();
    }

    private void DetachWeapon(bool backToRegular)
    {
        if (backToRegular)
        {
            firstWeapon.SetActive(true);
            secondWeapon.gameObject.SetActive(false);
        }

        this.ModifyBuff(0);
    }

    public void Initialize(BuffManager buffManager)
    {
        this._buffManager = buffManager;

        this._currentBuff = new Buff();
        this._currentBuff.AddEffect(CharacterConstants.AttributeType.Attack, 0, CharacterConstants.AttributeModifierType.Additive);
        this._buffManager.AddBuff(this._currentBuff);
    }

    public void AttachWeapon(WeaponItem weaponItem)
    {
        this.StopRemoveWeaponCoroutine();

        this.DetachWeapon(false);

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

        this.ModifyBuff(weaponItem.Attack);
        this.removeWeaponCoroutine = StartCoroutine(RemoveWeaponAfterDelay(weaponItem.WeaponDuration));
    }
}
