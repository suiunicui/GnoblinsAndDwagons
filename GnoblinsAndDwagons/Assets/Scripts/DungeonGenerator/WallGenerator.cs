using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void createWalls(HashSet<Vector2Int> floorPos, TileMapVisualizer tileMapVisualizer)
    {
        var basicWallPos = findWallsInDirections(floorPos, direction2D.cardinalDirList);
        var cornerWallPos = findWallsInDirections(floorPos, direction2D.diagDirList);
        createBasicWall(tileMapVisualizer, basicWallPos, floorPos);
        createCornerWall(tileMapVisualizer, cornerWallPos, floorPos);
    }

    private static void createCornerWall(TileMapVisualizer tileMapVisualizer, HashSet<Vector2Int> cornerWallPos, HashSet<Vector2Int> floorPos)
    {
        foreach (var wall in cornerWallPos)
        {
            string neighboursBinary = "";
            foreach (var direction in direction2D.eightDirList)
            {
                var neighbourPos = wall + direction;
                if (floorPos.Contains(neighbourPos))
                {
                    neighboursBinary += "1";
                }
                else
                {
                    neighboursBinary += "0";
                }
            }
            tileMapVisualizer.paintSingleCornerWall(wall, neighboursBinary);
        }
    }

    private static void createBasicWall(TileMapVisualizer tileMapVisualizer, HashSet<Vector2Int> basicWallPos, HashSet<Vector2Int> floorPos)
    {
        foreach (var wall in basicWallPos)
        {
            string neighboursBinary = "";
            foreach (var direction in direction2D.cardinalDirList)
            {
                var neighbourPos = wall + direction;
                if (floorPos.Contains(neighbourPos))
                {
                    neighboursBinary += "1";
                }
                else
                {
                    neighboursBinary += "0";
                }
            }
            tileMapVisualizer.paintSingleWall(wall, neighboursBinary);
        }
    }

    private static HashSet<Vector2Int> findWallsInDirections(HashSet<Vector2Int> floorPos, List<Vector2Int> dirList)
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
        foreach( var position in floorPos) 
            { 
                foreach(var direction in dirList)
            {
                var neighbourPos = position + direction;
                if (!floorPos.Contains(neighbourPos)) 
                {
                    wallPos.Add(neighbourPos);
                }
            }
           }
        return wallPos;
    }
}
