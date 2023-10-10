using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetxLevelLoader : MonoBehaviour, Interactable
{
    [SerializeField] GameStateMemory gameStateMemory;
    public void Interact()
    {
        gameStateMemory.inShop = false;
        gameStateMemory.inDungeon = true;
        gameStateMemory.leaveDungeon = true;
        gameStateMemory.leaveShop = false;
        gameStateMemory.inCombat = false;
        gameStateMemory.leaveCombat = false;
        gameStateMemory.dungeonLevel++;
        SceneManager.LoadScene("RandomDungeon");
    }
}
