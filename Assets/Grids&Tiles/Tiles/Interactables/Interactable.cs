using UnityEngine;
/**
    abstract class that allows for a player to interact with a tile.
*/
public abstract class Interactable : MonoBehaviour
{
    public abstract void StartProcess();
    public abstract void ContinueProcess();
    public abstract void EndProcess();
}