using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonController : MonoBehaviour
{
    [SerializeField] 
    private RoomFirstGenerator roomFirstGenerator;
    [SerializeField]
    private CorridorFirstGenerator corridorFirstGenerator;
    [SerializeField]
    private GameStateMemory gameStateMemory;

    // Start is called before the first frame update
    public void handleStart()
    {
        if(gameStateMemory.dungeonLevel < gameStateMemory.totalLevels)
        {
            roomFirstGenerator.generateDungeon();
        }
    }


}
