using System;
using UnityEngine;

public class RecycleItemDropController : ItemDropController 
{
    public override void ProccesItem(ItemUIController item)
    {
        this.itemsController.RecycleItem(item);
    }
}
