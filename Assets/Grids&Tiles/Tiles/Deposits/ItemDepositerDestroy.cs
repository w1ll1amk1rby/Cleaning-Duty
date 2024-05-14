using UnityEngine;
/**
    acts as a bin that allows items to be placed and immediatly destroyed.
*/
public class ItemDepositerDestroy : ItemDepositer
{
    public override bool CanDeposit(Item item)
    {
        return item != null;
    }

    public override void Deposit(Item item)
    {
        Object.Destroy(item.gameObject);
    }
}
