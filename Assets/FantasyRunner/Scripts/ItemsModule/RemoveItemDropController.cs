using System;
using UnityEngine;

public class RemoveItemDropController : ItemDropController 
{
    public override void ProccesItem(ItemUIController item)
    {
        this.itemsController.RemoveItem(item);
    }
}
