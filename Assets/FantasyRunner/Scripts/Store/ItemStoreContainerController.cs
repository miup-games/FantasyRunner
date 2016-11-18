using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStoreContainerController : MonoBehaviour 
{
    [SerializeField] private GridController _gridController;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private StoreOptionController _storeOptionController;

    public List<ItemStoreController> ItemControllers { get; private set; }

    public void Initialize(List<GamerItem> currentItems)
    {
        this.ItemControllers = new List<ItemStoreController>();

        for(int i = 0; i < currentItems.Count; i++)
        {
            this.ItemControllers.Add(this.CreateItem(currentItems[i]));
        }
    }

    private ItemStoreController CreateItem(GamerItem item)
    {
        GameObject itemObject = Instantiate(this._itemPrefab, this._gridController.GetNextPosition(), Quaternion.identity, this._gridController.transform) as GameObject;
        ItemStoreController itemStoreController = itemObject.GetComponent<ItemStoreController>();
        itemStoreController.SetItem(item);
        itemStoreController.OnSelectItem += this.OnSelectItem;

        return itemStoreController;
    }

    private void OnSelectItem(ItemStoreController item)
    {
        this._storeOptionController.SetItem(item);
    }
}
