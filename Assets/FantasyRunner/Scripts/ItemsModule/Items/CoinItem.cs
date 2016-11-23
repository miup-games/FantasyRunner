using System;
using UnityEngine;

public class CoinItem : ItemUsageController 
{
    private Coin _coin;

    public override void Initialize(Item item)
    {
        base.Initialize(item);
        this._coin = item.GetItemUsage<Coin>();
    }

    protected override void UseOverCharacter(CharacterController character)
    {
        base.UseOverCharacter(character);
        this._itemsController.AddCoins(this._coin.Coins, transform.position);
    }
}
