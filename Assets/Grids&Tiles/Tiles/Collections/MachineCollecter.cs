using UnityEngine;
[RequireComponent(typeof(Machine))]
/**
*   defines an item collecter that collects from a machine, checks if the machine is on before collecting.
*/
public class MachineCollecter : ItemCollecterHolder
{
    private Machine machine;
    public new void Start()
    {
        this.machine = this.GetComponent<Machine>();
        base.Start();
    }
    public override bool CanCollect()
    {
        if (this.machine.GetCurrentState() == Machine.State.On)
        {
            return false;
        }
        else
        {
            return base.CanCollect();
        }
    }
    public override Item Collect()
    {
        if (this.machine.GetCurrentState() == Machine.State.On)
        {
            return null;
        }
        else
        {
            return base.Collect();
        }
    }
}