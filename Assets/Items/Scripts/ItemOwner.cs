using UnityEngine;
/**
    describes an abstract item owner, something that can own an item
*/
public abstract class ItemOwner : MonoBehaviour
{
    public abstract bool HasItem(Item item);
    public abstract Item RemoveAndReturnItem(Item item);
    public abstract void DestroyItem(Item item);
    public abstract bool CanInsertItem();
    public abstract void InsertItem(Item item);
}