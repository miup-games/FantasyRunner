using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PowerItemOverlayController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _appearDuration;
    [SerializeField] private float _stayDuration;
    [SerializeField] private ShakeController _shakeCOntroller;

    private const float TIME_SCALE = 0.0001f;
    private float _totalDuration;

    private void Awake()
    {
        this.transform.localScale = Vector3.zero;
        this._totalDuration = this._appearDuration * 2f + this._stayDuration;
    }

    public IEnumerator StartEffect()
    {
        Time.timeScale = TIME_SCALE;
        this._audioSource.Play();

        yield return 0;

        this._shakeCOntroller.StartShake(this._totalDuration * TIME_SCALE, true);

        yield return this.transform.DOScale(1f, this._appearDuration * TIME_SCALE).SetEase(Ease.OutBack).WaitForCompletion();
        yield return new WaitForSeconds(this._stayDuration * TIME_SCALE);
        yield return this.transform.DOScale(0f, this._appearDuration * TIME_SCALE).SetEase(Ease.InBack).WaitForCompletion();
    }
}
