using System;
using UnityEngine;
[RequireComponent(typeof(Machine))]
[RequireComponent(typeof(ItemHolder))]
/**
    a class to handle machine lights, shows how full the machine is and when on how long left on the machine.
*/
public class MachineLightHandler : MonoBehaviour
{
    private Machine machine;
    private ItemHolder itemHolder;
    [SerializeField] private MeshRenderer[] lights;

    [SerializeField] private Material emptyLight;
    [SerializeField] private Material slotLight;
    [SerializeField] private Material countLight;
    public void Awake()
    {
        this.itemHolder = this.GetComponent<ItemHolder>();
        this.machine = this.GetComponent<Machine>();
        this.UpdateLights();
    }
    public void Update()
    {
        this.UpdateLights();
    }
    private void UpdateLights()
    {
        if (this.machine.GetCurrentState() == Machine.State.On)
        {
            this.SetLightsToCountDown();
        }
        else
        {
            this.SetLightsToStorage();
        }
    }
    private void SetLightsToCountDown()
    {
        int showLightCount = Mathf.CeilToInt(this.machine.GetPercentageComplete() * this.lights.Length);
        for (int i = 0; i < this.lights.Length; i++)
        {
            if (i < showLightCount)
            {
                this.lights[i].material = this.countLight;
            }
            else
            {
                this.lights[i].material = this.emptyLight;
            }
        }

    }
    private void SetLightsToStorage()
    {
        Item[] items = this.itemHolder.GetItems();
        int maxCount = Math.Min(this.lights.Length, items.Length);
        for (int i = 0; i < this.lights.Length; i++)
        {
            if(items[i] == null || i >= maxCount) {
                this.lights[i].material = this.emptyLight;
            } 
            else 
            {
                this.lights[i].material = this.slotLight;
            }
        }
    }
}