using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreOptionController : MonoBehaviour 
{
    [SerializeField] private TextMesh _title;
    [SerializeField] private TextMesh _description;
    [SerializeField] private TextMesh _cost;
    [SerializeField] private GameObject _itemOptionContainer;
    [SerializeField] private GameObject _purchaseContainer;

    private List<GamerItem> _currentItems;
    private List<GamerItem> _storeItems;
    private List<GamerItem> _allItems;

    private ItemStoreController _currentItem;

    private void Awake()
    {
        this._itemOptionContainer.SetActive(false);
    }

    public void SetItem(ItemStoreController currentItem)
    {
        this._itemOptionContainer.SetActive(true);
        this._currentItem = currentItem;  
        this._title.text = this._currentItem.GamerItem.Item.Name;
        this._description.text = this._currentItem.GamerItem.Item.Description;
        this._cost.text = "" + this._currentItem.GamerItem.Item.PurchaseCost;

        this._purchaseContainer.SetActive(!currentItem.GamerItem.IsPurchased);
    }

    public void PurchaseCurrentItem()
    {
        if(!this._currentItem.GamerItem.IsPurchased)
        {
            int currentCoins = PlayerRepository.GetCoins();
            int cost = this._currentItem.GamerItem.Item.PurchaseCost;

            if (currentCoins >= cost)
            {
                this._purchaseContainer.SetActive(false);
                PlayerRepository.AddCoins(-cost);
                this._currentItem.Purchase();

                AudioManager.instance.PlayFx("AddPower");
            }
        }
    }
}
