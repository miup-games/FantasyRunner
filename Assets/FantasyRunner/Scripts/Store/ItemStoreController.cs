using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStoreController : MonoBehaviour 
{
    [SerializeField] private DragController _dragController;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMesh _costText;
    [SerializeField] private GameObject _disabledObject;

    public Action<ItemStoreController, ItemStoreController> OnSwitchItem;
    public Action<ItemStoreController> OnSelectItem;

    public GamerItem Item { get; private set; }
    public bool Purchased { get; private set;}

    private void Awake()
    {
        this._disabledObject.SetActive(false);
        this.Purchased = false;
        this._dragController.OnDrop += OnDrop;
    }

    void OnMouseDown()
    {
        if (this.OnSelectItem != null)
        {
            this.OnSelectItem(this);
        }
    }

    private void OnDrop(DragController dragObject, DropController dropObject)
    {
        ItemDropController itemDropController = dropObject.GetComponent<ItemDropController>();

        if (itemDropController != null)
        {
            //itemDropController.ProccesItem(this);
        }
    }

    public void Return()
    {
        this._dragController.Return();
    }

    public void SetItem(GamerItem item)
    {
        this.Item = item;
        this._costText.text = "" + item.Item.Cost;
        this._spriteRenderer.sprite = Resources.Load<Sprite>(Item.Item.IconName);

        this.UpdatePurchase();

        if (item.ItemId == ItemConstants.COIN_ID)
        {
            this._dragController.EnableDrag(false);
        }
    }

    public void Purchase()
    {
        this.Item.IsPurchased = true;
        this.UpdatePurchase();
    }

    private void UpdatePurchase()
    {
        this._disabledObject.SetActive(!this.Item.IsPurchased);
        this._dragController.EnableDrag(this.Item.IsPurchased);
    }
}
