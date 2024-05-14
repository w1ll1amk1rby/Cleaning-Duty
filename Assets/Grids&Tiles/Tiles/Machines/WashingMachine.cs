using UnityEngine;
[RequireComponent(typeof(ItemHolder))]
/**
    a machine that washes items when on.
*/
public class WashingMachine : Machine
{
    private ItemHolder itemHolder;
    public new void Awake()
    {
        base.Awake();
        this.itemHolder = this.GetComponent<ItemHolder>();
    }
    public override void HandleMachineCompletion()
    {
        Item[] items = this.itemHolder.GetItems();
        for (int i = 0; i < items.Length; i++)
        {
            Item item = items[i];
            if (item)
            {
                Washable washable = item.GetComponent<Washable>();
                if (washable)
                {
                    Item newWashedItem = washable.GenerateWashedVersion();
                    this.itemHolder.RemoveAndReturnItem(item);
                    Destroy(item.gameObject);
                    this.itemHolder.InsertItem(newWashedItem);
                }
            }
        }
    }
    public override void HandleMachineOnUpdate()
    {
    }
    public override void HandleMachineSwitchOffState()
    {
    }
    public override void HandleMachineSwitchOnState()
    {
    }
}