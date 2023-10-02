using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLenght)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPos);
        var previousPos = startPos;

        for (int i = 0; i< walkLenght; i++)
        {
            var newPos = previousPos + direction2D.getRandomDirection();
            path.Add(newPos);
            previousPos = newPos;
        }

        return path;
    }

    public static List<Vector2Int> randomWalkCorridor(Vector2Int startPos, int length)
    {
        List<Vector2Int> path = new List<Vector2Int>();

        var dir = direction2D.getRandomDirection();

        var currentPos = startPos;
        path.Add(currentPos);

        for (int i = 0; i < length; i++)
        {
            currentPos += dir;
            path.Add(currentPos);
        }

        return path;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if (room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if(Random.value < 0.5f)
                {
                    //split horizontal
                    if(room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }else if (room.size.x >= minWidth * 2) //split vertical
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }else if(room.size.y >= minHeight && room.size.x >= minWidth)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    //split vertical
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)//split horizontal
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight && room.size.x >= minWidth)
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally( int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class direction2D 
{
    public static List<Vector2Int> cardinalDirList = new List<Vector2Int>()
    {
        new Vector2Int(0,1), //up
        new Vector2Int(1,0), //right
        new Vector2Int(0,-1),//down
        new Vector2Int(-1,0), //left
    };

    public static Vector2Int getRandomDirection()
    {
        return cardinalDirList[Random.Range(0, cardinalDirList.Count)];
    }
};