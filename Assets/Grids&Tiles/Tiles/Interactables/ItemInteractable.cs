using UnityEngine;
[RequireComponent(typeof(ItemHolder))]
/**
    handles interacting with an item, such as toggling the item state or washing
*/
public class ItemInteractable : Interactable
{
    private ItemHolder itemHolder;
    private Item currentItem;
    private Toggableable toggableable;
    private Washable washable;
    [SerializeField] private bool canToggle;
    [SerializeField] private bool canWashClothes;
    [SerializeField] private float washPerSecond;
    [SerializeField] private ProgressBar progressBar;
    public void Awake()
    {
        this.itemHolder = this.GetComponent<ItemHolder>();
        this.currentItem = null;
        this.toggableable = null;
        this.washable = null;
    }
    public override void StartProcess()
    {
        if (this.currentItem)
        {
            this.RemoveCurrentItem();
        }
        this.SetCurrentItem(this.itemHolder.GetNextItem());
        this.UpdateProgressBar();
    }
    public override void ContinueProcess()
    {
        Item newItem = itemHolder.GetNextItem();
        if (newItem != this.currentItem)
        {
            this.RemoveCurrentItem();
            this.SetCurrentItem(newItem);
        }
        if (this.washable && this.canWashClothes)
        {

            if (this.washable.Clean(this.washPerSecond * Time.deltaTime))
            {
                Item newWashedItem = this.washable.GenerateWashedVersion();
                Item oldItem = this.RemoveCurrentItem();
                this.SetCurrentItem(newWashedItem);
                this.itemHolder.RemoveAndReturnItem(oldItem);
                Destroy(oldItem.gameObject);
                this.itemHolder.InsertItem(newWashedItem);

            }
        }
        this.UpdateProgressBar();
    }
    public override void EndProcess()
    {
        this.RemoveCurrentItem();
        this.UpdateProgressBar();
    }
    private void UpdateProgressBar()
    {
        if (this.progressBar == null)
        {
            return;
        }
        else if (this.washable == null)
        {
            this.progressBar.SetShowBar(false);
        }
        else
        {
            this.progressBar.SetShowBar(true);
            this.progressBar.SetProgressPercentage(1 - this.washable.GetDirtPercentage());
        }

    }
    private void SetCurrentItem(Item item)
    {
        this.currentItem = item;
        if (!this.currentItem)
        {
            return;
        }
        this.toggableable = this.currentItem.GetComponent<Toggableable>();
        if (this.toggableable && this.canToggle)
        {
            this.toggableable.Toggle();
        }
        this.washable = this.currentItem.GetComponent<Washable>();
    }
    private Item RemoveCurrentItem()
    {
        Item oldItem = this.currentItem;
        this.currentItem = null;
        this.toggableable = null;
        this.washable = null;
        return oldItem;
    }
}
