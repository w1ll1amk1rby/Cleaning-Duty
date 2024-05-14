using UnityEngine;
/**
*   a collection point for resources, creates a new item. can always collect from
*/
public class ResourceCollector : ItemCollecter
{
    [SerializeField] private Item.ID itemId;
    [SerializeField] private ItemFactory itemFactory;
    public override bool CanCollect()
    {
        return true;
    }

    public override Item Collect()
    {
        return this.itemFactory.CreateItem(this.itemId);
    }
}
