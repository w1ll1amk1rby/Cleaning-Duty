using UnityEngine;
/**
    describes an abstract class for an item depositer, allows items to be deposited here.
*/
public abstract class ItemDepositer : MonoBehaviour
{
    public abstract bool CanDeposit(Item item);
    public abstract void Deposit(Item item);
}
