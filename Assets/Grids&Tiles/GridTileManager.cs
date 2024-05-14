using System.Collections.Generic;
using UnityEngine;
/**
    stores a grid of tiles in the world that can then be referred too.
*/
public class GridTileManager : MonoBehaviour
{
    [SerializeField] private int tileMapZMax;
    [SerializeField] private int tileMapXMax;
    [SerializeField] private List<Tile> initialTiles;
    private Tile[,] tileMap;
    // Start is called before the first frame update
    public void Start()
    {
        this.tileMap = new Tile[this.tileMapXMax, this.tileMapZMax];
        this.LoadInitialTiles();
    }
    public void AddTile(Vector3Int coord, Tile tile)
    {
        if (!this.GridCoordsWithinGrid(coord))
        {
            return;
        }
        Tile currentTile = this.tileMap[coord.x, coord.z];
        if (currentTile != null)
        {
            this.RemoveAndDestroyTile(coord);
        }
        this.tileMap[coord.x, coord.z] = tile;
        tile.gameObject.transform.SetParent(this.transform);
        tile.transform.position = GetGridHookPoint(coord);
    }
    public void RemoveAndDestroyTile(Vector3Int coord)
    {
        Tile tile = this.RemoveAndReturnTile(coord);
        Destroy(tile.gameObject);
    }
    public Tile RemoveAndReturnTile(Vector3Int coord)
    {
        if (!this.GridCoordsWithinGrid(coord))
        {
            return null;
        }
        Tile tile = this.tileMap[coord.x, coord.z];
        this.tileMap[coord.x, coord.z] = null;
        tile.gameObject.transform.SetParent(null);
        return tile;
    }
    public Tile GetTile(Vector3Int coord)
    {
        if (!this.GridCoordsWithinGrid(coord))
        {
            return null;
        }
        return this.tileMap[coord.x, coord.z];
    }
    public bool GridCoordsWithinGrid(Vector3Int coord)
    {
        float minX = 0;
        float maxX = minX + this.tileMapXMax - 1;
        float minZ = 0;
        float maxZ = minZ + this.tileMapZMax -1;
        return (minX <= coord.x && maxX >= coord.x) && (minZ <= coord.z && maxZ >= coord.z);
    }
    public Vector3Int ConvertWorldCoordsToGridCoord(Vector3 coord)
    {
        Vector3 relativePosition = coord - this.transform.position;
        Vector3Int gridCoord = Vector3Int.FloorToInt(relativePosition);
        gridCoord.y = 0;
        return gridCoord;
    }
    public Vector3Int ForceCoordToGrid(Vector3Int coord)
    {
        coord.x = (int)Mathf.Clamp(coord.x, 0, this.tileMapXMax - 1);
        coord.z = (int)Mathf.Clamp(coord.z, 0, this.tileMapZMax - 1);
        coord.y = 0;
        return coord;
    }
    private void LoadInitialTiles()
    {
        if (this.initialTiles == null)
        {
            return;
        }
        foreach (Tile tile in this.initialTiles)
        {
            Vector3Int gridCoord = this.ForceCoordToGrid(this.ConvertWorldCoordsToGridCoord(tile.transform.position));
            this.AddTile(gridCoord, tile);
        };
        this.initialTiles = null;
    }
    private Vector3 GetGridHookPoint(Vector3Int gridCoord)
    {
        Vector3 worldCoord = this.transform.position + gridCoord + new Vector3(0.5f, 0, 0.5f);
        worldCoord.y = this.transform.position.y;
        return worldCoord;
    }
}