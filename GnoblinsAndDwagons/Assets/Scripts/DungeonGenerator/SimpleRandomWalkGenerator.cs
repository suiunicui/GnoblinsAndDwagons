using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkGenerator : AbstractDungeonGenerator
{ 
    [SerializeField]
    private int iterations = 10;

    [SerializeField]
    public int walkLength = 10;

    [SerializeField]
    public bool startRandomEachIteration = true;

    protected override void runProceduralGeneration()
    {
        HashSet<Vector2Int> floorPos = runRandomWalk();
        tileMapVisualizer.paintFloor(floorPos);
        WallGenerator.createWalls(floorPos, tileMapVisualizer);
    }

    protected HashSet<Vector2Int> runRandomWalk()
    {
        var currentPos = startPosition;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        for (int i = 0; i < iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPos, walkLength);
            floorPos.UnionWith(path);
            if (startRandomEachIteration)
            {
                currentPos = floorPos.ElementAt(Random.Range(0,floorPos.Count));
            }
        }
        return floorPos;
    }
}
