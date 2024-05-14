using UnityEngine;
/**
    basic temp controller that handles players interactions with tile or items onm counters
*/
public class ProcessController : MonoBehaviour
{
    [SerializeField] private GridTileManager grid;
    [SerializeField] private GameObject controllerPoint;
    private Interactable currentInteractor;
    public void Update()
    {
        bool InteractKeyPressed = Input.GetKey(KeyCode.E);
        if (!InteractKeyPressed && currentInteractor)
        {
            this.EndProcess();
        }
        Vector3Int gridCoord = this.grid.ConvertWorldCoordsToGridCoord(this.controllerPoint.transform.position);
        Tile tile = null;
        if (this.grid.GridCoordsWithinGrid(gridCoord))
        {
            tile = this.grid.GetTile(gridCoord);
        }
        Interactable interactor = null;
        if (tile)
        {
            interactor = tile.GetComponent<Interactable>();
        }
        if (currentInteractor && currentInteractor != interactor)
        {
            this.EndProcess();
        }
        if (interactor && InteractKeyPressed)
        {
            if (this.currentInteractor != interactor)
            {
                this.StartProcess(interactor);
            }
            interactor.ContinueProcess();
        }
    }
    public void EndProcess()
    {
        currentInteractor.EndProcess();
        currentInteractor = null;
    }
    public void StartProcess(Interactable interactor)
    {
        this.currentInteractor = interactor;
        interactor.StartProcess();
    }
}
