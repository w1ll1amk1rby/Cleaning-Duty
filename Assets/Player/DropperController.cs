using UnityEngine;
[RequireComponent(typeof(ItemHolder))]
/**
 basic temp controller that controls a players ability to pickup and place items from its item holder.
*/
public class DropperController : MonoBehaviour
{
    [SerializeField] private GridTileManager grid;
    [SerializeField] private GameObject controllerPoint;
    private ItemHolder itemHolder;
    public void Start()
    {
        this.itemHolder = this.GetComponent<ItemHolder>();
    }
    public void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }
        Vector3Int gridCoord = this.grid.ConvertWorldCoordsToGridCoord(this.controllerPoint.transform.position);
        if (!this.grid.GridCoordsWithinGrid(gridCoord))
        {
            return;
        }
        Tile tile = this.grid.GetTile(gridCoord);
        if (tile == null)
        {
            return;
        }
        ItemDepositer itemDeposit = tile.GetComponent<ItemDepositer>();
        ItemCollecter itemCollection = tile.GetComponent<ItemCollecter>();
        if (
            itemDeposit != null &&
            this.itemHolder.HasItem() &&
            itemDeposit.CanDeposit(this.itemHolder.GetNextItem())
        )
        {
            itemDeposit.Deposit(this.itemHolder.RemoveAndReturnNextItem());
        }
        else if (
            itemCollection != null &&
            this.itemHolder.CanInsertItem()
            && itemCollection.CanCollect()
        )
        {
            this.itemHolder.InsertItem(itemCollection.Collect());
        }
    }
}
