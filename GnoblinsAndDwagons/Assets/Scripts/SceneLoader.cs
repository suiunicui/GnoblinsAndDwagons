using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, Interactable
{
    [SerializeField] GameStateMemory gameStateMemory;
    [SerializeField] public string sceneToLoad;
    public void Interact()
    {
        gameStateMemory.inShop = false;
        gameStateMemory.inDungeon = false;
        gameStateMemory.leaveDungeon = false;
        gameStateMemory.leaveShop = false;
        SceneManager.LoadScene(sceneToLoad);
    }
}
