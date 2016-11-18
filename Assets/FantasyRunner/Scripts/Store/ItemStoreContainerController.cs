using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStoreContainerController : MonoBehaviour 
{
    [SerializeField] private GridController _gridController;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private StoreOptionController _storeOptionController;

    /*private void Start()
    {
        for(int i = 0; i < 25; i++)
        {
            this.CreateItem();
        }
    }*/

    public void Initialize(List<GamerItem> currentItems)
    {
        for(int i = 0; i < currentItems.Count; i++)
        {
            this.CreateItem(currentItems[i]);
        }
    }

    private void CreateItem(GamerItem item)
    {
        GameObject itemObject = Instantiate(this._itemPrefab, this._gridController.GetNextPosition(), Quaternion.identity, this._gridController.transform) as GameObject;
        ItemStoreController itemStoreController = itemObject.GetComponent<ItemStoreController>();
        itemStoreController.SetItem(item);
        itemStoreController.OnSelectItem += this.OnSelectItem;
    }

    private void OnSelectItem(ItemStoreController item)
    {
        this._storeOptionController.SetItem(item);
    }
}
