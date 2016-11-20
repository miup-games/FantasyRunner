using System;
using UnityEngine;

public class CoinItem : ItemUsageController 
{
    [SerializeField] public int coins = 1;

    protected override void UseOverCharacter(CharacterController character)
    {
        this._itemsController.AddCoins(coins, transform.position);
    }
}
