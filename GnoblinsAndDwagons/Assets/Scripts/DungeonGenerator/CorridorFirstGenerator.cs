using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class CorridorFirstGenerator : SimpleRandomWalkGenerator
{
    [SerializeField]
    private int corridorLength = 10, corridorCount = 5;

    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;
   
    protected override void runProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPos = new HashSet<Vector2Int>();

        createCorridors(floorPos, potentialRoomPos);

        HashSet<Vector2Int> roomPos = CreateRooms(potentialRoomPos);

        List<Vector2Int> deadEnds = findDeadEnds(floorPos);

        CreateRoomsAtDeadEnds(deadEnds, roomPos);

        floorPos.UnionWith(roomPos);

        tileMapVisualizer.paintFloor(floorPos);
        WallGenerator.createWalls(floorPos, tileMapVisualizer);
    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPos)
    {
        foreach (Vector2Int pos in deadEnds)
        {
            if (!roomPos.Contains(pos))
            {
                HashSet<Vector2Int> room = runRandomWalk(randomParams, pos);
                roomPos.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> findDeadEnds(HashSet<Vector2Int> floorPos)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (Vector2Int pos in floorPos)
        {
            int neighboursCount = 0;
            foreach(var direction in direction2D.cardinalDirList)
            {
                if(floorPos.Contains(pos + direction))
                {
                    neighboursCount++;
                }
            }
            if(neighboursCount == 1)
            {
                deadEnds.Add(pos);
            }
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPos)
    {
        HashSet<Vector2Int> roomPos = new HashSet<Vector2Int>();

        int roomsToCreate = Mathf.RoundToInt(potentialRoomPos.Count*roomPercent);
        List<Vector2Int> roomToCreate = potentialRoomPos.OrderBy(x => Guid.NewGuid()).Take(roomsToCreate).ToList();

        foreach (var room in roomToCreate)
        {
            var roomFloor = runRandomWalk(randomParams, room);
            roomPos.UnionWith(roomFloor);
        }
        return roomPos;
    }

    private void createCorridors(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> potentialRoomPos)
    {
        var currentPos = startPosition;
        potentialRoomPos.Add(currentPos);
        for (int i = 0; i < corridorCount; i++) 
        {
            var path = ProceduralGenerationAlgorithms.randomWalkCorridor(currentPos, corridorLength);
            currentPos = path[path.Count - 1];
            potentialRoomPos.Add(currentPos);
            floorPos.UnionWith(path);
        }

    }
}
