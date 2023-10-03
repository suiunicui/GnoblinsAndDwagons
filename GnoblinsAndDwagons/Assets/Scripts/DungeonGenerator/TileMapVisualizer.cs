using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap;

    [SerializeField]
    private Tilemap wallTilemap;

    [SerializeField]
    private TileBase topWallTile;
    [SerializeField]
    private TileBase leftWallTile; 
    [SerializeField]
    private TileBase rightWallTile;
    [SerializeField]
    private TileBase bottomWallTile;

    [SerializeField]
    private TileBase floorTile;

    public void paintFloor(IEnumerable<Vector2Int> floorPositions)
    {
        paintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void paintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            paintSingleTile(tilemap, tile, position);
        }
    }

    private void paintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void paintSingleWall(Vector2Int wallPos)
    {
       paintSingleTile(wallTilemap, topWallTile, wallPos);
    }
}
