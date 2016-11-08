using UnityEngine;
using System.Collections;
using DG.Tweening;

public class LootAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject _lootItemAnimationPrefab;
    [SerializeField] private Transform _lootDestination;

    private Transform _transform;
    private Vector3 _toPosition;

    private void Awake()
    {
        this._transform = transform;
        this._toPosition = _lootDestination.position;
    }

    public void AddLoot(Vector3 fromPosition)
    {
        LootItemAnimationController lootAnim = GameObjectPool.instance.GetObject<LootItemAnimationController>(this._lootItemAnimationPrefab, fromPosition, this._transform);
        lootAnim.GoToPosition(fromPosition, this._toPosition, () => {
            GameObjectPool.instance.ReturnObject(lootAnim.gameObject);
        });
    }
}
