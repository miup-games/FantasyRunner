using Newtonsoft.Json;
using System.Collections.Generic;

public class Items
{
    public Item[] ItemList;
}

public class Item
{
    public int Id;
    public string IconName;
    public string PrefabName;
    public float Delay;
    public int Cost;
    public int PurchaseCost;
    public string Name;
    public string Description;
    public bool IsInitial;
}

public class GamerItems
{
    public List<GamerItem> ItemList;
}

public class GamerItem
{
    [JsonIgnore]
    public Item Item;
    public int ItemId;
    public bool IsPurchased;
    public bool IsUsing;

    public GamerItem(){}

    public GamerItem(Item item, bool useInitialSetting)
    {
        this.Item = item;
        this.ItemId = item.Id;
        this.IsPurchased = useInitialSetting ? item.IsInitial : false;
        this.IsUsing = useInitialSetting ? item.IsInitial : false;
    }
}