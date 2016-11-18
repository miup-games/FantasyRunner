using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStoreController : MonoBehaviour 
{
    [SerializeField] private DragController _dragController;
    [SerializeField] private DropController _dropController;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMesh _costText;
    [SerializeField] private GameObject _disabledObject;

    public Action<ItemStoreController> OnSelectItem;

    public GamerItem GamerItem { get; private set; }
    public bool Purchased { get; private set;}

    public Sprite Sprite
    {
        get
        {
            return this._spriteRenderer.sprite;
        }
    }

    private void Awake()
    {
        this._disabledObject.SetActive(false);
        this.Purchased = false;
        this._dragController.OnDrop += OnDrop;
    }

    private void OnMouseDown()
    {
        if (this.OnSelectItem != null)
        {
            this.OnSelectItem(this);
        }
    }

    private void OnDrop(DragController dragObject, DropController dropObject)
    {
        ItemStoreController otherItem = dropObject.GetComponent<ItemStoreController>();

        if (otherItem != null)
        {
            this.SwitchItems(otherItem);
            this._dragController.Return();
        }
    }

    private void SwitchItems(ItemStoreController otherItem)
    {
        AudioManager.instance.PlayFx("Item");
        GamerItem aux = otherItem.GamerItem;
        otherItem.SetItem(this.GamerItem);
        this.SetItem(aux);
    }

    public void Return()
    {
        this._dragController.Return();
    }

    public void SetItem(GamerItem item)
    {
        this.GamerItem = item;
        this._costText.text = "" + item.Item.Cost;
        this._spriteRenderer.sprite = Resources.Load<Sprite>(GamerItem.Item.IconName);

        this.UpdatePurchase(this.GamerItem.ItemId == ItemConstants.COIN_ID);
    }

    public void Purchase()
    {
        this.GamerItem.IsPurchased = true;
        this.UpdatePurchase(this.GamerItem.ItemId == ItemConstants.COIN_ID);
    }

    private void UpdatePurchase(bool isCoin)
    {
        this._disabledObject.SetActive(!this.GamerItem.IsPurchased);
        this._dragController.EnableDrag(!isCoin && this.GamerItem.IsPurchased);
        this._dropController.EnableDrop(!isCoin && this.GamerItem.IsPurchased);
    }
}
