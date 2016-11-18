using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDeckController : ItemsBaseController 
{
    private List<Item> _allItems;
    private List<Item> _items;
    private Dictionary<Transform, ItemUIController> _itemControllers;

    protected override void Awake()
    {
        base.Awake();
        this.SetUpSlots();
        this.DrawNextItem();
    }

    private void SetUpSlots()
    {
        this._allItems = new List<Item>(this._allCurrentItems);

        this._itemControllers = new Dictionary<Transform, ItemUIController>();

        for(int i = 0; i < this._itemSlots.Length; i++)
        {
            this._itemControllers[this._itemSlots[i]] = null;
        }
    }

    public override void UseItem(ItemUIController itemController)
    {
        if (itemController.CanUse)
        {
            this._itemControllers[itemController.Slot] = null;
            itemController.Use(this, this._gameManager);
            this.DrawNextItem();
        }
        else
        {
            itemController.Return();
        }
    }

    public override void RecycleItem(ItemUIController itemController)
    {
        this._itemControllers[itemController.Slot] = null;
        this._items.Add(itemController.Item);
        itemController.Discard();
        this.DrawNextItem();
    }

    public override void RemoveItem(ItemUIController itemController)
    {
        this._allItems.Remove(itemController.Item);
        this._itemControllers[itemController.Slot] = null;
        itemController.Discard();
        this.DrawNextItem();
    }

    private void ResetItems()
    {
        this._items = new List<Item>(this._allCurrentItems);
    }

    private void DrawNextItem()
    {
        if (this._allItems.Count > 0)
        {
            for(int i = 0; i < this._itemSlots.Length; i++)
            {
                if (this._itemControllers[this._itemSlots[i]] == null)
                {
                    if (this._items == null || this._items.Count == 0)
                    {
                        this._items = new List<Item>(this._allItems);
                    }

                    Item item = this._items[UnityEngine.Random.Range(0, this._items.Count)];
                    this._items.Remove(item);

                    ItemUIController itemController = (Instantiate(this._itemPrefab, this._itemSlots[i].position, Quaternion.identity) as GameObject).GetComponent<ItemUIController>();
                    this._itemControllers[this._itemSlots[i]] = itemController;
                    itemController.SetItem(item, this._itemSlots[i]);
                }
            }
        }

        this._infoText.text = this._items.Count + "/" + this._allItems.Count;
    }
}
