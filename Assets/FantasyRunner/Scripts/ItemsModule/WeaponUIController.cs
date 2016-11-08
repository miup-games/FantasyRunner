using System;
using System.Collections;
using UnityEngine;

public class WeaponUIController : MonoBehaviour 
{
    [SerializeField] private SpriteRenderer _icon;
    [SerializeField] private ProgressBarController _durationBar;

    private Coroutine _setProgressBarCoroutine;
    private Sprite _regularWeaponSprite;

    public void Awake()
    {
        this._regularWeaponSprite = this._icon.sprite;
    }

    public void SetWeapon(WeaponItem weaponItem)
    {
        StopSetProgressBarCoroutine();
        this._setProgressBarCoroutine = StartCoroutine(SetProgressBarCoroutine(weaponItem));
    }

    private void SetRegularWeapon()
    {
        this._icon.sprite = this._regularWeaponSprite;
        this._durationBar.SetValue(1f);
    }

    private void StopSetProgressBarCoroutine()
    {
        if (this._setProgressBarCoroutine != null)
        {
            StopCoroutine(this._setProgressBarCoroutine);
            this._setProgressBarCoroutine = null;
        }
    }

    private IEnumerator SetProgressBarCoroutine(WeaponItem weaponItem)
    {
        this._icon.sprite = weaponItem.IconSprite;
        this._durationBar.SetValue(1f);
        float time = weaponItem.WeaponDuration;
        while (true)
        {
            if (time <= 0)
            {
                break;
            }

            time = Mathf.Max(0, time - Time.deltaTime);
            this._durationBar.SetValue(time / weaponItem.WeaponDuration);

            yield return 0;
        }

        this.SetRegularWeapon();
    }
}
