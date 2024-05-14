using UnityEngine;
[RequireComponent(typeof(Machine))]
/**
allows for depositing the same as an item depositer but checks if the machine is off before being allowed to deposit
*/
public class MachineDepositer : ItemDepositerHolder
{
    private Machine machine;
    // Start is called before the first frame update
    public new void Start()
    {
        this.machine = this.GetComponent<Machine>();
        base.Start();
    }
    public override bool CanDeposit(Item item)
    {
        if(this.machine.GetCurrentState() == Machine.State.On) {
            return false;
        }
        return base.CanDeposit(item);
    }
    public override void Deposit(Item item)
    {
        if(this.machine.GetCurrentState() == Machine.State.On) {
            return;
        }
        base.Deposit(item);
    }
}
