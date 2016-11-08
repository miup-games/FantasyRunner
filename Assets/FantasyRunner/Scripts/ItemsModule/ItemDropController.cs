using System;
using UnityEngine;

public abstract class ItemDropController : MonoBehaviour
{
    [SerializeField] protected ItemsBaseController itemsController;

    public abstract void ProccesItem(ItemUIController item);
}
