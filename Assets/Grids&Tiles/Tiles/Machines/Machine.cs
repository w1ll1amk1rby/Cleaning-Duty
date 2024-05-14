using UnityEngine;
/**
    describes an abstract machine tile that can be switched on and off
*/
public abstract class Machine : MonoBehaviour
{
    public enum State
    {
        On = 0,
        Off = 1
    }
    [SerializeField] private float completeMachineTime;
    private float currentMachineLeftTime;
    private State currentState;
    public abstract void HandleMachineOnUpdate();
    public abstract void HandleMachineSwitchOnState();
    public abstract void HandleMachineSwitchOffState();
    public abstract void HandleMachineCompletion();
    public void Awake()
    {
        this.SetMachineState(State.Off);
    }
    public void Update()
    {
        if (this.currentState == State.On)
        {
            this.HandleMachineOnUpdate();
            this.currentMachineLeftTime -= Time.deltaTime;
            if (this.currentMachineLeftTime <= 0)
            {
                this.currentMachineLeftTime = 0;
                this.HandleMachineCompletion();
                this.SetMachineState(State.Off);
            }
        }
    }
    public void SetMachineState(State state)
    {
        if (state == this.currentState)
        {
            return;
        }
        switch (state)
        {
            case State.On:
                this.currentMachineLeftTime = this.completeMachineTime;
                this.HandleMachineSwitchOnState();
                break;
            case State.Off:
                this.currentMachineLeftTime = 0;
                this.HandleMachineSwitchOffState();
                break;
        }
        this.currentState = state;
    }
    public float GetPercentageComplete()
    {
        return this.currentMachineLeftTime / this.completeMachineTime;
    }
    public State GetCurrentState()
    {
        return this.currentState;
    }
}