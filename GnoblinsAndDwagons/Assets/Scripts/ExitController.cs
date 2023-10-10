using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour, Interactable
{
    [SerializeField] GameStateMemory gameStateMemory;
    public void Interact()
    {
        gameStateMemory.inShop = false;
        gameStateMemory.inDungeon = false;
        gameStateMemory.inCombat = false;
        gameStateMemory.leaveDungeon = true;
        gameStateMemory.leaveShop = false;
        gameStateMemory.leaveCombat = false;
        gameStateMemory.dungeonLevel = 0;
        SceneManager.LoadScene("Camp");
    }
}
