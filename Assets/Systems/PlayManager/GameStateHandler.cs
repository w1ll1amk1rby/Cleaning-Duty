using UnityEngine;
/**
    a class that describes a possible gamestate, describes what is allowed during the game state such as items  being able to exist
*/
public abstract class GameStateHandler : MonoBehaviour
{
    [SerializeField] protected GameStateManager playManager;
    public void Start() {
        playManager.AddNewHandler(this, this.GetGameState());
    }
    public abstract void HandleStateToUpdate();
    public abstract void HandleUpdate();
    public abstract void HandleStateFromUpdate();
    public abstract bool CanItemsExist();
    public abstract GameState GetGameState();
}