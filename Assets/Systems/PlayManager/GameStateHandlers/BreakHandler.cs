using UnityEngine;
/**
    game state between play states.
*/
public class BreakHandler : GameStateHandler
{
    public override void HandleStateFromUpdate()
    {

    }
    public override void HandleStateToUpdate()
    {

    }
    public override void HandleUpdate()
    {
        if(Input.GetKey(KeyCode.K)) {
            this.playManager.SetGameState(GameState.Play);
        }
    }
    public override bool CanItemsExist()
    {
        return false;
    }
    public override GameState GetGameState()
    {
        return GameState.Break;
    }
}