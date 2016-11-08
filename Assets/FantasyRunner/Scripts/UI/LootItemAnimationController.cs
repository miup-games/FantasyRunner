using UnityEngine;
using System.Collections;
using DG.Tweening;

public class LootItemAnimationController : MonoBehaviour
{
    [SerializeField] private float duration;

    private Transform _transform;

    private void Awake()
    {
        this._transform = this.transform;
    }

    public void GoToPosition(Vector3 fromPosition, Vector3 toPosition, System.Action done)
    {
        this._transform.position = fromPosition;
        this._transform.DOMove(toPosition, duration).OnComplete(() => done());
    }
}
