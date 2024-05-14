using UnityEngine;
[RequireComponent(typeof(ItemHolder))]
/**
*   this class is an item collecter that gets items from an itemHolder
*/
public class ItemCollecterHolder : ItemCollecter
{
    private ItemHolder itemHolder;
    [SerializeField] private GameStateManager playManager;
    public void Start()
    {
        this.itemHolder = this.GetComponent<ItemHolder>();
    }
    public override bool CanCollect()
    {
        Item item = this.itemHolder.GetNextItem();
        if (!this.playManager.GetGameStateHandler().CanItemsExist() || item == null)
        {
            return false;
        }
        else if(item.CanPickupItem()) 
        {
            return true;
        }
        else 
        {
            return item.CanHaveChildItems() && item.CanRemoveChildItem();
        }
    }
    public override Item Collect()
    {
        Item item = this.itemHolder.GetNextItem();
        if (!this.playManager.GetGameStateHandler().CanItemsExist() || item == null)
        {
            return null;
        }
        else if(item.CanPickupItem()) 
        {
            return this.itemHolder.RemoveAndReturnItem(item);
        } 
        else if(item.CanHaveChildItems() && item.CanRemoveChildItem()) 
        {
            return item.RemoveNextChildItem();
        }
        else 
        {
            return null;
        }
    }
}
