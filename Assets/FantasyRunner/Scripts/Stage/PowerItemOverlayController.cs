using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PowerItemOverlayController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _appearDuration;
    [SerializeField] private float _stayDuration;
    [SerializeField] private float left;
    [SerializeField] private float right;
    [SerializeField] private float center = 0;

    private const float TIME_SCALE = 0.0001f;
    private Vector3 _leftPosition;
    private Vector3 _rightPosition;
    private Vector3 _centerPosition;

    private void Awake()
    {
        Vector3 currentPosition = this.transform.localPosition;
        this._leftPosition = new Vector3(left, currentPosition.y, currentPosition.z);
        this._rightPosition = new Vector3(right, currentPosition.y, currentPosition.z);
        this._centerPosition = new Vector3(center, currentPosition.y, currentPosition.z);
    }

    public IEnumerator StartEffect()
    {
        Time.timeScale = TIME_SCALE;
        this._audioSource.Play();

        this.transform.localPosition = this._leftPosition;
        yield return 0;
        yield return this.transform.DOLocalMoveX(this._centerPosition.x, this._appearDuration * TIME_SCALE).WaitForCompletion();
        yield return new WaitForSeconds(this._stayDuration * TIME_SCALE);
        yield return this.transform.DOLocalMoveX(this._rightPosition.x, this._appearDuration * TIME_SCALE).WaitForCompletion();
    }
}
