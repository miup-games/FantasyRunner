using Newtonsoft.Json;
using System.Collections.Generic;

public class Items
{
    public Item[] ItemList { get; set; }
}

public class ItemEffect
{
    public CharacterConstants.AttributeType AttributeType { get; set; }
    public CharacterConstants.AttributeModifierType AttributeModifierType { get; set; }
    public float AttributeModifierValue { get; set; }
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
    public float Duration { get; set; }
    public float EffectDuration { get; set; }
    public CharacterConstants.CharacterType CharacterType { get; set; }
    public bool DestroyAfterUse { get; set; }
    public ItemEffect[] Buff { get; set; }
    public System.Object ItemUsage { get; set; }
    public T GetItemUsage<T>()
    {
        string objectStr = JsonConvert.SerializeObject(this.ItemUsage);
        return JsonConvert.DeserializeObject<T>(objectStr);
    }
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