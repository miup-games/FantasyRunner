using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AccesoryController : MonoBehaviour 
{
    private Buff _currentBuff;
    private BuffManager _buffManager;

    public void Initialize(BuffManager buffManager)
    {
        this._buffManager = buffManager;
    }

    public virtual void RemoveAccesory()
    {
        this._currentBuff = null;
    }

    public virtual void AddAccesory(AccesoryItem accesoryItem)
    {
        if (this._currentBuff != null)
        {
            this._buffManager.RemoveBuff(this._currentBuff);
        }

        this._currentBuff = accesoryItem.Buff;
    }
}
