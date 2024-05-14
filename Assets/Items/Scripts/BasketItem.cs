using System.Linq;
using UnityEngine;
[RequireComponent(typeof(ItemHolder))]
/**
    a basket item describes an item that can hold child items. 
    has a lid that determines if items can be added or if it can be picked up.
*/
public class BasketItem : Item
{
    [SerializeField] private GameObject lid;
    [SerializeField] private Item.Tag[] allowedTags;
    private ItemHolder itemHolder;
    private bool closedContainer;
    public void Awake()
    {
        this.itemHolder = this.GetComponent<ItemHolder>();
        this.SetClosedContainer(true);
    }
    public override bool CanAddChildItem(Item item)
    {
        if (this.closedContainer || item == null)
        {
            return false;
        }
        else if (!this.ItemPassedTagCheck(item))
        {
            return false;
        }
        return this.itemHolder.CanInsertItem();
    }
    public override bool CanHaveChildItems()
    {
        return true;
    }
    public override bool CanPickupItem()
    {
        return this.closedContainer;
    }
    public override bool CanRemoveChildItem()
    {
        if (this.closedContainer)
        {
            return false;
        }
        return this.itemHolder.HasItem();
    }
    public override void InsertChildItem(Item item)
    {
        if(!this.ItemPassedTagCheck(item)) {
            Object.Destroy(item.gameObject);
            return;
        }
        this.itemHolder.InsertItem(item);
    }
    public override Item RemoveAndReturnChildItem(Item item)
    {
        if (item == null)
        {
            return item;
        }
        return this.itemHolder.RemoveAndReturnItem(item);
    }
    public override Item RemoveNextChildItem()
    {
        return this.itemHolder.RemoveAndReturnNextItem();
    }
    public void ToggleOpen()
    {
        this.SetClosedContainer(!this.closedContainer);
    }
    private void SetClosedContainer(bool value)
    {
        this.closedContainer = value;
        this.lid.SetActive(value);

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