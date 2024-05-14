using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
/**
    the item class is applied to game objects that declares them as an item.
*/
public abstract class Item : MonoBehaviour
{
    public enum ID
    {
        ClothesBasket = 0,
        DirtyUnderWear = 1,
        DirtyPlate = 2,

        FryingPan = 3,
        Plate = 6,
        Underwear = 7

    }
    public enum Tag
    {
        Laundry = 0,
        DishWashable = 1,
        Food = 2,
    }
    [SerializeField] private ID id;
    [SerializeField] private Tag[] tags;
    private ItemOwner itemOwner;

    public ItemOwner GetItemOwner()
    {
        return this.itemOwner;
    }
    public void SetItemOwner(ItemOwner newOwner)
    {
        ItemOwner oldOwner = this.itemOwner;
        if (oldOwner == newOwner)
        {

        }
        else
        {
            this.itemOwner = null;
            if (oldOwner != null && oldOwner.HasItem(this))
            {
                oldOwner.RemoveAndReturnItem(this);
            }
            this.itemOwner = newOwner;
            if (newOwner != null && !newOwner.HasItem(this))
            {
                newOwner.InsertItem(this);
            }
        }
    }
    public bool IsItemOwner(ItemOwner owner)
    {
        if(owner == null) {
            return false;
        }
        return owner == this.itemOwner;
    }
    public ID GetItemID()
    {
        return this.id;
    }
    public Tag[] GetItemTags()
    {
        return this.tags;
    }
    public bool DoesItemContainTag(Tag tag)
    {
        return this.tags.Contains(tag);
    }
    public bool MatchesId(ID id)
    {
        return this.id == id;
    }
    public abstract bool CanPickupItem();
    public abstract bool CanAddChildItem(Item item);
    public abstract void InsertChildItem(Item item);
    public abstract Item RemoveAndReturnChildItem(Item item);
    public abstract Item RemoveNextChildItem();
    public abstract bool CanRemoveChildItem();
    public abstract bool CanHaveChildItems();
}