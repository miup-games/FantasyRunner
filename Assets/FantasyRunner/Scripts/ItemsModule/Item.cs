using Newtonsoft.Json;
using System.Collections.Generic;

public class Items
{
    public Item[] ItemList { get; set; }
}

public class Item
{
    public int Id { get; set; }
    public string IconName { get; set; }
    public string PrefabName { get; set; }
    public float Delay { get; set; }
    public int Cost { get; set; }
    public int PurchaseCost { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsInitial { get; set; }
}

public class GamerItems
{
    public List<GamerItem> ItemList { get; set; }
}

public class GamerItem
{
    [JsonIgnore]
    public Item Item { get; set; }
    public int ItemId { get; set; }
    public bool IsPurchased { get; set; }
    public bool IsUsing { get; set; }

    public GamerItem(){}

    public GamerItem(Item item, bool useInitialSetting)
    {
        this.Item = item;
        this.ItemId = item.Id;
        this.IsPurchased = useInitialSetting ? item.IsInitial : false;
        this.IsUsing = useInitialSetting ? item.IsInitial : false;
    }
}