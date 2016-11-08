using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItemUsageController : MonoBehaviour 
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private PowerItemOverlayController _powerItemOverlayController;

    private Coroutine _powerCoroutine;
    private Character _playerCharacter;
    private System.Action _doneCb;

    public void Initialize(Character playerCharacter)
    {
        this._playerCharacter = playerCharacter;
    }

    public void StartPower(System.Action doneCb)
    {
        this._doneCb = doneCb;
        this.StopPowerCoroutine();
        this._powerCoroutine = StartCoroutine(StartPowerCoroutine());
    }

    public void StopPower()
    {
        if(this.StopPowerCoroutine())
        {
            this.StopEffect();
        }
    }

    private void StartEffect()
    {
        this._playerCharacter.EnablePowerItem(true);
        this._playerCharacter.SetSpeedFactor(ItemConstants.POWER_PLAYER_SPEED_FACTOR);
        this._musicSource.pitch = ItemConstants.POWER_MUSIC_SPEED;
        Time.timeScale = ItemConstants.POWER_GAME_SPEED;
    }

    private void StopEffect()
    {
        this._musicSource.pitch = 1f;
        this._playerCharacter.EnablePowerItem(false);
        this._playerCharacter.SetSpeedFactor(1f);
        Time.timeScale = 1f;

        this._doneCb();
    }

    private bool StopPowerCoroutine()
    {
        if (this._powerCoroutine != null)
        {
            StopCoroutine(this._powerCoroutine);
            this._powerCoroutine = null;

            return true;
        }

        return false;
    }

    private IEnumerator StartPowerCoroutine()
    {
        yield return this._powerItemOverlayController.StartEffect();
        this.StartEffect();
        yield return new WaitForSeconds(ItemConstants.POWER_DURATION * ItemConstants.POWER_GAME_SPEED);
        this.StopEffect();
        this._powerCoroutine = null;
    }
}
