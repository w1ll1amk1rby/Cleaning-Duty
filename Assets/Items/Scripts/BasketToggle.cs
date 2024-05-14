using UnityEngine;
[RequireComponent(typeof(BasketItem))]
/**
    describes a toggleable for a basket that allows the player to toggle the lid on and off
*/
public class BasketToggle : Toggableable
{
    private BasketItem basket;
    public void Awake() {
        this.basket = this.GetComponent<BasketItem>();
    }
    public override void Toggle() {
        this.basket.ToggleOpen();
    }
}