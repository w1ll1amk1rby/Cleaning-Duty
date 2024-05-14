using System.Collections.Generic;
using UnityEngine;
/**
    manages and stores the possible game states
*/
public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameState initialGameState;
    private GameState gameState;
    private SortedDictionary<GameState, GameStateHandler> handlerMap;
    private GameStateHandler currentHandler;
    public void Awake()
    {
        this.handlerMap = new SortedDictionary<GameState, GameStateHandler>();
    }
    public void Start()
    {
        this.SetGameState(this.initialGameState);
    }
    public void Update()
    {
        if (this.currentHandler != null)
        {
            this.currentHandler.HandleUpdate();
        }
    }
    public GameState GetGameState()
    {
        return this.gameState;
    }
    public GameStateHandler GetGameStateHandler()
    {
        return this.currentHandler;
    }
    public void SetGameState(GameState gameState)
    {
        if (gameState == this.gameState)
        {
            return;
        }
        if (this.currentHandler != null)
        {
            this.currentHandler.HandleStateFromUpdate();
        }
        this.gameState = gameState;
        this.currentHandler = this.handlerMap.GetValueOrDefault(gameState);
        if (this.currentHandler != null)
        {
            this.currentHandler.HandleStateToUpdate();
        }
    }
    public void AddNewHandler(GameStateHandler handler, GameState ToAddTo)
    {
        if (handler == null)
        {
            return;
        }
        this.handlerMap.Add(ToAddTo, handler);
        if (ToAddTo == this.gameState)
        {
            this.currentHandler = handler;
        }
    }
}
public enum GameState
{
    PreGame,
    Play,
    Break
}
