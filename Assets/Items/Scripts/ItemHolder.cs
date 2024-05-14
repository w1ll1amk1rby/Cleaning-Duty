using UnityEngine;
/**
    describes an itemOwner with item anchors which states where the items should be placed when owned
*/
public class ItemHolder : ItemOwner
{
    [SerializeField] private GameObject[] itemAnchorPoints;
    private Item[] storedItems;
    [SerializeField] private GameStateManager playManager;
    public void Awake()
    {
        this.storedItems = new Item[this.itemAnchorPoints.Length];
    }
    public void Update()
    {
        if (this.playManager && !this.playManager.GetGameStateHandler().CanItemsExist())
        {
            while (this.HasItem())
            {
                this.DestroyNextItem();
            }
        }
    }
    public override bool CanInsertItem()
    {
        return this.FirstAvaliableIndex() != -1;
    }
    public override void InsertItem(Item item)
    {
        if (item == null)
        {
            return;
        }
        int index = this.FirstAvaliableIndex();
        if (index != -1)
        {
            if (this.HasItem(item))
            {
                return;
            }
            this.storedItems[index] = item;
            item.transform.SetParent(this.itemAnchorPoints[index].transform);
            item.transform.position = this.itemAnchorPoints[index].transform.position;
            if (!item.IsItemOwner(this))
            {
                item.SetItemOwner(this);
            }
        }
        else if (item)
        {
            Destroy(item.gameObject);
        }
    }
    public Item RemoveAndReturnNextItem()
    {
        int lastIndex = this.LastItemIndex();
        if (lastIndex != -1)
        {
            return this.RemoveAndReturnItem(this.storedItems[lastIndex]);
        }
        else
        {
            return null;
        }
    }
    public override Item RemoveAndReturnItem(Item item)
    {
        int index = this.GetItemIndex(item);
        if (index != -1 && index < this.storedItems.Length)
        {
            this.storedItems[index] = null;
            if (item != null)
            {
                item.transform.SetParent(null);
                if (item.IsItemOwner(this))
                {
                    item.SetItemOwner(null);
                }
                return item;
            }
        }
        return null;
    }
    public void DestroyNextItem()
    {
        Item item = this.RemoveAndReturnNextItem();
        this.DestroyItem(item);
    }
    public override void DestroyItem(Item item)
    {
        if (item == null)
        {
            return;
        }
        int index = this.GetItemIndex(item);
        if (index != -1)
        {
            this.RemoveAndReturnItem(item);
            UnityEngine.Object.Destroy(item.gameObject);
        }
    }
    public bool HasItem()
    {
        int lastIndex = this.LastItemIndex();
        return lastIndex != -1;
    }
    public override bool HasItem(Item item)
    {
        for (int i = 0; i < this.storedItems.Length; i++)
        {
            if (this.storedItems[i] == item)
            {
                return true;
            }
        }
        return false;
    }
    public Item[] GetItems()
    {
        return this.storedItems;
    }
    public Item GetNextItem()
    {
        int lastIndex = this.LastItemIndex();
        if (lastIndex != -1)
        {
            return this.storedItems[lastIndex];
        }
        return null;
    }
    public bool CanStoreMultipleItems()
    {
        return this.itemAnchorPoints.Length > 1;
    }
    private int LastItemIndex()
    {
        for (int i = this.storedItems.Length - 1; i >= 0; i--)
        {
            if (this.storedItems[i] != null)
            {
                return i;
            }
        }
        return -1;
    }
    private int FirstAvaliableIndex()
    {
        for (int i = 0; i < this.storedItems.Length; i++)
        {
            if (this.storedItems[i] == null)
            {
                return i;
            }
        }
        return -1; // no avaliable Index
    }
    private int GetItemIndex(Item item)
    {
        for (int i = 0; i < this.storedItems.Length; i++)
        {
            if (this.storedItems[i] == item)
            {
                return i;
            }
        }
        return -1; // Item not found
    }
}