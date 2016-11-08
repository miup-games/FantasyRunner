using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPriceController : ItemsBaseController 
{
    [SerializeField] private LootAnimationController _coinsLootAnimationController;

    private List<Item> _allItems;
    private List<ItemUIController> _itemControllers;

    private void Awake()
    {
        this.SetUpSlots();
    }

    private void SetUpSlots()
    {
        this._allItems = new List<Item>(DataConstants.ITEMS);

        this._itemControllers = new List<ItemUIController>();

        for(int i = 0; i < this._allItems.Count; i++)
        {
            ItemUIController itemController = (Instantiate(this._itemPrefab, this._itemSlots[i].position, Quaternion.identity) as GameObject).GetComponent<ItemUIController>();
            this._itemControllers.Add(itemController);
            itemController.SetItem(this._allItems[i], this._itemSlots[i]);
        }

        this.AddCoins(0);
    }

    public override void AddCoins(int coins, Vector3 position)
    {
        base.AddCoins(coins, position);
        this._coinsLootAnimationController.AddLoot(position);
    }

    public override void AddCoins(int coins)
    {
        base.AddCoins(coins);

        for (int i = 0; i < this._itemControllers.Count; i++)
        {
            this._itemControllers[i].UpdateCoins(this._coins);
        }
    }

    public override void UseItem(ItemUIController itemController)
    {
        if (itemController.CanUse && itemController.Item.Cost <= this._coins)
        {
            this.AddCoins(-itemController.Item.Cost);
            itemController.Use(this, this._gameManager, false);
        }
        else
        {
            itemController.Return();
        }
    }
}