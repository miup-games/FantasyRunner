using System;
using UnityEngine;

public class UseItemDropController : ItemDropController 
{
    public override void ProccesItem(ItemUIController item)
    {
        this.itemsController.UseItem(item);
    }
}
