using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap;

    [SerializeField]
    private Tilemap wallTilemap;

    [SerializeField]
    private TileBase topWallTile, leftWallTile, rightWallTile, bottomWallTile, wallFull;


    [SerializeField]
    private TileBase topRightWallTile, topLeftWallTile, bottomRightWallTile, bottomLeftWallTile;

    [SerializeField]
    private TileBase bottomRightInnerWallTile, bottomLeftInnerWallTile;

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

    internal void paintSingleWall(Vector2Int wallPos, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            tile = topWallTile;
        }else if (WallTypesHelper.wallBottm.Contains(typeAsInt))
        {
            tile = bottomWallTile;
        }else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = leftWallTile;
        }else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = rightWallTile;
        }else if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }

        if (tile != null)
        {
            paintSingleTile(wallTilemap, tile, wallPos);
        }
    }

    internal void paintSingleCornerWall(Vector2Int wallPos, string binaryType)
    {
       int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if( WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = topRightWallTile;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = topLeftWallTile;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = bottomLeftWallTile;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = bottomRightWallTile;
        }
        else if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = bottomLeftInnerWallTile;
        }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = bottomRightInnerWallTile;
        }else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallFull;
        }else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
        {
            tile = bottomWallTile;
        }

        if (tile != null)
        {
            paintSingleTile(wallTilemap, tile, wallPos);
        }
    }
}
