using UnityEngine;
using System.Collections;

public class ProgressBarController : MonoBehaviour
{
    [SerializeField] private Transform _bar;

    private float _localScaleY;

    private void Awake()
    {
        this._localScaleY = this._bar.localScale.y;
    }

    public void SetValue(float perc)
    {
        this._bar.localScale = new Vector2(perc, this._localScaleY);
    }
}
