using UnityEngine;
[RequireComponent(typeof(Machine))]
/**
    an iteractable that allows for a machine to be turned on.
*/
public class SwitchableMachine : Interactable
{
    private Machine machine;
    public void Awake()
    {
        this.machine = this.GetComponent<Machine>();
    }
        public override void StartProcess()
    {
        this.machine.SetMachineState(Machine.State.On);
    }
    public override void ContinueProcess()
    {
        // doNothing
    }
    public override void EndProcess()
    {
        // doNoithing
    }
}