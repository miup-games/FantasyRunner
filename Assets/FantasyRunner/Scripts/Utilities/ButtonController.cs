﻿using UnityEngine;

public abstract class ButtonController : MonoBehaviour 
{
    private Vector3 _regularScale;

    protected virtual void Awake()
    {
        this._regularScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        transform.localScale = this._regularScale * 0.9f;
    }

    private void OnMouseUp()
    {
        transform.localScale = this._regularScale;
        this.OnClick();
    }

    protected abstract void OnClick();
}
