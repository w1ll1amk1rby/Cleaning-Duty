using UnityEngine;
using System.Linq;
[RequireComponent(typeof(ItemHolder))]
/**
    item depositer that places items in an item holder if there is room
*/
public class ItemDepositerHolder : ItemDepositer
{
    private ItemHolder itemHolder;
    [SerializeField] private Item.Tag[] allowedTags;
    public void Start()
    {
        this.itemHolder = this.GetComponent<ItemHolder>();
    }
    public override bool CanDeposit(Item item)
    {
        Item storedItem = this.itemHolder.GetNextItem();
        if (item == null || !this.ItemPassedTagCheck(item))
        {
            return false;
        }
        else if (this.itemHolder.CanInsertItem())
        {
            return true;
        }
        else if (storedItem && !this.itemHolder.CanStoreMultipleItems())
        {
            return storedItem.CanHaveChildItems() && storedItem.CanAddChildItem(item);
        }
        else
        {
            return false;
        }
    }
    public override void Deposit(Item item)
    {
        Item storedItem = this.itemHolder.GetNextItem();
        if (item == null)
        {
            return;
        }
        if (this.itemHolder.CanInsertItem())
        {
            this.itemHolder.InsertItem(item);
        }
        else if (
            storedItem && !this.itemHolder.CanStoreMultipleItems() &&
            storedItem.CanHaveChildItems() &&
            storedItem.CanAddChildItem(item)
            )
        {
            storedItem.InsertChildItem(item);
        }
    }
    private bool ItemPassedTagCheck(Item item)
    {
        if (this.allowedTags.Length != 0)
        {
            return this.allowedTags.Any((itemTag) =>
            {
                return item.DoesItemContainTag(itemTag);
            });
        }
        else
        {
            return true;
        }
    }
}