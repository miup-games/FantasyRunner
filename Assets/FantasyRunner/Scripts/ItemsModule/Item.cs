
public class Item
{
    public int Id;
    public string IconName;
    public string PrefabName;
    public float Delay;
    public int Cost;

    public Item Clone()
    {
        return new Item
        {
            Id = this.Id,
            IconName = this.IconName,
            PrefabName = this.PrefabName,
            Delay = this.Delay,
            Cost = this.Cost
        };
    }
}
