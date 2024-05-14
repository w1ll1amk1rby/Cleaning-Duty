using System;
using TMPro;
using UnityEngine;
/**
    basic UI counter to show remaining round timer.
*/
public class CounterCanvasUpdater : MonoBehaviour
{
    [SerializeField] private GameStateManager playManager;
    [SerializeField] private PlayHandler playHandler;
    [SerializeField] private TextMeshProUGUI timerText;
    public void Update()
    {
        if (this.playManager.GetGameState() == GameState.Play)
        {
            this.timerText.gameObject.SetActive(true);
            Math.Round(this.playHandler.GetRemainingTimeSeconds());
            this.timerText.text = Math.Round(this.playHandler.GetRemainingTimeSeconds()).ToString() + "s";
        }
        else
        {
            this.timerText.gameObject.SetActive(false);
        }
    }
}
