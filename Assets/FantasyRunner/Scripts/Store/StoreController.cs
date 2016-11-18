using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour 
{
    [SerializeField] private ItemStoreContainerController _itemStoreContainerController;
    [SerializeField] private StoreCurrentItemsController _storeCurrentItemsController;

    private List<GamerItem> _currentItems;
    private List<GamerItem> _storeItems;
    private List<GamerItem> _allItems;

    private void Start()
    {
        this.SetItemLists();
        this._itemStoreContainerController.Initialize(this._storeItems);
        this._storeCurrentItemsController.Initialize(this._currentItems);
    }

    private void SetItemLists()
    {
        this._allItems = PlayerRepository.GetGamerItems();
        this._storeItems = new List<GamerItem>();
        this._currentItems = new List<GamerItem>();

        for(int i = 0; i < this._allItems.Count; i++)
        {
            if (this._allItems[i].IsUsing)
            {
                this._currentItems.Add(this._allItems[i]);
            }
            else
            {
                this._storeItems.Add(this._allItems[i]);
            }
        }
    }
}
