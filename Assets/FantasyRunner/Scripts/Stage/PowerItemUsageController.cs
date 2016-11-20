using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItemUsageController : MonoBehaviour 
{
    [SerializeField] private PowerItemOverlayController _powerItemOverlayController;
    [SerializeField] private ShakeController _cameraShakeController;

    private Coroutine _powerCoroutine;
    private CharacterController _playerCharacter;
    private System.Action _doneCb;
    private Buff _currentSpeedBuff;

    public void Initialize(CharacterController playerCharacter)
    {
        this._playerCharacter = playerCharacter;
        this.CreateSpeedBuff();
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

    private void CreateSpeedBuff()
    {
        this._currentSpeedBuff = new Buff();
        this._currentSpeedBuff.AddEffect(CharacterConstants.AttributeType.Speed, ItemConstants.POWER_PLAYER_SPEED_FACTOR, CharacterConstants.AttributeModifierType.Multiply);
    }

    private void StartEffect()
    {
        this._playerCharacter.EnablePowerItem(true);
        this._playerCharacter.AddBuff(this._currentSpeedBuff);
        AudioManager.instance.CurrentMusic.pitch = ItemConstants.POWER_MUSIC_SPEED;
        Time.timeScale = ItemConstants.POWER_GAME_SPEED;

        this._cameraShakeController.StartShake(ItemConstants.POWER_DURATION * ItemConstants.POWER_GAME_SPEED, true);
    }

    private void StopEffect()
    {
        AudioManager.instance.CurrentMusic.pitch = 1f;
        this._playerCharacter.EnablePowerItem(false);
        this._playerCharacter.RemoveBuff(this._currentSpeedBuff);
        Time.timeScale = 1f;

        this._cameraShakeController.StopShake();
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
