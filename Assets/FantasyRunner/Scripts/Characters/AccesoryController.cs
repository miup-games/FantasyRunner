using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AccesoryController : MonoBehaviour 
{
    protected abstract CharacterConstants.AttributeType AccesoryType { get; }
    protected abstract CharacterConstants.AttributeModifierType AccesoryModifierType { get; }
    protected abstract float BaseAccesoryValue { get; }

    private Buff _currentBuff;
    private BuffManager _buffManager;
    private Coroutine removeAccesoryCoroutine;

    private void StopRemoveWeaponCoroutine()
    {
        if (this.removeAccesoryCoroutine != null)
        {
            StopCoroutine(this.removeAccesoryCoroutine);
            this.removeAccesoryCoroutine = null;
        }
    }

    private IEnumerator RemoveAccesoryAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        this.DetachAccesory(true);
    }

    private void ModifyBuff(float accesoryValue)
    {
        this._currentBuff.ModifyEffectValue(this.AccesoryType, accesoryValue);
        this._buffManager.RefreshBuffs(this._currentBuff);
    }

    private void DetachAccesory(bool backToRegular)
    {
        if (backToRegular)
        {
            this.RemoveAccesory();
        }

        this.ModifyBuff(this.BaseAccesoryValue);
    }

    protected abstract void RemoveAccesory();
    protected abstract void AddAccesory(AccesoryItem accesoryItem);

    public void Initialize(BuffManager buffManager)
    {
        this._buffManager = buffManager;

        this._currentBuff = new Buff();
        this._currentBuff.AddEffect(this.AccesoryType, this.BaseAccesoryValue, this.AccesoryModifierType);
        this._buffManager.AddBuff(this._currentBuff);
    }

    public void AttachAccesory(AccesoryItem accesoryItem)
    {
        this.StopRemoveWeaponCoroutine();

        this.DetachAccesory(false);

        this.AddAccesory(accesoryItem);

        this.ModifyBuff(accesoryItem.AccesoryValue);
        this.removeAccesoryCoroutine = StartCoroutine(RemoveAccesoryAfterDelay(accesoryItem.AccesoryDuration));
    }
}
