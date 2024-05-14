using UnityEngine;
/**
    game state for when the game is in play. a timer counts down and moves the state to break state.
*/
public class PlayHandler : GameStateHandler
{
    private int dayCounter = 0;
    private float currentTimer = 0;
    [SerializeField] private float maxTimer;
    public void Awake()
    {
        this.ResetWarDay();
    }
    public override void HandleStateToUpdate()
    {
        this.ResetWarDay();
        this.dayCounter++;
    }
    public override void HandleUpdate()
    {
        this.currentTimer = this.currentTimer + Time.deltaTime;
        if (this.currentTimer >= this.maxTimer)
        {
            this.currentTimer = this.maxTimer;
            this.playManager.SetGameState(GameState.Break);
        }
    }
    public override void HandleStateFromUpdate()
    {

    }
    public override bool CanItemsExist()
    {
        return true;
    }
    public override GameState GetGameState()
    {
        return GameState.Play;
    }
    public float GetRemainingTimeSeconds()
    {
        return this.maxTimer - this.currentTimer;
    }
    public int GetDayCount()
    {
        return this.dayCounter;
    }
    private void ResetWarDay()
    {
        this.currentTimer = 0;
    }
}