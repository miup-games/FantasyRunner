using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreCurrentItemsController : MonoBehaviour 
{
    [SerializeField] private List<ItemStoreController> _itemController;
    [SerializeField] private StoreOptionController _storeOptionController;

    public List<ItemStoreController> ItemControllers
    {
        get
        {
            return this._itemController;
        }
    }

    public void Initialize(List<GamerItem> currentItems)
    {
        for (int i = 0; i < currentItems.Count; i++)
        {
            this._itemController[i].SetItem(currentItems[i]);
            this._itemController[i].OnSwitchItem += this.OnSwitchItem;
            this._itemController[i].OnSelectItem += this.OnSelectItem;
        }
    }

    private void OnSelectItem(ItemStoreController item)
    {
        this._storeOptionController.SetItem(item);
    }

    private void OnSwitchItem(ItemStoreController prevItem, ItemStoreController newItem)
    {
        int newItemIndex = this._itemController.IndexOf(newItem);
        int prevItemIndex = this._itemController.IndexOf(prevItem);

        if (newItemIndex != -1)
        {
            this._itemController[newItemIndex] = prevItem;
        }

        this._itemController[prevItemIndex] = newItem;
    }
}
