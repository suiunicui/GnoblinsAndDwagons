using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void createWalls(HashSet<Vector2Int> floorPos, TileMapVisualizer tileMapVisualizer)
    {
        var basicWallPos = findWallsInDirections(floorPos, direction2D.cardinalDirList);
        foreach (var wall in basicWallPos)
        {
            tileMapVisualizer.paintSingleWall(wall);
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
