using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkGenerator : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    public int walkLength = 10;
    [SerializeField]
    public bool startRandomEachIteration = true;

    [SerializeField]
    private TileMapVisualizer tileMapVisualizer;

    public void runProceduralGeneration()
    {
        HashSet<Vector2Int> floorPos = runRandomWalk();
        foreach (var position in floorPos)
        {
            tileMapVisualizer.paintFloor(floorPos);
        }
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
