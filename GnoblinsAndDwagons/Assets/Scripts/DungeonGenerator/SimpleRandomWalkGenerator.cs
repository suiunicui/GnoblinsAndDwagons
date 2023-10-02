using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkGenerator : AbstractDungeonGenerator
{ 
    [SerializeField]
    protected SimpleRandomWalkSO randomParams;


    protected override void runProceduralGeneration()
    {
        HashSet<Vector2Int> floorPos = runRandomWalk(randomParams, startPosition);
        tileMapVisualizer.paintFloor(floorPos);
        WallGenerator.createWalls(floorPos, tileMapVisualizer);
    }

    protected HashSet<Vector2Int> runRandomWalk(SimpleRandomWalkSO randomParams, Vector2Int startPosition)
    {
        var currentPos = startPosition;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        for (int i = 0; i < randomParams.iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPos, randomParams.walkLength);
            floorPos.UnionWith(path);
            if (randomParams.startRandomEachIteration)
            {
                currentPos = floorPos.ElementAt(Random.Range(0,floorPos.Count));
            }
        }
        return floorPos;
    }
}
