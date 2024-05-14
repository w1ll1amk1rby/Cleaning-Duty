using UnityEngine;
/**
    initial game state, player can switch state to play by pressing k
*/
public class PreGameHandler : GameStateHandler
{
    public override void HandleStateToUpdate() {
        
    }
    public override void HandleUpdate() {
        if(Input.GetKey(KeyCode.K)) {
            this.playManager.SetGameState(GameState.Play);
        }
    }
    public override void HandleStateFromUpdate() {

    }
    public override bool CanItemsExist() {
        return false;
    }
    public override GameState GetGameState() {
        return GameState.PreGame;
    }
}