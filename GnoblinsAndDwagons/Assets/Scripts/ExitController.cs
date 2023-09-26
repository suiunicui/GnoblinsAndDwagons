using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour, Interactable
{
    [SerializeField] GameStateMemory gameStateMemory;
    public void Interact()
    {
        gameStateMemory.inDungeon = false;
        gameStateMemory.leaveDungeon = true;
        SceneManager.LoadScene("Camp");
    }
}
