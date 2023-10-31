using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using Random = UnityEngine.Random;

public class RoomFirstGenerator : SimpleRandomWalkGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;

    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;

    [SerializeField]
    [Range(0,10)]
    private int offset = 1;

    [SerializeField]
    private List<GameObject> clutter;

    [SerializeField]
    private GameObject npcMiner, levelExit;

    [SerializeField]
    private GameObject enemyMimic;

    [SerializeField]
    private List<GameObject> lowLevelEnemies;

    [SerializeField]
    private GameStateMemory gameStateMemory;

    [SerializeField]
    GameObject playerController;

    [SerializeField]
    GameObject door; 

    [SerializeField]
    GameObject doorUnlocker;

    [SerializeField]
    GameObject doorLocker;

    private int lockedDoorRoomIndex;

    //Generated data
    private Dictionary<Vector2Int, HashSet<Vector2Int>> roomDict = new Dictionary<Vector2Int, HashSet<Vector2Int>>();

    protected override void runProceduralGeneration()
    {
        createRooms();
    }

    private void createRooms()
    {
        var roomList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, 
            new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = createSimpleRooms(roomList);

        List<Vector2Int> roomCenters = new List<Vector2Int>();

        foreach (var room in roomList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = connectRooms(roomCenters);
        floor.UnionWith(corridors);

        tileMapVisualizer.paintFloor(floor);
        WallGenerator.createWalls(floor, tileMapVisualizer);
        lockedDoorRoomIndex = 0;
        populateRooms(roomList, corridors);
    }

    private void populateRooms(List<BoundsInt> roomList, HashSet<Vector2Int> corridors)
    {
        BoundsInt room = roomList[Random.Range(0, roomList.Count)];

        gameStateMemory.DungeonStartPos = Vector3Int.RoundToInt(room.center + new Vector3(0.5f, 0.5f, 0));

        for(int i = 0; i<Random.Range(5,10); i++)
        {
            Vector3Int entityPos = new Vector3Int(Random.Range(room.min.x + offset, room.max.x), Random.Range(room.min.y + offset, room.max.y), 0);
            if(entityPos != gameStateMemory.DungeonStartPos && corridors.Contains((Vector2Int)entityPos) == false)
            {
                Instantiate(clutter[Random.Range(0,clutter.Count)], entityPos+new Vector3(0.5f,0.5f,0), Quaternion.identity);
            }
        }


        roomList.Remove(room);



        room = roomList[Random.Range(0, roomList.Count)];
        Vector3Int spawnPos = new Vector3Int(Random.Range(room.min.x+offset + 1, room.max.x - 1), Random.Range(room.min.y+offset + 1, room.max.y - 1), 0);
        Instantiate(levelExit, spawnPos, Quaternion.identity);

        spawnPos = new Vector3Int(Random.Range(room.min.x + offset, room.max.x), Random.Range(room.min.y + offset, room.max.y), 0);
        if (spawnPos != gameStateMemory.DungeonStartPos && corridors.Contains((Vector2Int)spawnPos) == false)
        {
            Instantiate(npcMiner, spawnPos + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        }
        
        roomList.Remove(room);

        foreach (BoundsInt loopRoom in roomList)
        {
            int randomRoom = Random.Range(0, 100);
            if(randomRoom < 5 && lockedDoorRoomIndex <= 2)
            { 
                CreateDoorPuzzelRoom(loopRoom, corridors);
            }
            else if (randomRoom < 15)
            {
                createMimicRoom(loopRoom, corridors);
            } 
            else if  (randomRoom < 80)
            {
               createMonsterRoom(loopRoom, corridors);
            }
            else 
            { 
                createEmptyRoom(loopRoom, corridors); 
            }
        }

        
    }

    private void CreateDoorPuzzelRoom(BoundsInt room, HashSet<Vector2Int> corridors)
    {
        //GameObject controller = GameObject.Find("GameController");

        Vector3Int doorUnlockerPos = new Vector3Int(Random.Range(room.min.x + offset, room.max.x), Random.Range(room.min.y + offset, room.max.y), 0);
        while(corridors.Contains((Vector2Int)doorUnlockerPos) == true)
        {
            doorUnlockerPos = new Vector3Int(Random.Range(room.min.x + offset, room.max.x), Random.Range(room.min.y + offset, room.max.y), 0);
        }
        GameObject doorUnlockerObject = Instantiate(doorUnlocker, doorUnlockerPos + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        doorUnlockerObject.tag = "doorUnlocker" + lockedDoorRoomIndex;
        doorUnlocker doorUnlockerScript = doorUnlockerObject.GetComponent<doorUnlocker>();
        doorUnlockerScript.doorTag = "doorRoom" + lockedDoorRoomIndex;
        doorUnlockerScript.isRoomLocked = false;


        Vector3Int doorLockerPos = new Vector3Int(Random.Range(room.min.x + offset, room.max.x), Random.Range(room.min.y + offset, room.max.y), 0);

        while (corridors.Contains((Vector2Int)doorLockerPos) == true)
        {
            doorLockerPos = new Vector3Int(Random.Range(room.min.x + offset, room.max.x), Random.Range(room.min.y + offset, room.max.y), 0);
        }
        GameObject doorLockerObject = Instantiate(doorLocker, doorLockerPos + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        doorLocker doorLockerScript = doorLockerObject.GetComponent<doorLocker>();
        doorLockerScript.doorTag = "doorRoom" + lockedDoorRoomIndex;
        doorLockerScript.unlockTag = "doorUnlocker" + lockedDoorRoomIndex;

        for (int wallx= room.min.x + offset; wallx < room.max.x; wallx++)
        {
            Vector2Int wall = new Vector2Int(wallx, room.min.y + offset-1);
            if (corridors.Contains(wall))
            {
                GameObject doorObject = Instantiate(door, new Vector3(wall.x, wall.y, 0) + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                doorObject.tag = "doorRoom" + lockedDoorRoomIndex;
                doorObject.GetComponent<BoxCollider2D>().enabled = false;
                doorObject.GetComponent<Renderer>().enabled = false;
            }
            wall = new Vector2Int(wallx, room.max.y);
            if (corridors.Contains(wall))
            {
                GameObject doorObject = Instantiate(door, new Vector3(wall.x, wall.y, 0) + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                doorObject.tag = "doorRoom" + lockedDoorRoomIndex;
                doorObject.GetComponent<BoxCollider2D>().enabled = false;
                doorObject.GetComponent<Renderer>().enabled = false;
            }
        }
        for (int wally = room.min.y + offset; wally < room.max.y; wally++)
        {
            Vector2Int wall = new Vector2Int(room.min.x + offset -1, wally);
            if (corridors.Contains(wall))
            {
                GameObject doorObject = Instantiate(door, new Vector3(wall.x, wall.y, 0) + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                doorObject.tag = "doorRoom" + lockedDoorRoomIndex;
                doorObject.GetComponent<BoxCollider2D>().enabled = false;
                doorObject.GetComponent<Renderer>().enabled = false;
            }
            wall = new Vector2Int(room.max.x, wally);
            if (corridors.Contains(wall))
            {
                GameObject doorObject = Instantiate(door, new Vector3(wall.x, wall.y, 0) + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                doorObject.tag = "doorRoom" + lockedDoorRoomIndex;
                doorObject.GetComponent<BoxCollider2D>().enabled = false;
                doorObject.GetComponent<Renderer>().enabled = false;
            }
        }
        lockedDoorRoomIndex++;
        Vector3Int entityPos = new Vector3Int(Random.Range(room.min.x + offset, room.max.x), Random.Range(room.min.y + offset, room.max.y), 0);
        for (int i = 0; i < Random.Range(5, 10); i++)
        {
            entityPos = new Vector3Int(Random.Range(room.min.x + offset, room.max.x), Random.Range(room.min.y + offset, room.max.y), 0);
            if (entityPos != gameStateMemory.DungeonStartPos && corridors.Contains((Vector2Int)entityPos) == false)
            {
                Instantiate(clutter[Random.Range(0, clutter.Count)], entityPos + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            }
        }
    }

    private void createMonsterRoom(BoundsInt room, HashSet<Vector2Int>  corridors)
    {
        GameObject controller = GameObject.Find("GameController");
        
        Vector3Int entityPos = new Vector3Int(Random.Range(room.min.x + offset, room.max.x), Random.Range(room.min.y + offset, room.max.y), 0);
        for (int i = 0; i < Random.Range(1, 5); i++)
        {
            entityPos = new Vector3Int(Random.Range(room.min.x + offset, room.max.x), Random.Range(room.min.y + offset, room.max.y), 0);
            if (entityPos != gameStateMemory.DungeonStartPos && corridors.Contains((Vector2Int)entityPos) == false)
            {
                GameObject monster = Instantiate(lowLevelEnemies[Random.Range(0, lowLevelEnemies.Count)], entityPos + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                monsterController monsterScript = monster.GetComponent<monsterController>();

                if (monsterScript == null) { Debug.Log("debug 3"); }
                monsterScript.setPlayerObject(playerController);
                controller.GetComponent<GameController>().npcControllers.Add(monster); 
            }
        }
        for (int i = 0; i < Random.Range(5, 10); i++)
        {
            entityPos = new Vector3Int(Random.Range(room.min.x + offset, room.max.x), Random.Range(room.min.y + offset, room.max.y), 0);
            if (entityPos != gameStateMemory.DungeonStartPos && corridors.Contains((Vector2Int)entityPos) == false)
            {
                Instantiate(clutter[Random.Range(0, clutter.Count)], entityPos + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            }
        }
    }
    private void createEmptyRoom(BoundsInt room, HashSet<Vector2Int> corridors)
    {
        Vector3Int entityPos = new Vector3Int(Random.Range(room.min.x + offset, room.max.x), Random.Range(room.min.y + offset, room.max.y), 0);
        for (int i = 0; i < Random.Range(5, 10); i++)
        {
            entityPos = new Vector3Int(Random.Range(room.min.x + offset, room.max.x), Random.Range(room.min.y + offset, room.max.y), 0);
            if (entityPos != gameStateMemory.DungeonStartPos && corridors.Contains((Vector2Int)entityPos) == false)
            {
                Instantiate(clutter[Random.Range(0, clutter.Count)], entityPos + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            }
        }
    }
    private void createMimicRoom(BoundsInt room, HashSet<Vector2Int> corridors)
    {
        Vector3Int entityPos = new Vector3Int(Random.Range(room.min.x + offset, room.max.x), Random.Range(room.min.y + offset, room.max.y), 0);
        Instantiate(enemyMimic, entityPos + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        for (int i = 0; i < Random.Range(5, 10); i++)
        {
            entityPos = new Vector3Int(Random.Range(room.min.x + offset, room.max.x), Random.Range(room.min.y + offset, room.max.y), 0);
            if (entityPos != gameStateMemory.DungeonStartPos && corridors.Contains((Vector2Int)entityPos) == false)
            {
                Instantiate(clutter[Random.Range(0, clutter.Count)], entityPos + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            }
        }
    }


    private HashSet<Vector2Int> connectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();

        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = createCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> createCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();

        Vector2Int position = currentRoomCenter;
        corridor.Add(position);

        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;

        foreach (var roomCenter in roomCenters)
        {
            float currentDist = Vector2Int.Distance(currentRoomCenter, roomCenter);
            if (currentDist < distance)
            {
                closest = roomCenter;
                distance = currentDist;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> createSimpleRooms(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        HashSet<Vector2Int> tmpRoom = new HashSet<Vector2Int>();

        foreach (var room in roomList)
        {
            for (int col = offset; col < room.size.x; col++)
            {
                for(int row = offset; row < room.size.y; row++)
                {
                    Vector2Int pos = (Vector2Int)room.min + new Vector2Int(col, row);
                    tmpRoom.Add(pos);
                    floor.Add(pos);
                }
            }
            roomDict.Add((Vector2Int)Vector3Int.RoundToInt(room.center), tmpRoom);
            tmpRoom.Clear();
        }
        return floor;
    }
}
