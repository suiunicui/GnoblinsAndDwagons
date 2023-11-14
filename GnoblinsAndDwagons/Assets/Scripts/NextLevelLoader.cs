using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetxLevelLoader : MonoBehaviour, Interactable
{
    [SerializeField] GameStateMemory gameStateMemory;
    public void Interact()
    {
        gameStateMemory.clearGameState();
        gameStateMemory.inDungeon = true;
        gameStateMemory.leaveDungeon = true;
        gameStateMemory.dungeonLevel++;
        if(gameStateMemory.dungeonLevel < gameStateMemory.totalLevels)
        {
            SceneManager.LoadScene("RandomDungeon");
        }
        else
        {
            SceneManager.LoadScene("FinalLevel");
        }
    }
}
