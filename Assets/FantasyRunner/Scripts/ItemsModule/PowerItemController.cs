using System;
using System.Collections;
using UnityEngine;

public class PowerItemController : MonoBehaviour 
{
    [SerializeField] private SpriteRenderer _icon;

    public Item Item { get; private set; }

    public void Initialize(Item item)
    {
        this.Item = item;
        this._icon.sprite = Resources.Load<Sprite>(this.Item.IconName);
    }

    public void Use()
    {
        Destroy(this.gameObject);
    }
}
