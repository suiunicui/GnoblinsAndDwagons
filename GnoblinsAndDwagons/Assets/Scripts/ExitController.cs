using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour, Interactable
{
    [SerializeField] GameStateMemory gameStateMemory;
    public void Interact()
    {
        gameStateMemory.clearGameState();
        gameStateMemory.leaveDungeon = true;
        gameStateMemory.dungeonLevel = 0;
        SceneManager.LoadScene("Camp");
    }
}
