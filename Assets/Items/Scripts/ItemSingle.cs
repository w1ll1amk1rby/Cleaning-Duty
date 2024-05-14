using UnityEngine;
/**
    describes an item that can't have child items
*/
public class ItemSingle : Item
{
    public override bool CanAddChildItem(Item item)
    {
        return false;
    }
    public override bool CanHaveChildItems()
    {
        return false;
    }
    public override bool CanPickupItem()
    {
        return true;
    }
    public override bool CanRemoveChildItem()
    {
        return false;
    }
    public override void InsertChildItem(Item item)
    {
        Object.Destroy(item.gameObject);
    }
    public override Item RemoveAndReturnChildItem(Item item)
    {
        return item;
    }
    public override Item RemoveNextChildItem()
    {
        return null;
    }
}